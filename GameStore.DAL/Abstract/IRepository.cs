﻿using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract
{
	public interface IRepository<TEntity> where TEntity : BaseEntity
	{
		IQueryable<TEntity> Get();

		TEntity Get(int id);

		void Insert(TEntity entity);

		void Delete(int id);

		void Update(TEntity entityToUpdate);
	}
}