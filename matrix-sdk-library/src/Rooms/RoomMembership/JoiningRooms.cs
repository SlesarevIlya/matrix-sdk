using Newtonsoft.Json.Linq;

namespace matrix_sdk.src.RoomMembership
{
    class JoiningRooms
    {
        private string url = "https://matrix.org/_matrix/client/r0/rooms/";

        //response = 
        //  {} 
        //if the user has been invited to join the room. (for example)

        //response = 
        //  "errcode": "M_FORBIDDEN",
        //  "error": "@***** is banned from the room" for example
        //if error
        public string Invite(string roomId, string userId)
        {
            url += roomId + "/invite?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        public string Join(string roomId, string userId)
        {
            url += roomId + "/join?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        //TODO
        //roomIdOrAlias
    }
}
