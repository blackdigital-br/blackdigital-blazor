using BlackDigital.Rest;
using System.Text.Json;

namespace Example.Service
{
    [Service("books")]
    public interface IOpenLibrary
    {
        [Action("{key}.json")]
        Task<Book> GetBook([FromRoute] string key);
    }
}
