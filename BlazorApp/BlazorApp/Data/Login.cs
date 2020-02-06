using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppLibrary;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Data
{
    public class Login
    {
        private readonly IConfiguration _config;

        public Login(IConfiguration config)
        {
            _config = config;
        }
        public bool GetToken(string username, string password)
        {
            string apiurl = _config.GetValue<string>("API");

            APIAccess api = new BlazorAppLibrary.APIAccess();

            string token = api.GetTokenFromAPI(apiurl, username, password);

            if (token == string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
