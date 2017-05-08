using Newtonsoft.Json.Linq;

namespace matrix_sdk.src.RoomMembership
{
    class Banning
    {
        private string url = "https://matrix.org/_matrix/client/r0/rooms/";

        //response = 
        //  {} 
        //if the user has been kicked and banned from the room. (for example)

        //response = 
        //  "errcode": "M_FORBIDDEN",
        //  "error": "You do not have a high enough power level to unban from this room."
        //if error
        public string Ban(string roomId, string userId)
        {
            url += roomId + "/ban?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        public string Ban(string roomId, string userId, string reason)
        {
            url += roomId + "/ban?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId,
                reason = reason
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        public string Unban(string roomId, string userId)
        {
            url += roomId + "/unban?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

    }
}
