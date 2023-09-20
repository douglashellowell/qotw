using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Questions.Models;

namespace Questions;

public class Responses
{
    private readonly MongoClient _mongoClient;
    private readonly ILogger _logger;

    private readonly IMongoCollection<QuestionDto> _questions;

    public Responses(ILoggerFactory loggerFactory, MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        _logger = loggerFactory.CreateLogger<Questions>();

        var database = _mongoClient.GetDatabase("qotw");
        _questions= database.GetCollection<QuestionDto>("questions");
    }
    [Function(nameof(AddResponse))]
    public async Task<HttpResponseData> AddResponse([HttpTrigger(AuthorizationLevel.Anonymous, 
            "post",
            Route = "addresponse")] 
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("Responses");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var content = await req.ReadFromJsonAsync<ResponseDto>();
        try
        {
            content.CreatedDate = DateTime.Now;
            _questions.UpdateOne(
                x => x.Id == content.QuestionId, 
                Builders<QuestionDto>.Update.Push(x => x.Responses, content)
                );
        }
        catch (Exception e)
        {
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
}