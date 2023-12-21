using ApiConsorcio.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ApiConsorcio.Filters;

public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public AuthorizationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
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

        HttpResponseMessage response = await client.PostAsJsonAsync(_configuration["AuthorizationApi"], token);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        var userId = await response.Content.ReadAsStringAsync();

        filterContext.HttpContext.Items["UserId"] = userId;
    }
}
