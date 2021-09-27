using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.User
{
    public class CreateUserResponse
    {
     

        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string UserName { get; set; }

        public string PhoneNo { get; set; }
        public string emialAddress { get; set; }
        // public byte Signature { get; set; }
        public string Department { get; set; }
       // public string groupId { get; set; }


    }
}
