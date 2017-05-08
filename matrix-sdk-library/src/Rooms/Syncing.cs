using Newtonsoft.Json.Linq;

namespace matrix_sdk.src.Rooms
{
    class Syncing
    {
        private string oldBatch;

        public string OldBatch
        {
            get
            {
                return oldBatch;
            }
            set
            {
                oldBatch = value;
            }
        }

        private string url = "https://matrix.org/_matrix/client/r0/sync?";

        public Syncing()
        {
            oldBatch = GetNextBatch();
        }

        //get synchronize object from server
        private JObject SynchronizeClient()
        {
            JObject responseObject = Requests.GET(url).Result;

            return responseObject;
        }

        //get batch id from synchronize object
        public string GetNextBatch()
        {
            JObject responseObject = SynchronizeClient();

            return responseObject["next_batch"].ToString();
        }

        public string GetPrevBatch(string roomId)
        {
            JObject responseObject = Requests.GET(url).Result;
            return responseObject["rooms"]["join"][roomId]["timeline"]["prev_batch"].ToString();
        }

    }
}
