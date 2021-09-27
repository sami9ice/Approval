namespace NewCustomerActivationProcess.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class Clients
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }
        /// <summary>
        ///  Gets or sets the client secret associated with the application. Note: depending
        ///  on the application manager used when creating it, this property may be hashed
        ///  or encrypted for security reasons.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the consent type associated with the application.
        /// </summary>
        /// <value>
        /// The type of the consent.
        /// </value>
        public virtual string ConsentType { get; set; }

        /// <summary>
        /// Gets or sets the display name associated with the application.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the logout callback URLs associated with the application.
        /// </summary>
        /// <value>
        /// The post logout redirect uris.
        /// </value>
        public string PostLogoutRedirectUris { get; set; }
        /// <summary>
        ///  Gets the callback URLs associated with the application.
        /// </summary>
        /// <value>
        /// The redirect uris.
        /// </value>
        public string RedirectUris { get; set; }
        /// <summary>
        /// Gets or sets the application type associated with the application.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ClientResource
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        public string Resources { get; set; }
    }
}
