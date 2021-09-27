using Api.ResponseWrapper;
using Application.Request.ApprovalLog;
using Application.Request.User;
using Application.Response.ApprovalLog;
using Application.Response.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCustomerActivationProcess.Controllers.ApprovalLog
{
    public class ApprovalLog:BaseApiController
    {
        [ProducesDefaultResponseType(typeof(ApprovalLogResponse))]
        [HttpGet("{id}/LogByFormId")]
        public async Task<IActionResult> GetUsersById(int id)
        {

            GetApprovalLog Formid = new GetApprovalLog();
            Formid.FormId = id.ToString();
            var user = await Mediator.Send(new GetApprovalLog());
            if (user != null)
                return Ok(user.ToResponse());
            return NotFound("No log found".ToResponse());
        }
        //  [Authorize]
        [ProducesDefaultResponseType(typeof(GetlogResponse[]))]
        [HttpGet("AllLog")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await Mediator.Send(new GetLogRequest());
            if (user != null)
                return Ok(user.ToResponse());
            return NotFound("No user found".ToResponse());
        }
    }
}
