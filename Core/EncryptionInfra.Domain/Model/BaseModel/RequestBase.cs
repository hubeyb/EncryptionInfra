using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Domain.Model.BaseModel
{
    public abstract class RequestBase
    {
        public string Token { get; set; }
    }
}
