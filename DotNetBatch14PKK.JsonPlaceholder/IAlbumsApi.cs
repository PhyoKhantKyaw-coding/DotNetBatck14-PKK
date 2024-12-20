using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.JsonPlaceholder
{
    internal interface IAlbumsAPi
    {
        [Get("/albums")]
        public Task<List<AlbumsModel>> GetPosts();

        [Get("/albums/{id}")]
        public Task<AlbumsModel> GetPost(int id);

        [Post("/albums")]
        public Task<AlbumsModel> CreatePost(AlbumsModel requestModel);

        [Patch("/albums/{id}")]
        public Task<AlbumsModel> UpdatePost(int id, AlbumsModel requestModel);

        [Delete("/albums/{id}")]
        public Task<AlbumsModel> DeletePost(int id);
    }
}
