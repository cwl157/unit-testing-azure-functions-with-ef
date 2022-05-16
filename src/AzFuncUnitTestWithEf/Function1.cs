using System;
using System.IO;
using System.Threading.Tasks;
using AzFuncUnitTestWithEf.DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using AzFuncUnitTestWithEf.DataContext;
using AzFuncUnitTestWithEf.Models;
using System.Web.Http;

namespace AzFuncUnitTestWithEf
{
    public class Function1
    {
        private readonly BookDataContext _bookDataContext;

        public Function1(BookDataContext ctx)
        {
            _bookDataContext = ctx;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext executionContext)
        {
            try
            {
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                var book = await ParseInput(req);

                _bookDataContext.Add(Map(book));

                await _bookDataContext.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new InternalServerErrorResult();
            }
        }

        private async Task<BookInput> ParseInput(HttpRequest req)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<BookInput>(requestBody);
        }

        private Book Map(BookInput b)
        {
            return new Book { Title = b.Title, Author = b.Author, PublishedDate = b.PublishedDate };
        }
    }
}
