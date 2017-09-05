﻿using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IGameService
	{
		int GetCount(GameFilterDto gameFilter = null);

		GameDto GetSingleOrDefault(string language, string gameKey);

		GameDto GetSingle(string language, string gameKey);

		void Create(string language, GameDto gameDto);

		void Update(string language, GameDto gameDto);

		void Delete(string gameKey);

		IEnumerable<GameDto> GetAll(string language, GameFilterDto gameFilter = null, int? itemsToSkip = null, int? itemsToTake = null, bool allowDeleted = false);

		IEnumerable<GameDto> GetAllByCompanyName(string language, string companyName);

		IEnumerable<GameDto> GetAllByGenreName(string language, string name);

		bool Contains(string gameKey);
	}
}