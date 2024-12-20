using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.JsonPlaceholder
{
    internal interface IAlbumAPi
    {
        [Get("/posts")]
        public Task<List<AlbumsModel>> GetPosts();

        [Get("/posts/{id}")]
        public Task<AlbumsModel> GetPost(int id);

        [Post("/posts")]
        public Task<AlbumsModel> CreatePost(AlbumsModel requestModel);

        [Patch("/posts/{id}")]
        public Task<AlbumsModel> UpdatePost(int id, AlbumsModel requestModel);

        [Delete("/posts/{id}")]
        public Task<AlbumsModel> DeletePost(int id);
    }
}
