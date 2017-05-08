using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace matrix_sdk.src.ClientAuthentication
{
    class Login
    {
        public async Task<string> GetAuthorizationToken(string user, string password, string type)
        {
            string url = "https://matrix.org/_matrix/client/r0/login";

            JObject body = JObject.FromObject(new
            {
                user = user,
                password = password,
                type = type
            });

            JObject responseObject = await POST(url, body);

            return responseObject["access_token"].ToString();
        }

        //post request without authorization
        public async Task<JObject> POST(string url, JObject body)
        {
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

        //TODO
        //tokenrefresh
        //logout
    }
}
