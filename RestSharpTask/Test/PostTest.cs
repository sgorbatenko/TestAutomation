using RestSharp;
using RestSharpTask.Source.Model;
using System.Net;


namespace RestSharpTask.Test
{
    public class PostTest
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com/");

        [TestCase("0", HttpStatusCode.NotFound)]
        [TestCase("1", HttpStatusCode.OK)]
        [TestCase("abc", HttpStatusCode.BadRequest)]
        public async Task TestGetPost(string id, HttpStatusCode expectedStatusCode)
        {
            var request = new RestRequest("posts/" + id);
            var response = await client.GetAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [TestCase("123", HttpStatusCode.BadRequest)]
        [TestCase("{123}", HttpStatusCode.BadRequest)]
        [TestCase("{\"title\": \"foo\", \"body\": \"bar\"}", HttpStatusCode.BadRequest)]
        [TestCase("{\"title\": \"foo\", \"body\": \"bar\", \"userId\": 11}", HttpStatusCode.BadRequest)]
        [TestCase("{}", HttpStatusCode.BadRequest)]
        [TestCase("{\"id\": 101}", HttpStatusCode.BadRequest)]
        public async Task TestPostBadReuest(string body, HttpStatusCode expectedStatusCode)
        {
            RestRequest request = new RestRequest("posts");

            request.AddJsonBody(body);

            var response = await client.PostAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Test]
        public async Task TestPostSuccessfull()
        {

            var myPost = new Post
            {
                title = "my test title",
                body = "some test body",
                userId = 88888
            };

            RestRequest postRequest = new RestRequest("posts");

            postRequest.AddJsonBody(myPost);

            var postResponse = await client.PostAsync(postRequest);
            var deserial = System.Text.Json.JsonSerializer.Deserialize<Post>(postResponse.Content);
            Assert.That(deserial.title, Is.EqualTo(myPost.title));
            Assert.That(deserial.body, Is.EqualTo(myPost.body));
            Assert.That(deserial.userId, Is.EqualTo(myPost.userId));

            RestRequest getRequest = new RestRequest("posts/" + deserial.id);
            var getRsponse = await client.GetAsync(getRequest);
            Assert.That(getRsponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var deserialGetResponse = System.Text.Json.JsonSerializer.Deserialize<Post>(getRsponse.Content);
            Assert.That(deserialGetResponse.title, Is.EqualTo(myPost.title));
            Assert.That(deserialGetResponse.body, Is.EqualTo(myPost.body));
            Assert.That(deserialGetResponse.userId, Is.EqualTo(myPost.userId));
            Assert.That(deserialGetResponse.id, Is.EqualTo(deserial.id));
        }
    }
}