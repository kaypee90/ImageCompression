namespace TinyPngProject.Tests
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Http;
    using Moq;
    using TinyPngProject.Helpers;
    using Xunit;

    public class TinyPngHelperTests
    {

        [Fact]
        public void CompressImageTest()
        {
            var tinyPngHelper = new TinyPngHelper();
            var formFile = GetIFormFileMock();
            const string uncompressed = 
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAEnQAABJ0Ad5mH3gAAAANSURBVBhXY/h57fF/AAkwA7IFNFSkAAAAAElFTkSuQmCC";
            var uncompressedBytes = Convert.FromBase64String(uncompressed);
            
            var compressedBytes = tinyPngHelper.CompressImage(uncompressedBytes).Result;

            Assert.True(compressedBytes != null);
            Assert.True(compressedBytes.Length <=  uncompressedBytes.Length);
        }

        [Fact]
        public void GetFileSizeTest()
        {
            var tinyPngHelper = new TinyPngHelper();
            const string someScenario = 
            "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==";
            byte[] bytes = Convert.FromBase64String(someScenario);

            var fileSize = tinyPngHelper.GetFileSize(bytes);
            
            Assert.True(!String.IsNullOrWhiteSpace(fileSize));
            Assert.Equal(bytes.Length.ToString(), fileSize);
        }

        [Fact]
        public void GetFileBytesTest()
        {
            var tinyPngHelper = new TinyPngHelper();
            var formFile = GetIFormFileMock();

            var bytes = tinyPngHelper.GetFileBytes(formFile);

            Assert.True(bytes != null);
        }

        private IFormFile GetIFormFileMock()
        {
            var fileMock = new Mock<IFormFile>();
            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            return fileMock.Object;
        }
    }

}

