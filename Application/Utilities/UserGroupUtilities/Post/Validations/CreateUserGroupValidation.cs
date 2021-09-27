using Application.Request.CreateUserGroupMappingRequest;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Utilities.UserGroupUtilities.Post
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.User.UserGroupMapping}</cref>
    /// </seealso>
    public class CreateUserGroupValidation : AbstractValidator<CreateUserGroupMappingRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidation"/> class.
        /// </summary>
        public CreateUserGroupValidation()
        {
           // RuleFor(x => x.PIN).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.GroupdId).Must(x => x.Length > 0).WithMessage("Group must be provided");
            RuleFor(x => x.UserId).Must(x => x.Length > 0).WithMessage("User must be provided");
            // RuleFor(x => x.GroupId).Must(x => x.Length > 0).WithMessage("At least one Group must be provided");
        }
    }
}