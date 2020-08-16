using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercadoPagoCore.Test
{
    public static class LaunchSettingsFixture
    {
        public static void LoadLaunchSettings()
        {
            using StreamReader file = File.OpenText("Properties\\launchSettings.json");
            JsonTextReader reader = new JsonTextReader(file);
            JObject jObject = JObject.Load(reader);

            System.Collections.Generic.List<JProperty> variables = jObject
                .GetValue("profiles")
                //select a proper profile here
                .SelectMany(profiles => profiles.Children())
                .SelectMany(profile => profile.Children<JProperty>())
                .Where(prop => prop.Name == "environmentVariables")
                .SelectMany(prop => prop.Value.Children<JProperty>())
                .ToList();

            foreach (JProperty variable in variables)
                if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(variable.Name)))
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
        }
    }
}
