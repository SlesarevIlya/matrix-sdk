using Newtonsoft.Json.Linq;

namespace matrix_sdk.src.Rooms
{
    class Creation
    {
        private string url = "https://matrix.org/_matrix/client/r0/createRoom?";

        //preset - type chat, public_chat for example
        //return room_id
        //POST
        public string CreateRoom(string preset, string roomName, string roomAliasName, string topicOfRoom)
        {

            JObject body = JObject.FromObject(new
            {
                preset = preset,
                room_alias_name = roomAliasName,
                name = roomName,
                topic = topicOfRoom,
                creation_content = new MFederateJson()
                {
                    MFederate = false
                }
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }
    }
}
