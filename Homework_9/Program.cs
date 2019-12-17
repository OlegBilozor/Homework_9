using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace Homework_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Post post = new Post(){id=101, userId = 12, title = "Homework 9", body = "Post was created by Oleg Bilozor for Homework 9"}; //create post object
            string url = @"https://jsonplaceholder.typicode.com/posts"; //initializing URL
            Console.WriteLine(CreatePost(url, post)); //calling method to create our post on site
            Console.ReadLine();
        }

        private static string CreatePost(string url, Post post)
        {
            
            var request = (HttpWebRequest)WebRequest.Create(url); //request to site
            var postData = JsonConvert.SerializeObject(post); //serialize our post to JSON format
            var data = Encoding.ASCII.GetBytes(postData); //writing bytes of our post to byte[]

            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length); //writing our post
            }

            string responseString;
            using (var response = (HttpWebResponse)request.GetResponse()) //to check if transaction was successful, write our post text in console
            {
                var stream = response.GetResponseStream();
                if (stream == null) return "Response stream is NULL!";
                responseString = new StreamReader(stream).ReadToEnd();
            }

            return responseString;
        }
    }
}
