using EncryptionInfra.Domain.Interfaces;
using EncryptionInfra.Domain.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public IDistributedCache _redisCache;
        public readonly bool _isKeyStored;

        public AuthenticationService(IDistributedCache redisCache)
        {
            _redisCache = redisCache;

            if (!_isKeyStored)
                _isKeyStored = ApiKeyPassStore();
        }

        public async Task<AuthResponse> Login(AuthRequest req)
        {
            var response = new AuthResponse();

            var apiPassword = await _redisCache.GetStringAsync(req.Key);

            if (apiPassword == null || req.Pass != apiPassword)
            {
                response.Message = "Invalid key or password";
            }
            else if (req.Pass == apiPassword)
            { 
                var token = Guid.NewGuid().ToString();
                response.Token = EncryptionService.Encrypt(token);

                await _redisCache.SetStringAsync("auth-token", token, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) });
            }

            return response;
        }

        public bool ApiKeyPassStore()
        {
            var apiKey = "key12432";
            _redisCache.SetString(apiKey, "4321pass");
            var apiPass = _redisCache.GetString(apiKey);

            return apiPass != null;
        }
    }
}
