namespace TinyPngProject.Helpers
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using TinifyAPI;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class TinyPngHelper
    {
        private const string TinifyApiKey = "uhMucYrcKkLoU4mfXnK6XIMwdGYIJWbv"; // "PROVIDE YOUR TINIFY API KEY HERE";
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
            Console.WriteLine($"TINYPNG Uncompressed Filesize: {uncompressedFilesize}");            
            var compressedImage = await Tinify.FromBuffer(uncomressedBytes).ToBuffer();     
            var compressedFilesize = GetFileSize(compressedImage);                 
            Console.WriteLine($"TINYPNG Compressed Filesize: {compressedFilesize}");
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

        public byte[] GzipImage(IFormFile formFile)
        {
            var uncompressedImage = GetFileBytes(formFile);
            var compressedBytes =  GzipImage(uncompressedImage);
            return compressedBytes;
        }

        public byte[] GzipImage(byte[] data)
        {
            var uncompressedFilesize = GetFileSize(data);
            Console.WriteLine($"GZIP Uncompressed Filesize: {uncompressedFilesize}");    
            using(MemoryStream comp = new MemoryStream())
            {
                using(GZipStream gzip = new GZipStream(comp, CompressionLevel.Optimal))
                {
                    gzip.Write(data, 0, data.Length);
                }
                data = comp.ToArray();
                var compressedFilesize = GetFileSize(data);                 
                Console.WriteLine($"GZIP: Compressed Filesize: {compressedFilesize}");
                return data;
            }
        }

        public string GetFileSize(byte[] bytesData) => bytesData.Length.ToString();

        private void SetTinifyApiKey() => Tinify.Key = TinifyApiKey;
    }
}