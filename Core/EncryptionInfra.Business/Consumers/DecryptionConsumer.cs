using EncryptionInfra.Business.Services;
using EncryptionInfra.Domain.Model;
using EncryptionInfra.Domain.Model.ResponseModel;
using MassTransit;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Business.Consumers
{
    public class DecryptionConsumer : IConsumer<DecryptionText>
    {
        public async Task Consume(ConsumeContext<DecryptionText> consumeContext)
        {
            var responseText = EncryptionService.Decrypt(consumeContext.Message.Text);
            var response = new MessageQueueResponse() { Text = responseText };

            await consumeContext.RespondAsync(response).ConfigureAwait(false);
        }
    }
}
