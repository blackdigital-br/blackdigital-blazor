using Microsoft.JSInterop;
using System.Text.Json;

namespace BlackDigital.Blazor
{
    public static class IJSRuntimeExtensions
    {
        public static ValueTask SetInLocalStorage(this IJSRuntime js, string key, string content)
             => js.InvokeVoidAsync("localStorage.setItem", key, content);

        public static async Task SetInLocalStorage<T>(this IJSRuntime js, string key, T content)
        {
            var jsonString = content?.ToJson() ?? string.Empty;
            await SetInLocalStorage(js, key, jsonString);
        }

        public static async ValueTask<string?> GetFromLocalStorage(this IJSRuntime js, string key, string? defaultValue = null)
        {
            var value = await js.InvokeAsync<string>("localStorage.getItem", key);

            if (value == null)
                return defaultValue;

            return value;
        }

        public static async Task<T?> GetFromLocalStorage<T>(this IJSRuntime js, string key, T? defaultValue = default)
        {
            var value = await GetFromLocalStorage(js, key);

            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            return value.To<T>();
        }

        public static ValueTask RemoveItem(this IJSRuntime js, string key)
            => js.InvokeVoidAsync("localStorage.removeItem", key);

        public static ValueTask ClearStorage(this IJSRuntime js)
            => js.InvokeVoidAsync("localStorage.clear");

        public static ValueTask<object> SetDocumentTitle(this IJSRuntime js, string title)
            => js.InvokeAsync<object>("eval", $"document.title = \"{title.Replace("\"", "'")}\"");
    }
}