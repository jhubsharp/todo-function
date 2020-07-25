using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace TodoFunction.Tests
{
    public class HelloFunctionTests
    {
        private readonly ILogger logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
        
        [Fact]
        public async void HttpTriggerWithoutParamsReturnsGenericMessage()
        {
            var query = new Dictionary<string, StringValues>();
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(query)
            };

            var response = (OkObjectResult)await HelloFunction.Run(request, logger);

            response.Value.ShouldBe("This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.");
        }

        [Fact]
        public async void HttpTriggerWithNameParamShouldReturnNameInResponse()
        {
            var query = new Dictionary<string, StringValues>();
            query.Add("name", "Robert");
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(query)
            };

            var response = (OkObjectResult)await HelloFunction.Run(request, logger);

            response.Value.ShouldBe("Hello, Robert. Welcome to this amazing Azure Function!");
        }
    }
}
