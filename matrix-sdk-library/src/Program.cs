using matrix_sdk.src.ClientAuthentication;
using matrix_sdk.src.Rooms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace matrix_sdk.src
{
    public class Program
    {
        public static void Main()
        {
            //var requests = new Requests();
            var loginAuth = new Login();
            Requests.AccessToken = loginAuth.GetAuthorizationToken("your-login", "your-password", "m.login.password").Result;
            Console.WriteLine(Requests.AccessToken);
            //Console.ReadLine();
            //var sync = new Syncing();

            string roomId = "!VvnKmuhSNisblYfLVv:matrix.org";
            runReading(roomId);
            //var roomEvent = new Events(roomId);
            //List<string> messages = roomEvent.GetLastNMessagesInRoom(100);
            //foreach (string message in messages)
            //{
            //    Console.WriteLine(message);
            //}
            //var names = new List<string>();
            //JToken outer = roomEvent.GetMembers()[0];
            //Console.WriteLine(outer.ToString());
        }

        private static void runReading(string roomId)
        {
            var roomEvent = new Events(roomId);

            while (true)
            {
                List<string> messages = roomEvent.GetNewMessagesInRoom();
                foreach (string message in messages)
                {
                    Console.WriteLine(message);
                }
            }
        }


    }
}