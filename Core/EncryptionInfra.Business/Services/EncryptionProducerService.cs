using EncryptionInfra.Domain.Interfaces;
using EncryptionInfra.Domain.Model;
using EncryptionInfra.Domain.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Business.Services
{
    public class EncryptionProducerService
    {
        private readonly IMessageQueueProducer<EncryptionText> encryptionProducer;
        private readonly IMessageQueueProducer<DecryptionText> decryptionProducer;

        public EncryptionProducerService(IMessageQueueProducer<EncryptionText> encryptionProducer, IMessageQueueProducer<DecryptionText> decryptionProducer)
        {
            this.encryptionProducer = encryptionProducer;
            this.decryptionProducer = decryptionProducer;
        }

        public async Task<MessageQueueResponse> SendEncrypt(EncryptionText encryptionText)
        {
            return await encryptionProducer.SendEncryptAsync(encryptionText);
        }

        public async Task<MessageQueueResponse> SendDecrypt(DecryptionText decryptionText)
        {
            return await decryptionProducer.SendDecryptAsync(decryptionText);
        }
    }
}
