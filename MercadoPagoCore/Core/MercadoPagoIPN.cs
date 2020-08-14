using System;
using System.Reflection;
using MercadoPagoCore.Exceptions;

namespace MercadoPagoCore.Core
{
    public class MercadoPagoIPN
    {
        public partial class Topic
        {
            public static string MerchantOrder { get { return "MercadoPago.Resources.MerchantOrder"; } }
            public static string Payment { get { return "MercadoPago.Resources.Payment"; } }
        }

        public static Type GetType(string resourceClassName)
        {
            Type type = Type.GetType(resourceClassName);
            if (type != null) return type;
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(resourceClassName);
                if (type != null)
                    return type;
            }
            return null;
        }

        public static MercadoPagoBase Manage<T>(string topic, string id) where T : MercadoPagoBase
        {

            if (string.IsNullOrEmpty(topic) || string.IsNullOrEmpty(id))
            {
                throw new MercadoPagoException("Topic and Id can not be null in the IPN request.");
            }

            MercadoPagoBase resourceObject;
            try
            {
                Type classType = GetType(topic);
                if (!classType.IsSubclassOf(typeof(MercadoPagoBase)))
                {
                    throw new MercadoPagoException(classType.Name + " does not extend from MercadoPagoBase");
                }

                MethodInfo method = classType.GetMethod("Load", new[] { typeof(string) });

                object classInstance = Activator.CreateInstance(classType, null);
                object[] parametersArray = new object[] { id };

                resourceObject = (T)method.Invoke(classInstance, parametersArray);
            }
            catch (Exception ex)
            {
                throw new MercadoPagoException(ex.Message);
            }

            return resourceObject;
        }
    }
}
