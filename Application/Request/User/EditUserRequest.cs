using Application.Response.User;
using MediatR;

namespace Application.Request.User
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         MediatR.IRequest{(System.Boolean succeed, System.String message, Application.Response.User.CreateUserResponse
    ///         userResponse)}
    ///     </cref>
    /// </seealso>
    public class EditUserRequest : IRequest<(bool succeed, string message, EditUserResponse userResponse)>
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public string[] Roles { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
       public string status { get; set; }

     
    }
}