using EncryptionInfra.Business.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace EncryptionInfra.Encryption.Attributes
{
    public class TokenAuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           ValidateToken(context);
        }

        private void ValidateToken(ActionExecutingContext context)
        {
            var encryptedToken = context.HttpContext.Request.Headers["token"].ToString();

            if (string.IsNullOrWhiteSpace(encryptedToken))
                context.Result = new UnauthorizedResult();
            else
            {
                var redisCache = context.HttpContext.RequestServices.GetService<IDistributedCache>();
                var decryptedToken = EncryptionService.Decrypt(encryptedToken);

                var token = redisCache?.GetString("auth-token");

                if (token == null || token != decryptedToken)
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}
