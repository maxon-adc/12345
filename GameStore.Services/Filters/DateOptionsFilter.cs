﻿
using GameStore.Common.Enums;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class DateOptionsFilter : IFilter<IQueryable<Game>>
	{
		private readonly DateOptions _dateOption;

		public DateOptionsFilter(DateOptions dateOption)
		{
			_dateOption = dateOption;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			var date = DateTime.UtcNow;

			switch (_dateOption)
			{
				case DateOptions.LastWeek:
					date = date.AddDays(-7);
					break;
				case DateOptions.LastMonth:
					date = date.AddDays(-30);
					break;
				case DateOptions.LastYear:
					date = date.AddDays(-365);
					break;
				case DateOptions.TwoYears:
					date = date.AddDays(-730);
					break;
				case DateOptions.ThreeYears:
					date = date.AddDays(-1095);
					break;
			}

			return input.Where(g => date <= g.DatePublished);
		}
	}
}