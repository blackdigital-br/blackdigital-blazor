
using BlackDigital.Rest;

namespace BlackDigital.Blazor.Rest
{
    public class RestConfig
    {
        internal string BaseUrl { get; set; }

        internal RestThownType ThownType { get; set; } = RestThownType.OnlyBusiness;

        public RestConfig AddBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
            return this;
        }

        public RestConfig AddThownType(RestThownType thownType)
        {
            ThownType = thownType;
            return this;
        }
    }
}
