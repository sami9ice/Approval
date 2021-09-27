using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.User
{
    public class GetUserResponse 
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string PhoneNo { get; set; }
        public string emialAddress { get; set; }
        public string FullName { get; set; }
        // public byte Signature { get; set; }
        public string Department { get; set; }
        public string GroupId { get; set; }

       //public IList<string> GroupbelongedTo { get; set; }
        public string[] GroupbelongedTo { get; set; }

        public Level[] LevelbelongedTo { get; set; }
       // public string[] LevelbelongedTo { get; set; }

    }
    public class GetgroupResponse
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }


    }
    public class Level
    {
        public string Id { get; set; }
        public string Name { get; set; }


    }

}
