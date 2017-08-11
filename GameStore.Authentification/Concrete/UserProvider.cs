﻿using GameStore.Authentification.Infrastructure;
using GameStore.Common.Enums;
using GameStore.DAL.Abstract.Common;
using System;
using System.Linq;
using System.Security.Principal;

namespace GameStore.Authentification.Concrete
{
	public class UserProvider : IPrincipal
	{
		private readonly UserIdentity _userIdentity;

		public IIdentity Identity => _userIdentity;

		public UserProvider()
		{
			_userIdentity = new UserIdentity();
		}

		public UserProvider(string login, IUserRepository repository)
		{
			_userIdentity = new UserIdentity {User = repository.GetSingle(u => u.Login == login)};
		}

		public bool IsInRole(string accessLevels)
		{
			var accessLevelsArray = accessLevels.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return accessLevelsArray.Any(roleName => _userIdentity.User.Roles.
				Any(role => string.Equals(Enum.GetName(typeof(AccessLevel), role.AccessLevel), roleName, StringComparison.CurrentCultureIgnoreCase)));
		}
	}
}