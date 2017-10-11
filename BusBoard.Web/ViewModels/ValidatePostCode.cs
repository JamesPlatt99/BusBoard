using System;
using BusBoard.Api;
using BusBoard.Web.Models;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.Web.ViewModels
{
    public class ValidatePostCode
    {
        public ValidatePostCode(PostcodeSelection selection)
        {
            valid = isValidPostCode(selection.Postcode);
        }

        public bool valid { get; set; }
        private Boolean isValidPostCode(string postCode)
        {
            RestClient client = new RestClient("http://api.postcodes.io/");
            RestRequest request = new RestRequest("postcodes/{postCode}/validate", Method.GET);
            request.AddUrlSegment("postCode",postCode );
            IRestResponse response = client.Execute(request);
            Console.WriteLine();
            ValidPostCode responseObject = JsonConvert.DeserializeObject<ValidPostCode>(response.Content);
            return responseObject.result;
        }
    }
}