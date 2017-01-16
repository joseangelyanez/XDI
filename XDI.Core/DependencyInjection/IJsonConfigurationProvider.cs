using Newtonsoft.Json.Linq;

namespace XDI.Core.DependecyInjection
{
    public interface IJsonConfigurationProvider
    {
        JObject GetJson();
    }
}