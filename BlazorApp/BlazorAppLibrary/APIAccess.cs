using BlazorAppLibrary.Models;
using DocumentFormat.OpenXml.Office.CustomUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppLibrary
{
    public class APIAccess
    {
        public string GetTokenFromAPI(string apiUrl, string username, string password)
        {
            string output = string.Empty;
                                                                                    
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(apiUrl+ "api/login?username=" + username+ "&pass=" + password);

            WebReq.Method = "GET";

            TokenModel token = new TokenModel();

            try
            {
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                string jsonString;

                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }

                token = JsonConvert.DeserializeObject<TokenModel>(jsonString);
            }
            catch
            {
                token.Token = string.Empty;                
            }

            return token.Token;
        }
    }
}
