﻿using ApiConsorcio.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ApiConsorcio.Filters;

public class AuthorizationFilterUserCompany : Attribute, IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthorizationFilterUserCompany(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
    {
        var token = new Token();

        token.Key = filterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token.Key == null)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        var client = _httpClientFactory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync(_configuration.GetSection("AuthorizationApi").Value, token);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        var responseJson = await response.Content.ReadAsStringAsync();

        User user = JsonSerializer.Deserialize<User>(responseJson);

        if (user.Role != "Company" && user.Role != "Admin")
        {
            filterContext.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }

        filterContext.HttpContext.Items["UserId"] = user.UserId;
        filterContext.HttpContext.Items["UserCompanyName"] = user.Company;
    }
}
