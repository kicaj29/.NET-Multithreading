using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace ExceptionHandlingInTasks.AspNetCore
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HowToThrowWithoutAwait
    {
        RunNestedNoAsyncWithReturn,
        RunNestedNoAsyncNoReturn,
        RunNestedAsyncWithReturn,
        RunNestedAsyncNoReturn,
        RunSingle,

        StartNestedNoAsyncWithReturnNoUnwrap,
        StartNestedNoAsyncWithReturnUnwrap,
        StartNestedAsyncWithReturnUnwrap,
        StartNestedAsyncNoReturnUnwrap,
    }
}
