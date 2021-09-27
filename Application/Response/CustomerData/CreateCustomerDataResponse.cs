using Application.Interfaces;
using Application.Request.CustomerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response.CustomerData
{
   public class CreateCustomerDataResponse: IMapFrom<CreateCustomerDataRequest>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
