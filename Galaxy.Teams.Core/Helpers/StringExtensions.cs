using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Galaxy.Teams.Core.Helpers
{
    public static class StringExtensions
    {
        public static object GetValue(this object value)
        {
            if (value is JObject jobject)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                using (IEnumerator<KeyValuePair<string, JToken>> enumerator = jobject.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<string, JToken> current = enumerator.Current;
                        dictionary.Add(current.Key, ((object) current.Value).GetValue());
                    }
                }
                return (object) dictionary;
            }
            if (value is JProperty jproperty)
                return (object) new Dictionary<string, object>()
                {
                    {
                        jproperty.Name,
                        ((object) jproperty.Value).GetValue()
                    }
                };
            if (value is JArray jarray)
                return (object) ((IEnumerable<JToken>) jarray.Children()).Aggregate<JToken, List<object>>(new List<object>(), (Func<List<object>, JToken, List<object>>) ((list, token) =>
                {
                    list.Add(((object) token).GetValue());
                    return list;
                }));
            if (!(value is JValue jvalue))
                return value;
            object obj = jvalue.Value;
            if (obj is long num && num >= (long) int.MinValue && num <= (long) int.MaxValue)
                return (object) (int) num;
            return obj;
        }
    }
}