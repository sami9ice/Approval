using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Utilities.UtilitiesFeatures
{
  
    public interface IEncryptionService
    {
        string Encrypt(string text);
    }
    public class EncryptionService : IEncryptionService
    {
        #region IEncryptionService Members

        public string Encrypt(string text)
        {
            //Calculate MD5 hash from input
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);

            //Convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        #endregion
    }

}
