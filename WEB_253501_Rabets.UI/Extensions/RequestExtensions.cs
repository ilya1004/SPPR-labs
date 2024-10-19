using Microsoft.AspNetCore.Http.Features;

namespace WEB_253501_Rabets.UI.Extensions;

public static class RequestExtensions
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        return request.HttpContext.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO;
    }
}
