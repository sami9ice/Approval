using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation;


namespace NewCustomerActivationProcess.Controllers
{
  // [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator mediator;
        /// <summary>
        /// 
        /// </summary>
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Users the identifier.
        /// </summary>
        /// <returns></returns>
        protected string UserId()
        {

            return User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        }
    }
}
