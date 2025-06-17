
using BlackDigital.Rest;

namespace BlackDigital.Blazor.Rest
{
    public class RestConfig
    {
        #region "Properties"

        internal string BaseUrl { get; set; } = "/";

        internal List<RestEventHandler> OnConnectionError { get; set; } = [];

        internal List<RestEventHandler> OnServerError { get; set; } = [];

        internal List<RestEventHandler> OnUnauthorized { get; set; } = [];

        internal List<RestEventHandler> OnForbidden { get; set; } = [];

        internal bool? RetryConnection { get; set; }

        internal int? TimeRetryConnection { get; set; }

        internal RestThownType ThrownType { get; set; } = RestThownType.OnlyBusiness;

        #endregion "Properties"

        #region "Builder"

        public RestConfig AddBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
            return this;
        }

        public RestConfig AddThrownType(RestThownType thrownType)
        {
            ThrownType = thrownType;
            return this;
        }

        public RestConfig AddOnConnectionError(RestEventHandler? connectionError)
        {
            if (connectionError != null)
                OnConnectionError.Add(connectionError);

            return this;
        }

        public RestConfig AddOnServerError(RestEventHandler? serverError)
        {
            if (serverError != null)
                OnServerError.Add(serverError);

            return this;
        }

        public RestConfig AddOnUnauthorized(RestEventHandler? unauthorized)
        {
            if (unauthorized != null)
                OnUnauthorized.Add(unauthorized);

            return this;
        }

        public RestConfig AddOnForbidden(RestEventHandler? forbidden)
        {
            if (forbidden != null)
                OnForbidden.Add(forbidden);

            return this;
        }

        public RestConfig SetRetryConnection(bool retryConnection)
        {
            RetryConnection = retryConnection;
            return this;
        }

        public RestConfig SetTimeRetryConnection(int timeRetryConnection)
        {
            TimeRetryConnection = timeRetryConnection;
            return this;
        }

        #endregion "Builder"
    }
}
