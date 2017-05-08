using Newtonsoft.Json.Linq;

namespace matrix_sdk.src.RoomMembership
{
    class LeavingRooms
    {
        private string url = "https://matrix.org/_matrix/client/r0/rooms/";

        //response = 
        //  {} 
        //if The room has been forgotten..

        //response = 
        //  "errcode": "M_FORBIDDEN",
        //  "error": "You do not have a high enough power level to kick from this room." (for example)
        //if error

        private string Forget(string roomId, string userId)
        {
            url += roomId + "/forget?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        private string Leave(string roomId, string userId)
        {
            url += roomId + "/leave?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        private string Kick(string roomId, string userId)
        {
            url += roomId + "/kick?";

            JObject body = JObject.FromObject(new
            {
                user_id = userId
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }

        private string Kick(string roomId, string userId, string reason)
        {
            url += roomId + "/kick?";
            JObject body = JObject.FromObject(new
            {
                user_id = userId,
                reason = reason
            });

            JObject responseObject = Requests.POST(url, body).Result;

            return responseObject.First.ToString();
        }
    }
}
