using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.JsonPlaceholder
{
    internal class AlbumServiceRefit
    {
        private readonly IAlbumAPi _jsonPlaceholeder;

        public AlbumServiceRefit()
        {
            _jsonPlaceholeder = RestService.For<IAlbumAPi>("https://jsonplaceholder.typicode.com");
        }

        public async Task<List<AlbumsModel>> GetAlbums()
        {
            return await _jsonPlaceholeder.GetPosts();
        }

        public async Task<AlbumsModel> GetAlbum(int id)
        {
            return await _jsonPlaceholeder.GetPost(id);
        }

        public async Task<AlbumsModel> CreateAlbum(AlbumsModel requestModel)
        {
            return await _jsonPlaceholeder.CreatePost(requestModel);
        }

        public async Task<AlbumsModel> UpdateAlbum(int id, AlbumsModel requestModel)
        {
            return await _jsonPlaceholeder.UpdatePost(id, requestModel);
        }

        public async Task<AlbumsModel> DeleteAlbum(int id)
        {
            return await _jsonPlaceholeder.DeletePost(id);
        }
    }
}
