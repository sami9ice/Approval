using Application.Interfaces;
using Application.Response.Group;
using MediatR;


namespace Application.Request.Group
{
    public class CreateGroupRequest : IRequest<(bool Succeed, string Message, CreateGroupResponse Response)>, IMapFrom<Domain.User.Group>
    {
        public string GroupName { get; set; }
        public string GroupEmail{ get; set; }
        public string ApprovalLevelId { get; set; }



    }
}
