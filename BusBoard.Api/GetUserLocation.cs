﻿using System;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.Api
{
    public class GetUserLocation
    {
        public static UserLocation ReturnUserLocation(string postCode)
        {
            RestClient client = new RestClient("http://api.postcodes.io/postcodes/");
            RestRequest request = new RestRequest(postCode, Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine();
            PostCodeResponse responseObject = JsonConvert.DeserializeObject<PostCodeResponse>(response.Content);
            return responseObject.result;
        }
    }
}