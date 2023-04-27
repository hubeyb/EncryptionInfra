using MassTransit.Util;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Business.Observers
{
    public class SendObserver : ISendObserver
    {
        public Task PreSend<T>(SendContext<T> context)
            where T : class
        {
            return TaskUtil.Completed;
        }

        public Task PostSend<T>(SendContext<T> context)
            where T : class
        {
            return TaskUtil.Completed;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception)
            where T : class
        {
            return TaskUtil.Completed;
        }
    }
}
