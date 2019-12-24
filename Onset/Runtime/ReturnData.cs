using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Onset.Runtime
{
    internal class ReturnData
    {
        internal bool IsFailed { get; }

        internal JToken this[string name] => _content[name];

        private readonly JObject _content;
        private readonly string _serializedContent;

        internal ReturnData(string data)
        {
            _serializedContent = data;
            if (string.IsNullOrEmpty(data))
            {
                IsFailed = true;
            }
            else
            {
                IsFailed = false;
                _content = JObject.Parse(data);
            }
        }

        internal T Value<T>(string name = null)
        {
            return name == null ? _content.Value<T>() : _content.Value<T>(name);
        }

        internal T Deserialize<T>()
        {
            return JsonConvert.DeserializeObject<T>(_serializedContent);
        }
    }
}
