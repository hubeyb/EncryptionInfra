using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Domain.Model.ResponseModel
{
    public class MessageQueueResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Text { get; set; }
    }
}
