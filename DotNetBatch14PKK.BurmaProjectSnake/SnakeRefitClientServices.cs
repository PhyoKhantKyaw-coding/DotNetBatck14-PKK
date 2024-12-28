using Refit;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 16865145f631abc70d2ab970f01f3dfd66163d08

namespace DotNetBatch14PKK.BurmaProjectSnake
{
    internal class SnakeRefitClientServices
    {
<<<<<<< HEAD
        private readonly IsnakeApi _snakeApi;

        public SnakeRefitClientServices()
        {
            _snakeApi = RestService.For<IsnakeApi>("https://burma-project-ideas.vercel.app");
=======
        private readonly IsnakeApi _gameApi;

        public SnakeRefitClientServices()
        {
            _gameApi = RestService.For<IsnakeApi>("https://burma-project-ideas.vercel.app/");
>>>>>>> 16865145f631abc70d2ab970f01f3dfd66163d08
        }

        public async Task<List<Rootobject>> Getsnakes()
        {
<<<<<<< HEAD
            var model = await _snakeApi.GetSnakes();
=======
            var model = await _gameApi.GetSnakes();
>>>>>>> 16865145f631abc70d2ab970f01f3dfd66163d08
            return model;
        }



        public async Task<Rootobject> Getsnake(int Id)
        {
<<<<<<< HEAD
            return await _snakeApi.GetRootobject(Id);
=======
            return await _gameApi.GetRootobject(Id);
>>>>>>> 16865145f631abc70d2ab970f01f3dfd66163d08
        }
    }
    public interface IsnakeApi
    {
        [Get("/snakes")]
<<<<<<< HEAD
        Task<List<Rootobject>> GetSnakes();

        [Get("/snake/{id}")]
        Task<Rootobject> GetRootobject(int id);
=======
        Task< List<Rootobject> > GetSnakes();

        [Get("/snake/{id}")]
        Task<Rootobject> GetRootobject(int id) ;
>>>>>>> 16865145f631abc70d2ab970f01f3dfd66163d08
    }
}
