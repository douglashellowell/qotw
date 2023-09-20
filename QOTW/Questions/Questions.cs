using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Questions.Models;

namespace Questions;

public class Questions
{
    private readonly MongoClient _mongoClient;
    private readonly ILogger _logger;

    private readonly IMongoCollection<QuestionDto> _questions;

    public Questions(ILoggerFactory loggerFactory, MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _logger = loggerFactory.CreateLogger<Questions>();

        var database = _mongoClient.GetDatabase("qotw");
        _questions = database.GetCollection<QuestionDto>("questions");
    }

    [Function(nameof(AddQuestion))]
    public async Task<HttpResponseData> AddQuestion(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "addquestion")]
        HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var content = await req.ReadFromJsonAsync<QuestionDto>();

        if (content is null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        try
        {
            content.CreatedDate = DateTime.Now;
            var recordExists = await _questions.FindAsync(x => x.StartDate == content.StartDate);
            if (recordExists.ToList().Count == 0)
            {
                _questions.InsertOne(content);
                return req.CreateResponse(HttpStatusCode.OK);
            }

            return req.CreateResponse(HttpStatusCode.Conflict);
        }
        catch (Exception e)
        {
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }

    [Function(nameof(GetQuestions))]
    public async Task<HttpResponseData> GetQuestions(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getquestions")]
        HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        try
        {
            var comments = await _questions.FindAsync(c => true);
            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(comments.ToList());
            return response;
        }
        catch (Exception e)
        {
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }

    [Function(nameof(GetCurrentQuestion))]
    public async Task<HttpResponseData> GetCurrentQuestion(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "question/current")]
        HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        try
        {
            var comments = await _questions.FindAsync(c => c.EndDate > DateTime.Now && c.StartDate < DateTime.Now);
            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(comments.First());
            return response;
        }
        catch (Exception e)
        {
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}