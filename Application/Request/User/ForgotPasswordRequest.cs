using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.User
{
    public class ForgotPasswordRequest:IRequest<(bool success, string message)>
    {
        public string Email { get; set; }
    }
}
