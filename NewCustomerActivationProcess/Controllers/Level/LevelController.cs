using Api.ResponseWrapper;
using Application.Request.Group;
using Application.Request.Level;
using Application.Request.User;
using Application.Response.Group;
using Application.Response.Level;
using Application.Response.User;
using Application.ResponseLevel;
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
    public class LevelController : BaseApiController
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

        public LevelController(OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
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
        [ProducesDefaultResponseType(typeof(GetAllLevelResponse[]))]
        [HttpGet("levels")]
        public async Task<IActionResult> GetAllLevels()
        {
            var level = await Mediator.Send(new GetAllLevelRequest());
            if (level != null)
                return Ok(level.ToResponse());
            return NotFound("No level found".ToResponse());
        }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
       // [Authorize]
        [ProducesDefaultResponseType(typeof(GetLevelByIdResponse))]
        [HttpPost("levelbyid")]
        public async Task<IActionResult> GetLevelById([FromBody] GetLevelByIdRequest id)
        {
            // var user = await Mediator.Send(new GetUserByIdRequest { Id=id});
            var level = await Mediator.Send(id);
            if (level != null)
                return Ok(level.ToResponse());
            return NotFound("No level found".ToResponse());
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="createUser">The create user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(CreateLevelResponse))]
        [HttpPost("CreateLevel")]
        public async Task<IActionResult> CreateLevel([FromBody] CreateLevelRequest createLevel)
        {
            (bool succeed, string message, CreateLevelResponse levelResponse) = await Mediator.Send(createLevel);
            if (succeed)
                return Ok(levelResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }

        /// <summary>
        /// Edit the user.
        /// </summary>
        /// <param name="editUser">The edit user.</param>
        ///// <returns></returns>
        //[Authorize]

        [ProducesDefaultResponseType(typeof(EditUserResponse))]
        [HttpPost("editLevel")]
        public async Task<IActionResult> EditUser([FromBody] EditLevelRequest editLevel)
        {
            (bool succeed, string message, EditLevelResponse levelResponse) = await Mediator.Send(editLevel);
            if (succeed)
                return Ok(levelResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }


    }
}