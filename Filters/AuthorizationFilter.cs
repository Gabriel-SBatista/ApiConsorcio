using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ApiConsorcio.Filters;

public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
    {
        var token = filterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        string dataToSend = "{\"key\": \"" + token + "\"}";

        HttpContent content = new StringContent(dataToSend, Encoding.UTF8, "application/json");

        var client = new HttpClient();

        HttpResponseMessage response = await client.PostAsync("https://localhost:7006/usuarios/validacao", content);      

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        filterContext.HttpContext.Items["UserId"] = response.Content.ToString();
    }
}
