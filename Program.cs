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
            var httpResponsePost = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            var httpResponseUser = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

            if (httpResponsePost.IsSuccessStatusCode)
            {
                var contentString = await httpResponsePost.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<Post>>(contentString);

                var filteredPosts = posts.Where(post => post.Id <= 60);

                foreach (var post in filteredPosts)
                {
                    Console.WriteLine($"Post id: {post.Id}, \r\n Post title: {post.Title}.");
                }
                Console.WriteLine();
            }

            if (httpResponseUser.IsSuccessStatusCode)
            {
                var contentString = await httpResponseUser.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(contentString);

                var filteredUsers = users.Where(user => user.Id <= 50);

                foreach (var user in filteredUsers)
                {
                    Console.WriteLine($"User id: {user.Id}, \r\n Username: {user.Username}.");
                }

                Console.WriteLine();
                users.ForEach(user => Console.WriteLine($"User id: {user.Id}, \r\n Username: {user.Username}, \r\n User e-mail: {user.Email}."));
                Console.WriteLine();
                var SamanthaUser = users.Where(user => user.Username == "Samantha").Select(user => user.Username).FirstOrDefault();
                Console.WriteLine(SamanthaUser);
            }

            Console.WriteLine();
        }
    }
}
