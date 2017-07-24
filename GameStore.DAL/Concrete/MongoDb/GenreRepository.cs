﻿using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using System.Linq;

namespace GameStore.DAL.Concrete.MongoDb
{
	public class GenreRepository : IGenreRepository
	{
		private readonly IMongoCollection<Genre> _collection;

		public GenreRepository(IMongoDatabase database)
		{
			_collection = database.GetCollection<Genre>("categories");
		}

		public IQueryable<Genre> Get()
		{
			return _collection.AsQueryable();
		}

		public Genre Get(string gameId)
		{
			var entity = _collection.AsQueryable().First(g => g.Id.ToString() == gameId);

			return entity;
		}
	}
}
