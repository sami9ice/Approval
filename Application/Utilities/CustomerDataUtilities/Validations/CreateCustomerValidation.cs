using Application.Request.CustomerData;
using Application.Request.User;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Utilities.CustomerDataUtilities.Validations
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso>
    ///     <cref>FluentValidation.AbstractValidator{Application.Request.User.CreateUserRequest}</cref>
    /// </seealso>
    public class CreatecustomerValidation : AbstractValidator<CreateCustomerDataRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidation"/> class.
        /// </summary>
        public CreatecustomerValidation()
        {
           // RuleFor(x => x.PIN).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.EmialAddress).EmailAddress(mode: EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.firstName).NotEmpty();
            RuleFor(x => x.lastName).NotEmpty();
            RuleFor(x => x.AcctManagergPhoneNo).NotEmpty();
            RuleFor(x => x.AcctManagergName).NotEmpty();
            RuleFor(x => x.AcctManagergSignature).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();






        }
    }
}