using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace OpenSkyDemo
{
    class Program
    {
        private static string endpoint = "https://opensky-network.org/api/states/all";

        static void Main(string[] args)
        {
            Console.WriteLine("Fetching data from OpenSky API");
            // ### uncomment to send request to openSky api service
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            var result = JsonConvert.DeserializeObject<StatesApiResponse>(MakeRequest(request));

            // this result is using a trimmed response from the service for speed
            // var result = JsonConvert.DeserializeObject<StatesApiResponse>(MockResponse.ShortTrimmedResponse);

            Console.WriteLine(result.States[0]);
            // if using Object[] on api response oobject, the error is:
            // Unable to cast object of type 'Newtonsoft.Json.Linq.JArray' to type MartinJsonParser.State
            // if using State[]
            // Cannot create and populate list type MartinJsonParser.State. Path 'states[0]', line 1, position 30.

            // the solution is to use an object Array on the Response API object, 
            // and then use your own converter.
            // Inner type is Newtonsoft.Json.Linq.JArray
            foreach (var state in result.States)
            {
                State myState = State.ConvertJArray((JArray)state);
                Console.WriteLine("Accessing Array entry as type State, callsign is :");
                Console.WriteLine(myState.callsign);
            }
        }

        // butchered this
        // https://www.codeproject.com/tips/497123/how-to-make-rest-requests-with-csharp
        static string MakeRequest(HttpWebRequest request)
        {
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }
    }
}