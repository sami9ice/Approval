using Application.Response.User;
using MediatR;

namespace Application.Request.User
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CreateUserResponse[]}" />
    public class GetUsersRequest : IRequest<GetUserResponse[]>
    {
    }
}