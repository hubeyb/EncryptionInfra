using EncryptionInfra.Domain.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Domain.Model
{
    public class AuthRequest
    {
        public string Key { get; set; }

        public string Pass { get; set; }
    }
}
