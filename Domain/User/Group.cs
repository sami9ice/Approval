using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.User
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityRole" />
    public class Group : IdentityRole
    {
        // public int GroupdId { get; set; }
       // [Key]
       // public string GroupId { get; set; } = Guid.NewGuid().ToString();

        public string GroupName { get; set; }
        public string GroupEmail { get; set; }
        public bool Status { get; set; }

        //[ForeignKey("LevelId")]

        public string ApprovalLevelId { get; set; }
        public ApprovalLevel level { get; set; }
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
        public User User { get; set; }

        //public string CreatedBy { get; set; }



    }
}