using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Sas;
using Azure.Storage;

namespace SASProviderIS
{
    public class StorageAccountContainerSAS
    {

        public StorageAccountContainerSAS()
        {
            
        }


        [FunctionName("StorageAccountContainerSAS")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string accountName = "<storage-account-name>";
            string accountKey = "<storage-account-key>"; // TODO: Read storage-account-key from keyVault.

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var blobSasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = data?.containerName,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
                Resource = "c"
            };
            blobSasBuilder.SetPermissions(BlobSasPermissions.Write);
            blobSasBuilder.SetPermissions(BlobSasPermissions.Read);
            if (string.IsNullOrEmpty(data?.storedPolicyName))
            {
                blobSasBuilder.Identifier = data?.storedPolicyName;
            }
            var sasToken = blobSasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(accountName, accountKey)).ToString();
            log.LogInformation($"{nameof(StorageAccountContainerSAS)} TOKEN ISSUED {accountName}, {accountKey.Substring(0, 5)}, {data?.containerName}.");
            
            return new OkObjectResult(sasToken);
        }
    }
}
