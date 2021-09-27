using Application.Interfaces;
using Application.Request.User;
using Application.Response.User;
using Domain.Entities;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthenticateRequest = Application.Request.User.AuthenticateRequest;

namespace Application.Features.AuthenticationFeatures.Commands
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso>
    /// <cref>MediatR.IRequestHandler{Application.Request.User.AuthenticateRequest, (System.Boolean Succeed, System.String Message, System.Object user)}</cref>
    /// </seealso>
    public class AuthenticateUser : IRequestHandler<AuthenticateRequest, (bool Succeed, string Message, (object user, object userGroup))>
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Group> roleManager;

        private readonly SignInManager<User> signInManager;
        private readonly IApplicationDbContext applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateUser"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="applicationDbContext"></param>
        public AuthenticateUser(RoleManager<Group> roleManager,UserManager<User> userManager, SignInManager<User> signInManager, IApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationDbContext = applicationDbContext;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<(bool Succeed, string Message, (object user, object userGroup))> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            //var user = await userManager.Users.Where(x=>x.Email==request.Email).FirstOrDefaultAsync();
            if (request.Password==null)
            {
                return (false, "Password cannot be null", (null, null));
            }
            //System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            //byte[] encData_byte = new byte[request.Password.Length];
            //encData_byte = System.Text.Encoding.UTF8.GetBytes(request.Password);
            //string encodedData = Convert.ToBase64String(encData_byte);
            //request.Password = encodedData;
           // request.Password = hash ;

            var user = await userManager.FindByEmailAsync(request.Email);

            //var pass = await userManager.CheckPasswordAsync(user, request.Password);

            if (user == null) user = await userManager.FindByNameAsync(request.Username);
            if (user != null)
            {
                //byte[] encData_byte = new byte[request.Password.Length];
                //encData_byte = System.Text.Encoding.UTF8.GetBytes(request.Password);
                //string encodedData = Convert.ToBase64String(encData_byte);
                var PasswordPass = request.Password;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(request.Password);
                var encodedData = System.Convert.ToBase64String(plainTextBytes);

                request.Password = encodedData;
                if(user.PIN!=encodedData) return (false, "Username or password does not eist", (null, null));

                if (user.IsDeleted)
                {
                    return (false, "The user has been deleted", (null, null));
                }
                if (user.ComfirmPin == false)
                {
                    return (false, "The User password has not been changed", (null, null));
                }
                if (user.status == "Pending")
                {
                    //return (false, "The User awaits approval from system control", (null, null));
                }
                if (user.status == "Reject")
                {
                    return (false, "The User has been rejected", (null, null));
                }
                
                bool lockoutOnFailure=true;

                var result = await signInManager.CheckPasswordSignInAsync(user, PasswordPass, lockoutOnFailure);
                if (result.Succeeded)
                {
                    //var userGroup = await roleManager.Roles.Where(x => x.Id == user.GroupId)
                    //    .Select(x => new Group { Id = x.GroupId, GroupName=x.GroupName,GroupEmail=x.GroupEmail})
                    //    .ToArrayAsync();

                    var getGroup = await userManager.GetRolesAsync(user);
                    var userGroup = roleManager.Roles.Where(x => getGroup.Contains(x.Name)).ToArray();
                    if (userGroup.Any())
                    {
                        return (true, "User has been authenticated successfully", (user, userGroup));
                    }
                    else
                    {
                        return (true, "User has been authenticated successfully but not mapped yet to a group", (user, null));
                    }
                }
                else if (result.IsLockedOut)
                {
                    return (false, "The user has been locked out", (null, null));
                }
                else if (result.RequiresTwoFactor)
                {
                    return (false, "The requires two factor authentication", (user, null));
                }
                else if (result.IsNotAllowed)
                {
                    return (false, "The user is not allowed", (null, null));
                }
            }
            else
            {
                return (false, "Invalid login  attempt ", (null, null));
            }

            return (false, "Unable to login user, invalid request", (null, null));
        }
    }
}