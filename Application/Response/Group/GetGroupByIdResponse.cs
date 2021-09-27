using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Group
{
   public class GetGroupByIdResponse : IMapFrom<Domain.User.Group>
    {
        public string Id { get; set; }

        public string GroupName { get; set; }
        public string GroupEmail { get; set; }
        public bool Status { get; set; }

        //[ForeignKey("LevelId")]

        public string ApprovalLevelId { get; set; }
       // public ApprovalLevel level { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date Modified.
        /// </summary>
        /// <value>
        /// The date Modified.
        /// </value>
        public DateTimeOffset DateModified { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        public string DeletedBy { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
    }
}
