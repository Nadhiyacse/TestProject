using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Automation_Framework.PublicApi.Base.Serialization
{
    public class JsonSerializer : ISerializer, IDeserializer
    {
        private static JsonSerializer serializer;

        private JsonSerializer()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            ContentType = "application/json";
        }

        public static JsonSerializer Instance
        {
            get
            {
                if (serializer == null)
                {
                    serializer = new JsonSerializer();
                }

                return serializer;
            }
        }

        public JsonSerializerSettings SerializerSettings { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, SerializerSettings);
        }

        public T Deserialize<T>(string message)
        {
            return JsonConvert.DeserializeObject<T>(message, SerializerSettings);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content, SerializerSettings);
        }
    }
}
