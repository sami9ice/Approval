using Application.Response.User;
using MediatR;

namespace Application.Request.ApprovalLog
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CreateUserResponse[]}" />
    public class GetLogRequest : IRequest<GetlogResponse>
    {
    }
}