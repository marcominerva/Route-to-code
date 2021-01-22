using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using RouteToCode.Models;
using RouteToCode.Services;
using System;
using System.Web;

namespace RouteToCode.Endpoints
{
    public static class PeopleEndpoints
    {
        public static void Map(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/people", async context =>
            {
                var queryString = HttpUtility.ParseQueryString(context.Request.QueryString.ToString());

                var peopleService = context.RequestServices.GetService<IPeopleService>();
                var people = peopleService.Get();
                await context.Response.WriteAsJsonAsync(people);
            });

            endpoints.MapGet("/api/people/{id:int}", async context =>
            {
                var id = Convert.ToInt32(context.GetRouteValue("id"));

                var peopleService = context.RequestServices.GetService<IPeopleService>();
                var person = peopleService.Get(id);
                if (person != null)
                {
                    await context.Response.WriteAsJsonAsync(person);
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status404NotFound;
            });

            endpoints.MapPost("/api/people", async context =>
            {
                if (!context.Request.HasJsonContentType())
                {
                    context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                    return;
                }

                var peopleService = context.RequestServices.GetService<IPeopleService>();
                var person = await context.Request.ReadFromJsonAsync<Person>();
                peopleService.Add(person);

                context.Response.StatusCode = StatusCodes.Status201Created;
                context.Response.Headers.Add(HeaderNames.Location, "...");
            });
        }
    }
}
