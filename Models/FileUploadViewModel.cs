namespace TinyPngProject.Models
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class FileUploadViewModel
    {
        public IFormFile FormFile{get;set;}
    }
}