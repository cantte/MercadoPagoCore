using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MercadoPagoCore.Core
{
    public class CustomDeserializationContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (!property.Writable)
            {
                PropertyInfo propertyInfo = member as PropertyInfo;
                if (property != null)
                {
                    property.Writable = propertyInfo.GetSetMethod(true) != null;
                }
            }

            return property;
        }
    }
}
