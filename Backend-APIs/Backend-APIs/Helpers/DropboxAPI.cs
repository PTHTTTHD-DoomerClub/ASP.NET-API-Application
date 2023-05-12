using Dropbox.Api;
using Dropbox.Api.Files;

namespace Backend_APIs.Helpers
{
    public class DropboxAPI
    {
        public static string accessToken { get; set; }
        public static async Task<string> UploadFileAsync(string folder, string fileName, string fileUri)
        {
            using (var dbx = new DropboxClient(accessToken))
            {
                var fileContent = new FileStream(fileUri, FileMode.Open, FileAccess.Read);
                var uploadResult = await dbx.Files.UploadAsync("/" + folder + "/" + fileName,
                WriteMode.Overwrite.Instance,
                body: fileContent);

                // Get the URL of the uploaded file
                var result = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(uploadResult.PathDisplay);

                return result.Url;
            }
        }
    }
}
