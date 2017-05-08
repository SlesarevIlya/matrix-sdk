using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace matrix_sdk.src
{
    static class Requests
    {

        private static string accessToken;

        public static string AccessToken
        {
            get
            {
                return accessToken;
            }
            set
            {
                accessToken = value;
            }
         }

        //for unstatic
        //public Requests()
        //{
        //    var login = new Login();
        //    AccessToken = login.GetAuthorizationToken("@spisok-anonymous:matrix.org", "spisokpassword", "m.login.password"); 
        //}

        public static async Task<JObject> POST(string url, JObject body)
        {
            url += "access_token=" + accessToken;

            UTF8Encoding enc = new UTF8Encoding();
            string json = body.ToString();

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            Stream dataStream = await request.GetRequestStreamAsync();
            dataStream.Write(enc.GetBytes(json), 0, json.Length);


            WebResponse wr = await request.GetResponseAsync();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);

            var responseObject = JObject.Parse(reader.ReadToEnd());

            return responseObject;
        }

        public static async Task<JObject> POST(string url, JObject body, string queryParameters)
        {
            return await POST(url + queryParameters, body);
        }

        public static async Task<JObject> GET(string url)
        {
            url += "access_token=" + accessToken;

            UTF8Encoding enc = new UTF8Encoding();

            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            WebResponse wr = await request.GetResponseAsync();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);

            var responseObject = JObject.Parse(reader.ReadToEnd());

            return responseObject;
        }

        public static async Task<JObject> GET(string url, string queryParameters)
        {
            return await GET(url + queryParameters);
        }
    }
}
