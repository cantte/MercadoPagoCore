using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MercadoPagoCore.Core
{
    public class CustomSerializationContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = propInstance => property.Writable;

            if (!property.Readable)
            {
                PropertyInfo propertyInfo = member as PropertyInfo;
                if (property != null)
                {
                    property.Readable = propertyInfo.GetGetMethod(true) != null;
                }
            }

            return property;
        }
    }
}
