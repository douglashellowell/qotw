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

        private readonly IMongoCollection<Post> _posts;

        public Questions(ILoggerFactory loggerFactory, MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _logger = loggerFactory.CreateLogger<Questions>();

            var database = _mongoClient.GetDatabase("TestThings");
            _posts = database.GetCollection<Post>("FirstTry");
        }

        [Function(nameof(SetComments))]
        public async Task<HttpResponseData> SetComments(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "set")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

           
            var post = new Post()
            {
                PostAuthor = "Me!",
                PostTitle = "What sort of question would?",
            };
            try
            {
                _posts.InsertOne(post);
            }
            catch (Exception e)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(post);
            return response;
        }
        
        [Function(nameof(GetComments))]
        public async Task<HttpResponseData> GetComments(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var comments = await _posts.FindAsync(c => true);
                var response = req.CreateResponse(HttpStatusCode.OK);

                await response.WriteAsJsonAsync(comments.ToList());
                return response;
            }
            catch (Exception e)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }

           
        }
    [Function("Questions")]
    public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("Questions");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
}