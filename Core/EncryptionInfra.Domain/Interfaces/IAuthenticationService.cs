using EncryptionInfra.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> Login(AuthRequest req);
    }
}
