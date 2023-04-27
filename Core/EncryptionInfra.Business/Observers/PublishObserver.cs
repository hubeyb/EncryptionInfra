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
    public class PublishObserver : IPublishObserver
    {
        public Task PrePublish<T>(PublishContext<T> context)
            where T : class
        {
            return TaskUtil.Completed;
        }

        public Task PostPublish<T>(PublishContext<T> context)
            where T : class
        {
            return TaskUtil.Completed;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception)
            where T : class
        {
            return TaskUtil.Completed;
        }
    }
}
