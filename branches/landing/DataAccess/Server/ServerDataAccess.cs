using DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DataAccess.Server
{
    public class ServerDataAccess
    {
        public List<Exercise> ListExercises()
        {
            return this.Get<Exercise>("Exercises");
        }
        
        public void PutExercise(Exercise exercise)
        {
            Put<Exercise>("Exercises", exercise);
        }

        protected List<T> Get<T>(string controllerName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://sqlconfidence.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/" + controllerName + "/get").Result;
                if (response.IsSuccessStatusCode)
                {
                    List<T> objectsFromServer = response.Content.ReadAsAsync<List<T>>().Result;
                    return objectsFromServer;
                }
                else
                {
                    throw new Exception("Error getting data from server from " + controllerName + " - " + response.Content);
                }
            }
        }

        protected void Put<T>(string controllerName, T data)
        {
            using (var client = new WebClient())
            {
                var baseAddress = new Uri("http://sqlconfidence.com/api/");
                
                var request = (HttpWebRequest)WebRequest.Create(baseAddress + "api/" + controllerName + "/put");
                request.Method = "PUT";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    }));
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Error putting data to the server - " + response.StatusCode);
                }
            }
        }
    }
}
