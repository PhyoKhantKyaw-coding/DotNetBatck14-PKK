using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.JsonPlaceholder
{
    internal class PostServiceRefit
    {
        private readonly IAlbumAPi _jsonPlaceholeder;

        public PostServiceRefit()
        {
            _jsonPlaceholeder = RestService.For<IAlbumAPi>("https://jsonplaceholder.typicode.com");
        }

        public async Task<List<AlbumsModel>> GetPosts()
        {
            return await _jsonPlaceholeder.GetPosts();
        }

        public async Task<AlbumsModel> GetPost(int id)
        {
            return await _jsonPlaceholeder.GetPost(id);
        }

        public async Task<AlbumsModel> CreatePost(AlbumsModel requestModel)
        {
            return await _jsonPlaceholeder.CreatePost(requestModel);
        }

        public async Task<AlbumsModel> UpdatePost(int id, AlbumsModel requestModel)
        {
            return await _jsonPlaceholeder.UpdatePost(id, requestModel);
        }

        public async Task<AlbumsModel> DeletePost(int id)
        {
            return await _jsonPlaceholeder.DeletePost(id);
        }
    }
}
