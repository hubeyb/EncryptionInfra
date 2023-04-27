using EncryptionInfra.Domain.Interfaces;
using EncryptionInfra.Domain.Model.ResponseModel;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Business
{
    public class MessageQueueProducer<TMessage> : IMessageQueueProducer<TMessage>
    {
        private readonly IBus bus;

        public MessageQueueProducer(IBus bus)
        {
            this.bus = bus;
        }

        public async Task<MessageQueueResponse> SendEncryptAsync(TMessage? request)
        {
            if (request == null)
                throw new Exception("Request can not be null");

            var req = bus.CreateRequestClient<string>().Create(request);
            var result = await req.GetResponse<MessageQueueResponse>();
            return result.Message;
        }

        public async Task<MessageQueueResponse> SendDecryptAsync(TMessage? request)
        {
            if (request == null)
                throw new Exception("Request can not be null");

            var req = bus.CreateRequestClient<string>().Create(request);
            var result = await req.GetResponse<MessageQueueResponse>();
            return result.Message;
        }
    }
}
