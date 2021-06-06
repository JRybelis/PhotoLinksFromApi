using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhotoLinksFromApi.Models;

namespace PhotoLinksFromApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var httpResponseUser = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            var httpResponseAlbum = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");
            var httpResponsePhoto = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/photos");

            int dennisSchulistUserId = 0;
            IEnumerable<int> dennisSchulistAlbumsIds = new int[] { };

            if (httpResponseUser.IsSuccessStatusCode)
            {
                var contentString = await httpResponseUser.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(contentString);

                dennisSchulistUserId = users.Where(user => user.Name == "Mrs. Dennis Schulist")
                    .Select(user => user.Id)
                    .FirstOrDefault();
                Console.WriteLine("Mrs. Dennis Schulist user id is: " + dennisSchulistUserId);
            }

            Console.WriteLine();
            if (httpResponseAlbum.IsSuccessStatusCode)
            {
                var contentString = await httpResponseAlbum.Content.ReadAsStringAsync();
                var albums = JsonConvert.DeserializeObject<List<Album>>(contentString);

                dennisSchulistAlbumsIds = albums.Where(album => album.UserId == dennisSchulistUserId)
                    .Select(album => album.Id);
                Console.Write("The id numbers of photo albums by Mrs. Dennis Schulist are: ");
                foreach (var dennisSchulistAlbumId in dennisSchulistAlbumsIds)
                {
                    Console.Write(dennisSchulistAlbumId + " ");
                }

                Console.WriteLine();
            }

            if (httpResponsePhoto.IsSuccessStatusCode)
            {
                var contentString = await httpResponsePhoto.Content.ReadAsStringAsync();
                var photos = JsonConvert.DeserializeObject<List<Photo>>(contentString);

                var dennisSchulistPhotosUrls = photos.Where(photo => dennisSchulistAlbumsIds.Contains(photo.AlbumId))
                    .Select(photo => photo.Url);

                Console.WriteLine("The urls of photos by Mrs. Dennis Schulist in all her albums are:");
                foreach (var dennisSchulistPhotoUrl in dennisSchulistPhotosUrls)
                {
                    Console.WriteLine(dennisSchulistPhotoUrl);
                }
            }
        }
    }
}