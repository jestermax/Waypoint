﻿using System;

using Domain.Models;

namespace Domain.Repositories
{
    public class ApiTokenRepository : IApiTokenRepository
    {
        public ApiToken Get(string id)
        {
            throw new NotImplementedException();
        }

        public ApiToken[] Where(Func<ApiToken, bool> filter)
        {
            throw new NotImplementedException();
        }

        public ApiToken Add(ApiToken apiToken)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, ApiToken apiToken)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
