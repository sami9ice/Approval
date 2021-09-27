using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeneratorService
    {
        /// <summary>
        /// Generates the asset identifier.
        /// </summary>
        /// <returns></returns>
        //   Task<string> GenerateAssetId();
        /// <summary>
        /// Generates the identifier.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        //   Task<string> GenerateId(string v);
        bool SendMail(string FormId, string ApprovedBy, string Email, string body);
        bool SendMailForCreateUser(string ApprovedBy, string Email, string body);


    }
}