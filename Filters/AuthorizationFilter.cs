using ApiConsorcio.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ApiConsorcio.Filters;

public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _role;

    public AuthorizationFilter(string role)
    {
        _role = role;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
    {     
        var token = new Token();

        token.Key = filterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token.Key == null)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        var client = new HttpClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7006/validacao", token);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        var responseJson = await response.Content.ReadAsStringAsync();

        User user = JsonConvert.DeserializeObject<User>(responseJson);

        if (user.Role != _role)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        filterContext.HttpContext.Items["UserId"] = user.UserId;
        filterContext.HttpContext.Items["UserCompany"] = user.Company;
    }
}
