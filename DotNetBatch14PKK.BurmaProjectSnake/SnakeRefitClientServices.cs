﻿using Refit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DotNetBatch14PKK.BurmaProjectSnake
{
    internal class SnakeRefitClientServices
    {
        private readonly IsnakeApi _snakeApi;

        public SnakeRefitClientServices()
        {
            _snakeApi = RestService.For<IsnakeApi>("https://burma-project-ideas.vercel.app");

        }



        public async Task<List<Rootobject>> Getsnakes()
        {
            var model = await _snakeApi.GetSnakes();
            return model;
        }



        public async Task<Rootobject> Getsnake(int Id)
        {
            return await _snakeApi.GetRootobject(Id);
        }
    }
    public interface IsnakeApi
    {
        [Get("/snakes")]
        Task<List<Rootobject>> GetSnakes();

        [Get("/snake/{id}")]
        Task<Rootobject> GetRootobject(int id);
    }
}
