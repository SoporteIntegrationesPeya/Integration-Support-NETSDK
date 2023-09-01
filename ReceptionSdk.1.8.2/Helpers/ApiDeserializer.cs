///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;

namespace ReceptionSdk.Helpers
{
    public class ApiDeserializer : JsonDeserializer
    {
        public T Deserialize<T>(string content)
        {
            object json = SimpleJson.DeserializeObject(content);
            return (T)this.ConvertValue(typeof(T), json);
        }
    }
}
