using Microservices.Comments.Domain.DTOs;
using Microservices.Comments.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Microservices.Comments.Infrastructure
{
    public class PostResolver : IPostResolver
    {
        private readonly HttpClient _httpClient;
        private const string _postsUrlPart = "api/posts";

        public PostResolver(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostDto> GetPostByIdAsync(Guid postId)
        {
            var response = await _httpClient.GetAsync($"{_postsUrlPart}/{postId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Post with ID {postId} not found.");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<PostDto>(responseString);

            if (post == null)
            {
                throw new InvalidOperationException($"Failed to deserialize post with ID {postId}.");
            }

            return post;
        }

        public async Task<bool> PostExistsAsync(Guid postId)
        {
            var response = await _httpClient.GetAsync($"{_postsUrlPart}/{postId}");
            return response.IsSuccessStatusCode;
        }
    }


}
