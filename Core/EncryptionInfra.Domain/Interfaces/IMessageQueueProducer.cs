using EncryptionInfra.Domain.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Domain.Interfaces
{
    public interface IMessageQueueProducer<TMessage>
    {
        Task<MessageQueueResponse> SendEncryptAsync(TMessage message);
        Task<MessageQueueResponse> SendDecryptAsync(TMessage message);
    }
}