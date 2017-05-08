using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace matrix_sdk.src.Rooms
{
    class Events
    {
        private string url = "https://matrix.org/_matrix/client/r0/rooms/";
        private string roomId;
        private string oldBatch;

        public Events(string roomId)
        {
            this.roomId = roomId;
            url += roomId;
            var sync = new Syncing();
            oldBatch = sync.OldBatch;
        }

        public string GetRoomName()
        {
            string tmpUrl = url + "/state/m.room.name?";

            JObject responseObject = Requests.GET(tmpUrl).Result;

            return responseObject.First.ToString();
        }

        //return List<string> with all person in this room
        public List<string> GetMembers()
        {
            string tmpUrl = url + "/members?";

            JObject responseObject = Requests.GET(tmpUrl).Result;

            var membersList = new List<string>();

            foreach (var person in responseObject["chunk"])
            {
                membersList.Add(person["user_id"].ToString());
            }

            return membersList;
        }

        public List<string> GetNewMessagesInRoom()
        {
            var sync = new Syncing();

            string nextBatch = sync.GetNextBatch();
            string dir = "b"; //The direction to return events from. One of: ["b", "f"]

            string from = "from=" + nextBatch + "&";
            string to = "to=" + oldBatch + "&";
            dir = "dir=" + dir + "&";
            string tmpUrl = url + "/messages?" + from + to + dir;
            oldBatch = nextBatch;

            JObject responseObject = Requests.GET(tmpUrl).Result;

            return responseHandling(responseObject);
        }

        public List<string> GetLastNMessagesInRoom(int N)
        {
            var sync = new Syncing();

            string nextBatch = sync.GetNextBatch();
            string dir = "b"; //The direction to return events from. One of: ["b", "f"]

            string from = "from=" + nextBatch + "&";
            dir = "dir=" + dir + "&";
            string limit = "limit=" + N + "&";
            string tmpUrl = url + "/messages?" + from + dir + limit;
            oldBatch = nextBatch;

            JObject responseObject = Requests.GET(tmpUrl).Result;

            return responseHandling(responseObject);
        }

        private List<string> responseHandling(JObject response)
        {
            var listMessages = new List<string>();
            if (response["chunk"] != null)
            {
                foreach (JObject chunk in response["chunk"])
                {
                    if ((chunk["content"]["msgtype"] != null) &&
                        (chunk["content"]["msgtype"].ToString().Equals("m.text")))
                    {
                        listMessages.Add(chunk["content"]["body"].ToString());
                    }
                }
            }

            return listMessages;
        }

        //return event_id
        //private string SendMessage(string message, string messageType)
        public string SendMessage(string message)
        {
            string tmpUrl = url + "/send/m.room.message?";

            JObject body = JObject.FromObject(new
            {
                msgtype = "m.text",
                body = message
            });

            JObject responseObject = Requests.POST(tmpUrl, body).Result;

            return responseObject.First.ToString();
        }

        /*
         types messages:
         m.text
         m.emote
         m.notice
         m.image
         m.file
         m.location
         m.video
         m.audio
        */
    }
}
