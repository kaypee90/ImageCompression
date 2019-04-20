namespace TinyPngProject.Helpers
{
    using System;
    using System.IO;
    using TinifyAPI;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class TinyPngHelper
    {
        private const string TinifyApiKey = "PROVIDE YOUR TINIFY API KEY HERE";
        public TinyPngHelper(){
            SetTinifyApiKey();
        }
        public async Task<byte[]> CompressImage(IFormFile formFile)
        {
            var uncompressedImage = GetFileBytes(formFile);
            var compressedBytes = await CompressImage(uncompressedImage);
            return compressedBytes;
           
        }

        public async Task<byte[]> CompressImage(byte[] uncomressedBytes)
        {
             var uncompressedFilesize = GetFileSize(uncomressedBytes);            
            Console.WriteLine($"Uncompressed Filesize: {uncompressedFilesize}");            
            var compressedImage = await Tinify.FromBuffer(uncomressedBytes).ToBuffer();     
            var compressedFilesize = GetFileSize(compressedImage);                 
            Console.WriteLine($"Compressed Filesize: {compressedFilesize}");
            return compressedImage;
        }

        public byte[] GetFileBytes(IFormFile formFile)
        {
            using (var stream = new MemoryStream()) 
            { 
                formFile.CopyTo(stream); 
                return stream.ToArray(); 
            } 
        }

        public string GetFileSize(byte[] bytesData) => bytesData.Length.ToString();

        private void SetTinifyApiKey() => Tinify.Key = TinifyApiKey;
    }
}