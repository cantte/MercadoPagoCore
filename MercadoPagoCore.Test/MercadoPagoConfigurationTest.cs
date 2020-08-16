using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using ConfigurationException = MercadoPagoCore.Exceptions.ConfigurationException;

namespace MercadoPagoCore.Test
{
    [TestFixture]
    public class MercadoPagoConfigurationTest
    {
        [Test]
        public void AttributesConfigurationTests()
        {
            MercadoPagoSDK.CleanConfiguration();

            // Test attribute value assignation
            MercadoPagoSDK.ClientSecret = "CLIENT_SECRET";
            MercadoPagoSDK.ClientId = "CLIENT_ID";
            MercadoPagoSDK.AccessToken = "ACCESS_TOKEN";
            MercadoPagoSDK.AppId = "APP_ID";

            Assert.AreEqual("CLIENT_SECRET", MercadoPagoSDK.ClientSecret,
                "Client Secret must be \"CLIENT_SECRET\" at this point");
            Assert.AreEqual("CLIENT_ID", MercadoPagoSDK.ClientId,
                "Client Id must be \"CLIENT_ID\" at this point");
            Assert.AreEqual("ACCESS_TOKEN", MercadoPagoSDK.AccessToken,
                "Access Token must be \"ACCESS_TOKEN\" at this point");
            Assert.AreEqual("APP_ID", MercadoPagoSDK.AppId,
                "App Id must be \"APP_ID\" at this point");
            Assert.AreEqual("https://api.mercadopago.com", MercadoPagoSDK.BaseUrl,
                "MercadoPagoBase url must be default \"https://api.mercadopago.com\" at this point");

            // Test for value locking
            ConfigurationException configurationException = null;
            try
            {
                MercadoPagoSDK.ClientSecret = "CHANGED_CLIENT_SECRET";
            }
            catch (ConfigurationException exception)
            {
                Assert.AreEqual("ClientSecret setting can not be changed.", exception.Message, "Exception must have \"clientSecret setting can not be changed\" message");
                configurationException = exception;
            }

            Assert.IsInstanceOf<ConfigurationException>(configurationException, "Exception type must be \"ConfigurationException\"");

            Assert.AreEqual("CLIENT_SECRET", MercadoPagoSDK.ClientSecret, "Client Secret must be \"CLIENT_SECRET\" at this point");

            configurationException = null;
            try
            {
                MercadoPagoSDK.ClientId = "CHANGED_CLIENT_ID";
            }
            catch (ConfigurationException exception)
            {
                Assert.AreEqual("ClientId setting can not be changed.", exception.Message, "Exception must have \"clientId setting can not be changed\" message");
                configurationException = exception;
            }

            Assert.IsInstanceOf<ConfigurationException>(configurationException, "Exception type must be \"ConfigurationException\"");

            Assert.AreEqual("CLIENT_ID", MercadoPagoSDK.ClientId,
                "Client Id must be \"CLIENT_ID\" at this point");

            configurationException = null;
            try
            {
                MercadoPagoSDK.AccessToken = "CHANGED_ACCESS_TOKEN";
            }
            catch (ConfigurationException exception)
            {
                Assert.AreEqual("AccessToken setting can not be changed.", exception.Message,
                    "Exception must have \"accessToken setting can not be changed\" message");
                configurationException = exception;
            }

            Assert.IsInstanceOf<ConfigurationException>(configurationException, "Exception type must be \"ConfigurationException\"");

            Assert.AreEqual("ACCESS_TOKEN", MercadoPagoSDK.AccessToken,
               "Access Token must be \"ACCESS_TOKEN\" at this point");

            try
            {
                MercadoPagoSDK.AppId = "CHANGED_APP_ID";
            }
            catch (ConfigurationException exception)
            {
                Assert.AreEqual("AppId setting can not be changed.", exception.Message,
                    "Exception must have \"appId setting can not be changed\" message");
                configurationException = exception;
            }

            Assert.IsInstanceOf<ConfigurationException>(configurationException, "Exception type must be \"ConfigurationException\"");

            Assert.AreEqual("APP_ID", MercadoPagoSDK.AppId,
                "App Id must be \"APP_ID\" at this point");
        }

        [Test]
        public void InvalidHashMapConfigurationTests()
        {
            MercadoPagoSDK.CleanConfiguration();

            Dictionary<string, string> hashConfiguration = null;
            Exception auxException = null;
            try
            {
                MercadoPagoSDK.SetConfiguration(hashConfiguration);
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Invalid configurationParams parameter", exception.Message, "Exception must have \"Invalid configurationParams parameter\" message");
                auxException = exception;
            }

            Assert.IsInstanceOf<ArgumentException>(auxException, "Exception type must be \"ArgumentException\"");

            hashConfiguration = new Dictionary<string, string>()
            {
                {"clientSecret", null }
            };
            auxException = null;
            try
            {
                MercadoPagoSDK.SetConfiguration(hashConfiguration);
            }
            catch (Exception exception)
            {
                auxException = exception;
            }

            Assert.IsNull(auxException, "Exception must be \"null\"");

            hashConfiguration["clientSecret"] = "CLIENT_SECRET";
            hashConfiguration["clientId"] = "";
            auxException = null;
            try
            {
                MercadoPagoSDK.SetConfiguration(hashConfiguration);
            }
            catch (Exception exception)
            {
                auxException = exception;
            }

            Assert.IsNull(auxException, "Exception must be \"null\"");

            Assert.AreEqual("CLIENT_SECRET", MercadoPagoSDK.ClientSecret, "Client Secret must be \"CLIENT_SECRET\" at this point");
        }

        [Test]
        public void AppConfigInvalidConfigurationTests()
        {
            MercadoPagoSDK.CleanConfiguration();

            Exception auxException = null;
            try
            {
                Configuration config = null;
                MercadoPagoSDK.SetConfiguration(config);
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Configuration parameter cannot be null", exception.Message, "Exception must have \"Configuration parameter cannot be null\" message");
                auxException = exception;
            }

            Assert.IsInstanceOf<ArgumentException>(auxException, "Exception type must be \"ArgumentException\"");

            auxException = null;
            try
            {
                MercadoPagoSDK.SetConfiguration(GetConfigurationByFileName("MercadoPagoConf_invalid_App"));
            }
            catch (Exception exception)
            {
                auxException = exception;
            }

            Assert.IsNull(auxException, "Exception must be \"null\"");
            Assert.IsNull(MercadoPagoSDK.ClientSecret, "Client Secret must be \"null\" at this point");
            Assert.IsNull(MercadoPagoSDK.ClientId, "Client Id must be \"null\" at this point");
            Assert.IsNull(MercadoPagoSDK.AccessToken, "Access Token must be \"null\" at this point");
            Assert.IsNull(MercadoPagoSDK.AppId, "App Id must be \"null\" at this point");
        }

        [Test]
        public void AppConfigValidConfigurationTests()
        {
            MercadoPagoSDK.CleanConfiguration();

            MercadoPagoSDK.SetConfiguration(GetConfigurationByFileName("MercadoPagoConf_valid_App.config"));
            Assert.AreEqual("CLIENT_SECRET", MercadoPagoSDK.ClientSecret, "Client Secret must be \"CLIENT_SECRET\" at this point");
            Assert.AreEqual("CLIENT_ID", MercadoPagoSDK.ClientId, "Client Id must be \"CLIENT_ID\" at this point");
            Assert.AreEqual("ACCESS_TOKEN", MercadoPagoSDK.AccessToken, "Access Token must be \"ACCESS_TOKEN\" at this point");
            Assert.AreEqual("APP_ID", MercadoPagoSDK.AppId, "App Id must be \"APP_ID\" at this point");
        }


        public Configuration GetConfigurationByFileName(string fileName)
        {
            string current_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = current_path + "/Data/" + fileName;
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configFile
            };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            return config;
        }
    }
}
