using AzFuncUnitTestWithEf.DataContext;
using AzFuncUnitTestWithEf.DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzFuncUnitTestWithEf.Tests
{
    public class FunctionTests
    {
        private static InMemoryDatabaseRoot _root = new InMemoryDatabaseRoot();
        private BookDataContext _bookContext;

        [SetUp]
        public void Setup()
        {
            var dbBuilder = new DbContextOptionsBuilder<BookDataContext>();
            dbBuilder.UseInMemoryDatabase(databaseName: "Book", _root);
            _bookContext = new BookDataContext(dbBuilder.Options);
        }

        [Test]
        public async Task ReturnsSuccess()
        {
            // Arrange
            // Make sure database is empty when starting
            _bookContext.Database.EnsureDeleted();
            // Arrange function dependencies
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger("DebugLogger");
            var executionContext = new Microsoft.Azure.WebJobs.ExecutionContext();
            var id = Guid.NewGuid();
            executionContext.InvocationId = id;
            // Arrange request data
            string input = "{\"Title\": \"Test Title\", \"Author\": \"Test Author\", \"PublishedDate\": \"2022-04-08T14:47:46Z\"}";
            using (var requestStream = new MemoryStream())
            {
                requestStream.Write(Encoding.ASCII.GetBytes(input));
                requestStream.Flush();
                requestStream.Position = 0;

                var requestMock = new Mock<HttpRequest>();
                requestMock.Setup(x => x.Body).Returns(requestStream);

                // Act
                Function1 sut = new Function1(_bookContext);
                var result = await sut.Run(requestMock.Object, logger, executionContext);

                Book resultData = await _bookContext.Books.FirstOrDefaultAsync(b => b.Title == "Test Title");

                //Assert
                Assert.AreEqual(typeof(NoContentResult), result.GetType());
                Assert.IsNotNull(resultData);
            }
        }
    }
}