using EncryptionInfra.Domain.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Business.Services
{
    public class RedisCacheService : ICache
    {

        private IDatabase _db;

        public RedisCacheService()
        {
            _db = RedisConnection.Connection.GetDatabase();

        }

        public T Get<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public bool Set<T>(string key, T value, TimeSpan expiryTime)
        {
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }

        public object Remove(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}
