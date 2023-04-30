using ArkDiffDisplayer;
using ArkDiffDisplayer.DiffCreator;
using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.FileManagement;
using ArkDiffDisplayer.OutputCreator;
using ArkDiffDisplayer.Parser;
using Microsoft.AspNetCore.Http;
using System.Globalization;

const string allowAllOriginsCors = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllOriginsCors,
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors(allowAllOriginsCors);


IDataManagement dataManagement = new LocalFileManagement();
IParser parser = new ParserUtils();
IDiffCreator diffCreator = new DiffCreator();
IOutputCreator outputCreator = new OutputCreator();

var diffDisplayer = new DiffDisplayer(dataManagement, parser, diffCreator, outputCreator);

app.MapGet("/latest-diff",
    IResult () =>
    {
        var lastWorkingDay = DateTime.Now;
        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
        {
            lastWorkingDay = DateTime.Now.AddDays(-1);
        } else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        {
            lastWorkingDay = DateTime.Now.AddDays(-2);
        }
        
        var lastWorkingDayString = lastWorkingDay.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo);
        
        try
        {
            var result = diffDisplayer.GetDataDiff(DateTime.Parse(lastWorkingDayString, DateTimeFormatInfo.InvariantInfo));
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
);

app.Run();
