using BlackDigital.Blazor.IndexedDB;
using Example.Models;
using Microsoft.JSInterop;

namespace Example.Service
{
    public class TestIndexedContext : IndexedContext
    {
        public TestIndexedContext(IJSRuntime jsRuntime) : base(jsRuntime)
        {
        }

        public IndexedSet<MyModel> Models { get; set; }
    }
}
