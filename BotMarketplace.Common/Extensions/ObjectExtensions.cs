using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace BotMarketplace.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(obj, options ?? new JsonSerializerOptions());  
        }
    }
}
