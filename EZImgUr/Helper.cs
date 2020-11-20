using System;

namespace EZImgUr
{
    public class Helper
    {
        public static string ImageToBase64(string filePath)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath));
        }

        public static UploadImageResult UploadImage(string clientId, string localPath)
        {

            var client = new RestSharp.RestClient("https://api.imgur.com/3/image");
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", ImageToBase64(localPath));
            RestSharp.IRestResponse response = client.Execute(request);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<UploadImageResult>(response.Content);

        }

        public static CreateAlbumResult CreateAlbum(string clientId, string albumTitle, string albumDesc, string imageIdForCover = "")
        {
            var client = new RestSharp.RestClient("https://api.imgur.com/3/album");
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("title", albumTitle);
            request.AddParameter("description", albumDesc);
            request.AddParameter("cover", imageIdForCover);
            RestSharp.IRestResponse response = client.Execute(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAlbumResult>(response.Content);
        }

        public static AlbumBasicResult AddImageToAlbum(string cliendId, string[] imageDeleteHash, string albumDeletehash)
        {
            var client = new RestSharp.RestClient("https://api.imgur.com/3/album/" + albumDeletehash);
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddHeader("Authorization", "Client-ID " + cliendId);
            request.AlwaysMultipartFormData = true;
            foreach (var id in imageDeleteHash)
            {
                request.AddParameter("deletehashes[]", id);
            }
            RestSharp.IRestResponse response = client.Execute(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AlbumBasicResult>(response.Content);
        }

        public static AlbumBasicResult DeleteImage(string clientId, string imageDeleteHash)
        {
            var client = new RestSharp.RestClient("https://api.imgur.com/3/image/" + imageDeleteHash);
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.DELETE);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            RestSharp.IRestResponse response = client.Execute(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AlbumBasicResult>(response.Content);
        }

        public static GetImageInfoFromAlbumResult GetImageInfoFromAlbum(string clientId, string albumId)
        {
            var client = new RestSharp.RestClient("https://api.imgur.com/3/album/" + albumId + "/images");
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.GET);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            RestSharp.IRestResponse response = client.Execute(request);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<GetImageInfoFromAlbumResult>(response.Content);
            }
            catch
            {
                return new GetImageInfoFromAlbumResult();
            }
        }


        public static AlbumInfoResult GetAlbumInfo(string clientId, string albumHash)
        {

            var client = new RestSharp.RestClient("https://api.imgur.com/3/album/" + albumHash);
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.GET);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            RestSharp.IRestResponse response = client.Execute(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AlbumInfoResult>(response.Content);
        }
    }
}
