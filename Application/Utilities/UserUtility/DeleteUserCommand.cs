using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.UserUtility
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="MediatR.IRequest{(System.Boolean succeed, System.String message)}" />
    public class DeleteUserCommand : IRequest<(bool succeed, string message)>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso>
        ///     <cref>MediatR.IRequestHandler{Commands.DeleteUserCommand, (System.Boolean succeed, System.String message)}</cref>
        /// </seealso>
        private class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, (bool succeed, string message)>
        {
            private readonly UserManager<User> userManager;

            /// <summary>
            /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
            /// </summary>
            /// <param name="userManager">The user manager.</param>
            public DeleteUserCommandHandler(UserManager<User> userManager)
            {
                this.userManager = userManager;
            }

            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<(bool succeed, string message)> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (user != null)
                {

                    var usertable = await userManager.GetRolesAsync(user);


                       var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                        return (true, "The user has been deleted successfully");
                    return (false, string.Join(',', result.Errors));
                }

                return (false, "The user is not found");
            }
        }
    }
}