using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using MercadoPagoCore.Core.Annotations;
using MercadoPagoCore.Core.Endpoints;
using MercadoPagoCore.DataStructures.Generic;
using MercadoPagoCore.Exceptions;
using MercadoPagoCore.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Core
{
    public abstract class MercadoPagoBase
    {
        public static readonly bool WITHOUT_CACHE = false;
        public static readonly bool WITH_CACHE = true;
        public static readonly List<string> ALLOWED_BULK_METHODS = new List<string>() { "All", "Search", "CreateAll" };

        public static readonly string DataTypeError = "Error on property #PROPERTY. The value you are trying to assign has not the correct type. ";
        public static readonly string RangeError = "Error on property #PROPERTY. The value you are trying to assign is not in the specified range. ";
        public static readonly string RequiredError = "Error on property #PROPERTY. There is no value for this required property. ";
        public static readonly string RegularExpressionError = "Error on property #PROPERTY. The specified value is not valid. RegExp: #REGEXPR . ";

        public static string IdempotencyKey { get; set; }

        protected MercadoPagoAPIResponse _lastApiResponse;
        protected JObject _lastKnownJson;

#nullable enable
        protected BadParamsError? _errors;
        public BadParamsError? Errors
        {
            get { return _errors; }
            private set { _errors = value; }
        }
#nullable disable

        protected string _marketplaceAccessToken;
        public string MarketplaceAccessToken
        {
            get { return _marketplaceAccessToken; }
            set { _marketplaceAccessToken = value; }
        }

        public bool ShouldSerializeMarketplaceAccessToken()
        {
            return false;
        }

        public void DelegateErrors(BadParamsError DelegatedErrors)
        {
            _errors = DelegatedErrors;
        }

        public MercadoPagoAPIResponse GetLastApiResponse()
        {
            return _lastApiResponse;
        }

        public JObject GetLastKnownJson()
        {
            return _lastKnownJson;
        }

        public static void AdmitIdempotencyKey(Type classType)
        {
            object[] attribute = classType.GetCustomAttributes(true);

            foreach (Attribute attr in attribute)
            {
                if (attr.GetType() == typeof(Idempotent))
                {
                    IdempotencyKey = attr.GetType().GUID.ToString();
                }
            }
        }

        public static List<T> ProcessMethodBulk<T>(Type clazz, string methodName, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethodBulk<T>(clazz, methodName, useCache, null);
        }

        public static List<T> ProcessMethodBulk<T>(Type clazz, string methodName, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            return ProcessMethodBulk<T>(clazz, methodName, (Dictionary<string, string>)null, useCache, requestOptions);
        }

        public static List<T> ProcessMethodBulk<T>(Type clazz, string methodName, string param1, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethodBulk<T>(clazz, methodName, param1, useCache, null);
        }

        public static List<T> ProcessMethodBulk<T>(Type clazz, string methodName, string param1, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            Dictionary<string, string> mapParams = new Dictionary<string, string>
            {
                { "param1", param1 }
            };
            return ProcessMethodBulk<T>(clazz, methodName, mapParams, useCache, requestOptions);
        }

        public static MercadoPagoBase ProcessMethod(string methodName, bool useCache)
        {
            return ProcessMethod(methodName, useCache, null);
        }

        public static MercadoPagoBase ProcessMethod(string methodName, bool useCache, RequestOptions requestOptions)
        {
            Type classType = GetTypeFromStack();
            AdmitIdempotencyKey(classType);
            Dictionary<string, string> mapParams = new Dictionary<string, string>();
            return ProcessMethod<MercadoPagoBase>(classType, null, methodName, mapParams, useCache, requestOptions);
        }

        public static MercadoPagoBase ProcessMethod<T>(Type type, string methodName, string param, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethod<T>(type, methodName, param, useCache, null);
        }

        public static MercadoPagoBase ProcessMethod<T>(Type type, string methodName, string param, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            Type classType = GetTypeFromStack();
            AdmitIdempotencyKey(classType);
            Dictionary<string, string> mapParams = new Dictionary<string, string>
            {
                { "id", param }
            };

            return ProcessMethod<T>(classType, null, methodName, mapParams, useCache, requestOptions);
        }

        public static MercadoPagoBase ProcessMethod<T>(Type clazz, string methodName, string param1, string param2, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethod<T>(clazz, methodName, param1, param2, useCache, null);
        }

        public static MercadoPagoBase ProcessMethod<T>(Type clazz, string methodName, string param1, string param2, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            Dictionary<string, string> mapParams = new Dictionary<string, string>
            {
                { "param0", param1 },
                { "param1", param2 }
            };

            return ProcessMethod<T>(clazz, null, methodName, mapParams, useCache, requestOptions);
        }

        public static MercadoPagoBase ProcessMethod<T>(string methodName, string param, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethod<T>(methodName, param, useCache, null);
        }

        public static MercadoPagoBase ProcessMethod<T>(string methodName, string param, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            Type classType = GetTypeFromStack();
            AdmitIdempotencyKey(classType);
            Dictionary<string, string> mapParams = new Dictionary<string, string>
            {
                { "id", param }
            };
            return ProcessMethod<T>(classType, null, methodName, mapParams, useCache, requestOptions);
        }

        public MercadoPagoBase ProcessMethod<T>(string methodName, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethod<T>(methodName, useCache, null);
        }

        public MercadoPagoBase ProcessMethod<T>(string methodName, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            Dictionary<string, string> mapParams = null;
            _ = ProcessMethod<T>(GetType(), (T)this, methodName, mapParams, useCache, requestOptions);
            return (T)this;
        }

        public bool ProcessMethodBool<T>(string methodName, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethodBool<T>(methodName, useCache, null, null);
        }

        public bool ProcessMethodBool<T>(string methodName, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            return ProcessMethodBool<T>(methodName, useCache, null, requestOptions);
        }

        public bool ProcessMethodBool<T>(string methodName, bool useCache, Dictionary<string, string> mapParams, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            T resource = ProcessMethod<T>(GetType(), (T)this, methodName, mapParams, useCache, requestOptions);
            return resource.Errors == null;
        }

        protected static List<T> ProcessMethodBulk<T>(Type clazz, string methodName, Dictionary<string, string> mapParams, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethodBulk<T>(clazz, methodName, mapParams, useCache, null);
        }

        protected static List<T> ProcessMethodBulk<T>(Type clazz, string methodName, Dictionary<string, string> mapParams, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            //Validates the method executed
            if (!ALLOWED_BULK_METHODS.Contains(methodName))
            {
                throw new MercadoPagoException("Method \"" + methodName + "\" not allowed");
            }

            MethodInfo annotatedMethod = GetAnnotatedMethod(clazz, methodName);
            Dictionary<string, object> hashAnnotation = GetRestInformation(annotatedMethod);

            if (requestOptions == null)
            {
                int retries = (int)hashAnnotation["retries"];
                int requestTimeout = (int)hashAnnotation["requestTimeout"];
                requestOptions = new RequestOptions
                {
                    Retries = retries,
                    Timeout = requestTimeout
                };
            }

            T resource = null;
            HttpMethod httpMethod = (HttpMethod)hashAnnotation["method"];
            PayloadType payloadType = (PayloadType)hashAnnotation["payloadType"];
            string path = ParsePath(hashAnnotation["path"].ToString(), mapParams, resource, requestOptions);
            JObject payload = (httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT) ? new JObject() : null;

            MercadoPagoAPIResponse response = CallAPI(httpMethod, path, payloadType, payload, useCache, requestOptions);

            List<T> resourceArray = new List<T>();
            if (response.StatusCode >= 200 && response.StatusCode < 300)
            {
                resourceArray = FillArrayWithResponseData<T>(clazz, response);
            }

            return resourceArray;
        }

        protected static T ProcessMethod<T>(Type clazz, T resource, string methodName, Dictionary<string, string> parameters, bool useCache) where T : MercadoPagoBase
        {
            return ProcessMethod<T>(clazz, resource, methodName, parameters, useCache, null);
        }

        protected static T ProcessMethod<T>(Type clazz, T resource, string methodName, Dictionary<string, string> parameters, bool useCache, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            if (resource == null)
            {
                try
                {
                    resource = (T)Activator.CreateInstance(clazz);
                }
                catch (Exception ex)
                {
                    throw new MercadoPagoException(ex.Message);
                }
            }

            MethodInfo clazzMethod = GetAnnotatedMethod(clazz, methodName);
            Dictionary<string, object> restData = GetRestInformation(clazzMethod);
            HttpMethod httpMethod = (HttpMethod)restData["method"];
            PayloadType payloadType = (PayloadType)restData["payloadType"];
            JObject payload = GeneratePayload(httpMethod, resource);

            if (requestOptions == null)
            {
                int requestTimeout = (int)restData["requestTimeout"];
                int retries = (int)restData["retries"];
                requestOptions = new RequestOptions
                {
                    Retries = retries,
                    Timeout = requestTimeout
                };
            }

            string path = ParsePath(restData["path"].ToString(), parameters, resource, requestOptions);
            MercadoPagoAPIResponse response = CallAPI(httpMethod, path, payloadType, payload, useCache, requestOptions);

            if (response.StatusCode >= 200 && response.StatusCode < 300)
            {
                if (httpMethod != HttpMethod.DELETE)
                {
                    resource = (T)FillResourceWithResponseData(resource, response);
                    resource._lastApiResponse = response;
                }
                else
                {
                    resource = null;
                }
            }
            else if (response.StatusCode >= 400 && response.StatusCode < 500)
            {
                BadParamsError badParamsError = MercadoPagoCoreUtils.GetBadParamsError(response.StringResponse);
                resource.Errors = badParamsError;
            }
            else
            {
                MercadoPagoException webserverError = new MercadoPagoException()
                {
                    StatusCode = response.StatusCode,
                    ErrorMessage = response.StringResponse
                };
                webserverError.Cause.Add(response.JsonObjectResponse.ToString());
                throw webserverError;
            }

            return resource;
        }

        public static JObject GeneratePayload<T>(HttpMethod httpMethod, T resource) where T : MercadoPagoBase
        {
            if (httpMethod.ToString() == "PUT")
            {
                JObject actualJSON = MercadoPagoCoreUtils.GetJsonFromResource(resource);
                JObject oldJSON = resource.GetLastKnownJson();
                return GetDiffFromLastChange(actualJSON, oldJSON);
            }
            else if (httpMethod.ToString() == "POST")
            {
                return MercadoPagoCoreUtils.GetJsonFromResource(resource);
            }
            else
            {
                return null;
            }
        }

        public static JObject GetDiffFromLastChange(JToken jactual, JToken jold)
        {
            JObject new_jobject = new JObject();

            if (((JObject)jactual).Properties().Count() > 0)
            {
                foreach (JProperty x in ((JObject)jactual).Properties())
                {
                    string key = ToSnakeCase(x.Name);

                    if (x.Value.GetType() == typeof(JObject))
                    {
                        if (jold != null)
                        {
                            JObject new_value = GetDiffFromLastChange(x.Value, ((JObject)jold[x.Name]));
                            if (new_value != null)
                            {
                                if (new_value.Properties().Count() > 0)
                                {
                                    new_jobject.Add(key, new_value);
                                }
                            }
                        }
                        else if (x.Value.HasValues)
                        {
                            new_jobject.Add(key, x.Value);
                        }
                    }
                    else if (x.Value.GetType() == typeof(JArray))
                    {
                        if (x.Value.ToString() != jold[x.Name].ToString())
                        {
                            new_jobject.Add(key, x.Value);
                        }
                    }
                    else if (x.Value.GetType() == typeof(JValue))
                    {
                        if (jold != null)
                        {
                            if (jold[x.Name] != null)
                            {
                                if ((string)x.Value != (string)jold[x.Name])
                                {
                                    new_jobject.Add(key, x.Value);
                                }
                            }
                            else
                            {
                                new_jobject.Add(key, x.Value);
                            }
                        }
                        else
                        {
                            new_jobject.Add(key, x.Value);
                        }
                    }
                }
                return new_jobject;
            }
            else
            {
                return null;
            }
        }

        protected static MercadoPagoBase FillResourceWithResponseData<T>(T resource, MercadoPagoAPIResponse response) where T : MercadoPagoBase
        {
            if (response.JsonObjectResponse != null &&
                    response.JsonObjectResponse is JObject)
            {
                JObject jsonObject = response.JsonObjectResponse;
                T resourceObject = (T)MercadoPagoCoreUtils.GetResourceFromJson<T>(resource.GetType(), jsonObject);
                resource = (T)FillResource(resourceObject, resource);
                resource._lastKnownJson = MercadoPagoCoreUtils.GetJsonFromResource(resource);
            }

            return resource;
        }

        protected static List<T> FillArrayWithResponseData<T>(Type clazz, MercadoPagoAPIResponse response) where T : MercadoPagoBase
        {
            List<T> resourceArray = new List<T>();
            if (response.JsonObjectResponse != null)
            {
                JArray jsonArray = MercadoPagoCoreUtils.GetArrayFromJsonElement<T>(response.JsonObjectResponse);

                if (jsonArray != null)
                {
                    for (int i = 0; i < jsonArray.Count(); i++)
                    {
                        T resource = (T)MercadoPagoCoreUtils.GetResourceFromJson<T>(clazz, (JObject)jsonArray[i]);

                        resource.DumpLog();

                        resource._lastKnownJson = MercadoPagoCoreUtils.GetJsonFromResource(resource);
                        resourceArray.Add(resource);
                    }
                }
            }
            else
            {
                JArray jsonArray = MercadoPagoCoreUtils.GetJArrayFromStringResponse<T>(response.StringResponse);
                if (jsonArray != null)
                {
                    for (int i = 0; i < jsonArray.Count(); i++)
                    {
                        T resource = (T)MercadoPagoCoreUtils.GetResourceFromJson<T>(clazz, (JObject)jsonArray[i]);
                        resource._lastKnownJson = MercadoPagoCoreUtils.GetJsonFromResource(resource);
                        resourceArray.Add(resource);
                    }
                }
            }
            return resourceArray;
        }

        private static MercadoPagoBase FillResource<T>(T sourceResource, T destinationResource) where T : MercadoPagoBase
        {
            FieldInfo[] declaredFields = destinationResource.GetType().GetFields(BindingFlags.Instance |
                                                                                   BindingFlags.Static |
                                                                                   BindingFlags.NonPublic |
                                                                                   BindingFlags.Public);
            foreach (FieldInfo field in declaredFields)
            {
                try
                {
                    FieldInfo originField = sourceResource.GetType().GetField(field.Name, BindingFlags.Instance |
                                                                                   BindingFlags.Static |
                                                                                   BindingFlags.NonPublic |
                                                                                   BindingFlags.Public);
                    field.SetValue(destinationResource, originField.GetValue(sourceResource));

                }
                catch (Exception ex)
                {
                    throw new MercadoPagoException(ex.Message);
                }
            }

            return destinationResource;
        }

        public static MercadoPagoAPIResponse CallAPI(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            WebHeaderCollection colHeaders,
            bool useCache,
            int requestTimeout,
            int retries)
        {
            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            foreach (object header in colHeaders)
            {
                customHeaders.Add(header.ToString(), colHeaders[header.ToString()]);
            }

            RequestOptions requestOptions = new RequestOptions
            {
                Timeout = requestTimeout,
                Retries = retries,
                CustomHeaders = customHeaders
            };

            return CallAPI(httpMethod, path, payloadType, payload, useCache, requestOptions);
        }

        public static MercadoPagoAPIResponse CallAPI(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            bool useCache,
            RequestOptions requestOptions)
        {
            string cacheKey = httpMethod.ToString() + "_" + path;
            MercadoPagoAPIResponse response = null;

            if (requestOptions == null)
            {
                requestOptions = new RequestOptions();
            }

            if (useCache)
            {
                response = MercadoPagoCache.GetFromCache(cacheKey);

                if (response != null)
                {
                    response.IsFromCache = true;
                }
            }

            if (response == null)
            {
                response = new MercadoPagoRestClient().ExecuteRequest(
                    httpMethod,
                    path,
                    payloadType,
                    payload,
                    requestOptions);

                if (useCache)
                {
                    MercadoPagoCache.AddToCache(cacheKey, response);
                }
                else
                {
                    MercadoPagoCache.RemoveFromCache(cacheKey);
                }
            }

            return response;
        }

        private static MethodInfo GetAnnotatedMethod(Type clazz, string methodName)
        {
            foreach (MethodInfo method in clazz.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            {
                if (method.Name == methodName && method.GetCustomAttributes(false).Length > 0)
                {
                    return method;
                }
            }

            throw new MercadoPagoException("No annotated method found");
        }

        private static Dictionary<string, object> GetRestInformation(MethodInfo element)
        {
            if (element.GetCustomAttributes(false).Length == 0)
            {
                throw new MercadoPagoException("No rest method found");
            }

            Dictionary<string, object> hashAnnotation = new Dictionary<string, object>();
            foreach (Attribute annotation in element.GetCustomAttributes(false))
            {
                if (annotation is BaseEndpoint endpoint)
                {
                    if (string.IsNullOrEmpty(endpoint.Path))
                    {
                        throw new MercadoPagoException(string.Format("Path not found for {0} method", endpoint.HttpMethod.ToString()));
                    }
                }
                else
                {
                    throw new MercadoPagoException("Not supported method found");
                }

                hashAnnotation = new Dictionary<string, object>
                {
                    { "method", ((BaseEndpoint)annotation).HttpMethod },
                    { "path", ((BaseEndpoint)annotation).Path },
                    { "instance", element.ReturnType.Name },
                    { "Header", element.ReturnType.GUID },
                    { "payloadType", ((BaseEndpoint)annotation).PayloadType },
                    { "requestTimeout", ((BaseEndpoint)annotation).RequestTimeout },
                    { "retries", ((BaseEndpoint)annotation).Retries }
                };
            }

            return hashAnnotation;
        }

        public static Type GetTypeFromStack()
        {
            MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
            string className = methodBase.DeclaringType.FullName;
            Type type = Type.GetType(className);
            if (type != null) return type;
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(className);
                if (type != null)
                    return type;
            }
            return null;
        }

        public static string ParsePath<T>(string path, Dictionary<string, string> mapParams, T resource) where T : MercadoPagoBase
        {
            return ParsePath<T>(path, mapParams, resource, null);
        }

        public static string ParsePath<T>(string path, Dictionary<string, string> mapParams, T resource, RequestOptions requestOptions) where T : MercadoPagoBase
        {
            string param_pattern = @":([a-z0-9_]+)";
            MatchCollection matches = Regex.Matches(path, param_pattern);
            foreach (Match param in matches)
            {
                string param_string = param.Value.Replace(":", "");

                if (mapParams != null)
                {
                    foreach (KeyValuePair<string, string> entry in mapParams)
                    {
                        if (param_string == entry.Key)
                        {
                            path = path.Replace(param.Value, entry.Value);
                        }
                    }
                }

                if (resource != null)
                {
                    JObject json = JObject.FromObject(resource);
                    JToken resource_value = json.GetValue(ToPascalCase(param_string));
                    if (resource_value != null)
                    {
                        path = path.Replace(param.Value, resource_value.ToString());
                    }
                }
            }

            StringBuilder result = new StringBuilder();
            result.Insert(0, MercadoPagoSDK.BaseUrl);
            result.Append(path);

            if (requestOptions == null)
            {
                requestOptions = new RequestOptions();
            }

            string accessToken;
            if (!string.IsNullOrEmpty(requestOptions.AccessToken))
            {
                accessToken = requestOptions.AccessToken;
            }
            else if (resource != null && !string.IsNullOrEmpty(resource.MarketplaceAccessToken))
            {
                accessToken = resource.MarketplaceAccessToken;
            }
            else
            {
                accessToken = MercadoPagoSDK.OAuthAccessToken;
            }

            if (!string.IsNullOrEmpty(accessToken) && !path.Equals("/oauth/token", StringComparison.InvariantCultureIgnoreCase))
            {
                result.Append(string.Format("{0}{1}", "?access_token=", accessToken));
            }

            bool search = !path.Contains(':') && mapParams != null && mapParams.Any();
            if (search) //search url format, no :id type. Params after access_token
            {
                foreach (KeyValuePair<string, string> elem in mapParams)
                {
                    if (!string.IsNullOrEmpty(elem.Value))
                    {
                        result.Append(string.Format("{0}{1}={2}", "&", elem.Key, elem.Value));
                    }
                }
            }

            return result.ToString();
        }

        public void DumpLog()
        {
            _ = JsonConvert.SerializeObject(this, Formatting.Indented);
            Trace.WriteLine("Resource " + ObjectDumper.Dump(this));

        }

        public static string GetUserToken(Type classType)
        {
            string userToken = "";
            UserToken userTokenAttribute = (UserToken)Attribute.GetCustomAttribute(classType, typeof(UserToken));

            if (userTokenAttribute != null)
            {
                userToken = userTokenAttribute.UserAsignedToken;
            }

            return userToken;
        }

        public JObject GetJsonSource()
        {
            return _lastApiResponse?.JsonObjectResponse;
        }

        public static bool Validate(object o)
        {
            Type type = o.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Type attrType = typeof(ValidationAttribute);
            ValidationResult result = new ValidationResult();
            string FinalMessageError = "There are errors in the object you're trying to create. Review them to continue: ";

            foreach (PropertyInfo propertyInfo in properties)
            {
                object[] customAttributes = propertyInfo.GetCustomAttributes(attrType, inherit: true);

                foreach (object customAttribute in customAttributes)
                {
                    ValidationAttribute validationAttribute = (ValidationAttribute)customAttribute;

                    bool isValid = validationAttribute.IsValid(propertyInfo.GetValue(o, BindingFlags.GetProperty, null, null, null));

                    if (!isValid)
                    {
                        switch (validationAttribute.GetType().Name)
                        {
                            case "RangeAttribute":
                                {
                                    result.Errors.Add(new ValidationError() { Message = RangeError.Replace("#PROPERTY", propertyInfo.Name) });
                                }
                                break;
                            case "RequiredAttribute":
                                {
                                    result.Errors.Add(new ValidationError() { Message = RequiredError.Replace("#PROPERTY", propertyInfo.Name) });
                                }
                                break;
                            case "RegularExpressionAttribute":
                                {
                                    result.Errors.Add(new ValidationError() { Message = RegularExpressionError.Replace("#PROPERTY", propertyInfo.Name).Replace("#REGEXPR", ((RegularExpressionAttribute)customAttribute).Pattern) });
                                }
                                break;
                            case "DataTypeAttribute":
                                {
                                    result.Errors.Add(new ValidationError() { Message = DataTypeError.Replace("#PROPERTY", propertyInfo.Name) });
                                }
                                break;
                        }
                    }
                }
            }

            if (result.Errors.Count() != 0)
            {
                foreach (ValidationError error in result.Errors)
                {
                    FinalMessageError += error.Message;
                }

                throw new Exception(FinalMessageError);
            }

            return true;
        }

        public partial class ValidationResult
        {
            public List<ValidationError> Errors = new List<ValidationError>();
        }

        public partial class ValidationError
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }

        public static string ToPascalCase(string text)
        {
            const string pattern = @"(-|_)\w{1}|^\w";
            return Regex.Replace(text, pattern, match => match.Value.Replace("-", string.Empty).Replace("_", string.Empty).ToUpper());
        }
        public static string ToSnakeCase(string text)
        {
            const string pattern = @"(?<=[a-z0-9])[A-Z\s]";
            return Regex.Replace(text, pattern, "_$0").ToLower();
        }
    }
}
