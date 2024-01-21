using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniApi.Classes;
using MiniApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

app.MapGet("/", () => "Hello Christoffer!");

app.MapGet("/people", (DataContext context) =>
{
    var result = context.Person
        .Include(p => p.Interests)
        .Include(p => p.InterestLinks)
        .Select(p => new
        {
            p.PersonId,
            p.FirstName,
            p.LastName,
            p.PhoneNumber,
            Interests = p.Interests.Select(i => new { i.InterestId, i.Title, i.Descriptions }),
            InterestLinks = p.InterestLinks.Select(il => new { il.InterestLinkId, il.Url })
        })
        .ToList();

    return Results.Json(result);
});

app.MapGet("/person/{id}/interests", (DataContext context, int id) =>
{
    var personInterests = context.Person
        .Where(p => p.PersonId == id)
        .Include(p => p.Interests)
            .ThenInclude(i => i.InterestLinks)
        .SelectMany(p => p.Interests)
        .ToList();

    var result = personInterests.Select(interest => new
    {
        InterestId = interest.InterestId,
        Title = interest.Title,
        Descriptions = interest.Descriptions,
        InterestLinks = interest.InterestLinks.Select(link => new
        {
            InterestLinkId = link.InterestLinkId,
            Url = link.Url,
            PersonId = link.PersonId,
            InterestId = link.InterestId
        }).ToList()
    }).ToList();

    return result.Any() ? Results.Json(result) : Results.NotFound();
});

app.MapGet("/person/{id}/links", (DataContext context, int id) =>
{
    var result = context.Person
        .Where(p => p.PersonId == id)
        .SelectMany(p => p.InterestLinks.Select(il => new { PersonName = $"{p.FirstName} {p.LastName}", LinkUrl = il.Url }))
        .ToList();

    return result.Any() ? Results.Json(result) : Results.NotFound();
});

app.MapPost("/new/person", (DataContext context, Person person) =>
{
    if (person == null)
        return Results.BadRequest();

    context.Person.Add(person);
    context.SaveChanges();

    return Results.Created($"/new/person/{person.PersonId}", person);
});

app.MapPost("/person/{personId}/interests", (DataContext context, int personId, Interest interest) =>
{
    var person = context.Person.Find(personId);

    if (person == null || interest == null)
        return Results.NotFound();

    person.Interests ??= new List<Interest> { interest };
    context.SaveChanges();

    return Results.Created($"/person/{personId}/interest/{interest.InterestId}", person);
});

app.MapPost("/person/{personId}/interest/{interestId}/link", async (HttpContext httpContext, DataContext context, int personId, int interestId) =>
{
    var person = await context.Person.FindAsync(personId);
    var interest = await context.Interests.FindAsync(interestId);

    if (person == null || interest == null)
        return Results.NotFound();

    try
    {
        using var document = await JsonDocument.ParseAsync(httpContext.Request.Body);
        var linkUrl = document.RootElement.GetProperty("Url").GetString();

        if (string.IsNullOrEmpty(linkUrl))
            return Results.BadRequest("Invalid data");

        context.InterestLinks.Add(new InterestLink { PersonId = personId, InterestId = interestId, Url = linkUrl });
        await context.SaveChangesAsync();

        return Results.Json($"Interest link added {interestId} personID: {personId}.");
    }
    catch (JsonException)
    {
        return Results.BadRequest("Not Correct JSON format.");
    }
});

app.Run();