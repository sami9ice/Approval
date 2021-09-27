using Application.Request.User;
using FluentValidation;
using FluentValidation.Validators;
using System;

namespace Application.Features.AuthenticationFeatures.Validations
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.User.CreateUserRequest}</cref>
    /// </seealso>
    public class CreateUserValidation : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidation"/> class.
        /// </summary>
        public CreateUserValidation()
        {
           // RuleFor(x => x.PIN).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.EmialAddress).EmailAddress(mode: EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Department).NotEmpty();
            RuleFor(x => x.PhoneNo).Must(x => x.Length !=10).WithMessage("Phone No must be 11 digit");
            //RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty();




            // RuleFor(x => x.GroupId).Must(x => x.Length > 0).WithMessage("At least one Group must be provided");
        }

    }
}