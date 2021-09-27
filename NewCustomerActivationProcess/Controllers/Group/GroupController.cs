using Api.ResponseWrapper;
using Application.Request.Group;
using Application.Request.User;
using Application.Response.Group;
using Application.Response.User;
using Application.Utilities.UserUtility;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Domain.Entities;
using Domain.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewCustomerActivationProcess.Controllers;
using Newtonsoft.Json;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Api.Endpoints
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Api.Endpoints.BaseApiController" />
    public class GroupController : BaseApiController
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> applicationManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountController"/> class.
        /// </summary>
        /// <param name="applicationManager">The application manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="configuration">The configuration.</param>

        public GroupController(OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            SignInManager<User> signInManager,

            IConfiguration configuration)
        {
            this.applicationManager = applicationManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

       
        //[ProducesDefaultResponseType(typeof(string))]
        //[HttpGet("delete/{id}")]
        //public async Task<IActionResult> DeleteUsers(string id)
        //{
        //    (bool succeed, string message) = await Mediator.Send(new DeleteUserCommand { Id = id });
        //    if (succeed)
        //        return Ok(message.ToResponse());
        //    return NotFound(message.ToResponse());
        //}

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
      //  [Authorize]
        [ProducesDefaultResponseType(typeof(GetAllGroupResponse[]))]
        [HttpGet("groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var group = await Mediator.Send(new GetAllGroupRequest());
            if (group != null)
                return Ok(group.ToResponse());
            return NotFound("No group found".ToResponse());
        }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
       // [Authorize]
        [ProducesDefaultResponseType(typeof(GetGroupByIdResponse))]
        [HttpPost("groupbyid")]
        public async Task<IActionResult> GetGroupById([FromBody] GetGroupByIdRequest id)
        {
            // var user = await Mediator.Send(new GetUserByIdRequest { Id=id});
            var group = await Mediator.Send(id);
            if (group != null)
                return Ok(group.ToResponse());
            return NotFound("No group found".ToResponse());
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="createUser">The create user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(CreateGroupResponse))]
        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest createGroup)
        {
            (bool succeed, string message, CreateGroupResponse groupResponse) = await Mediator.Send(createGroup);
            if (succeed)
                return Ok(groupResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }

        /// <summary>
        /// Edit the user.
        /// </summary>
        /// <param name="editUser">The edit user.</param>
        ///// <returns></returns>
        //[Authorize]

        [ProducesDefaultResponseType(typeof(EditGroupResponse))]
        [HttpPost("editGroup")]
        public async Task<IActionResult> EditGroup([FromBody] EditGroupRequest editGroup)
        {
            (bool succeed, string message, EditGroupResponse groupResponse) = await Mediator.Send(editGroup);
            if (succeed)
                return Ok(groupResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }

       
    }
}