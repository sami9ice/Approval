using Application.Response.CustomerData;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request.CustomerData
{
   public class GetCustomerByFormIdRequest: IRequest<GetCustomerDataByFormIDResponse>
    {
        public string FormId { get; set; }

    }
}
