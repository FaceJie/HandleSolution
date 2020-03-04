﻿using Newtonsoft.Json;
using SharedHelper.ContractResolvers;

namespace SharedHelper.Helpers
{
    public static class JsonConvertHelper
    {
        public static string SerializeObjectRaw(object value)
        {
            return JsonConvert.SerializeObject(value,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto
                });
        }
        public static T DeserializeObjectRaw<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ContractResolver =  new NonPublicPropertiesResolver()
            });
        }
    }
}