using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Application.Response.User
{
    /// <summary>
    ///
    /// </summary>
    public class ErrowVm
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [Display(Name = "Error")]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        [Display(Name = "Description")]
        public string ErrorDescription { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class AuthorizeVm
    {
        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        [Display(Name = "Application")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        [BindNever]
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        [Display(Name = "Scope")]
        public string Scope { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class LogoutVm
    {
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        [BindNever]
        public string RequestId { get; set; }
    }
}