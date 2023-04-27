using MassTransit.Util;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace EncryptionInfra.Business.Observers
{
    public class ConsumeObserver : IConsumeObserver
    {
        private readonly ILogger<ConsumeObserver> logger;

        public ConsumeObserver(ILogger<ConsumeObserver> logger)
        {
            this.logger = logger;
        }

        Task IConsumeObserver.PreConsume<T>(ConsumeContext<T> context)
        {
            logger.LogDebug(JsonSerializer.Serialize(context.Message));
            return TaskUtil.Completed;
        }

        Task IConsumeObserver.PostConsume<T>(ConsumeContext<T> context)
        {
            return TaskUtil.Completed;
        }

        Task IConsumeObserver.ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        {
            logger.LogError(exception, $"PublishObserver - {exception.Message}");

            return TaskUtil.Completed;
        }
    }
}
