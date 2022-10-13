using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.Sockets;

namespace Projeto_Api.Services
{
    public class Aws3Services : IAws3Services
    {
        public Task<GetObjectResponse> getFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFile(IFormFile formFile, string identity)
        {
            var resposta = "asd"; 
            IFormFile file = formFile;
            // check for the right content type
            if (!file.ContentType.Contains("pdf"))
            {
                return null;
            }

            // connecting to the client
            var client = new AmazonS3Client("AKIAW475BF2JBHIW5SUB", "U8IkUNnf8lJefS0P8UjiAZEn0mRzYnKupom6GgYf", Amazon.RegionEndpoint.SAEast1);

            // get the file and convert it to the byte[]
            byte[] fileBytes = new Byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

            var fileName = $"{identity}_{DateTime.Now.Year}_{DateTime.Now.Month}_{file.FileName}";
            var bucketName = "lojainterativadocumentos";
            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType
                };
                // specify no ACL, else upload will be blocked by S3!
                response = await client.PutObjectAsync(request);
            }
;

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                // this model is up to you, i have saved a reference to my database 
                // to connect the images with users etc.
                // adds the image reference
                resposta = fileName;
            }
            else
            {
                // do some error handling
            }

            return resposta;
        }
    }

    public interface IAws3Services
    {
        Task<string> UploadFile(IFormFile formFile, string identity);
        Task<GetObjectResponse> getFile(string fileName);
    }
}