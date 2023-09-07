using Amazon.S3;
using Amazon.S3.Transfer;



namespace HealthAndMonetaryHarmony.Services
{
    

    public class S3Uploader
    {
        private readonly IAmazonS3 _s3Client;

        public S3Uploader(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task UploadFileAsync(Stream fileStream, string bucketName, string fileName)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead // Set the access level here
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
        }
    }

}
