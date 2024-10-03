using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace BlackDigital.Blazor
{
    public class NavigationHistory
    {
        public NavigationHistory(NavigationManager navigation,
                                 IJSRuntime jsRuntime)
        {
            History = new();
            Navigation = navigation;
            JSRuntime = jsRuntime;
            Navigation.LocationChanged += LocationChanged;
        }


        public readonly NavigationManager Navigation;

        public readonly IJSRuntime JSRuntime;

        public Queue<string> History { get; private set; }

        private void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            Uri uri = new(e.Location);
            History.Enqueue(uri.LocalPath);

            while (History.Count > 20)
                History.Dequeue();
        }

        public bool HasInHistory(string url = null)
        {
            if (string.IsNullOrEmpty(url))
                url = Navigation.Uri;

            Uri uri = new(url);

            var compareHistory = History.Take(History.Count - 1).ToList();
            return compareHistory.Contains(uri.LocalPath);
        }

        public string GetQueryString(string key, string defaultValue = null)
        {
            try
            {
                var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
                return uri.GetQueryString()[key];
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public int? GetQueryInt32(string key, int? defaultValue = null)
        {
            try
            {
                var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
                return Convert.ToInt32(uri.GetQueryString()[key]);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public DateTime? GetQueryDateTime(string key, DateTime? defaultValue = null)
        {
            try
            {
                var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
                if (DateTime.TryParse(uri.GetQueryString()[key], out DateTime dateTime))
                    return dateTime;

                return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public async void BackHistory()
        {
            await JSRuntime.InvokeVoidAsync("history.go", -1);
        }
    }
}