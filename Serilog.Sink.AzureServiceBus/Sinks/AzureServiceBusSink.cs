using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Sinks.AzureServiceBus
{
    public class SerilLogEventModel
    {
        public LogEventLevel Level { set;  get; }
        public string MessageTemplateName { set; get; }
        public string MessageTemplate { set; get; }
        public IReadOnlyDictionary<string, LogEventPropertyValue> Properties { set; get; }
        public System.Exception Exception { set;  get; }
    }

    public class AzureServiceBusSink : ILogEventSink
    {
        readonly int _waitTimeoutInMilliseconds = Timeout.Infinite;
        readonly IQueueClient _queueClient;
        readonly IFormatProvider _formatProvider;

        public AzureServiceBusSink(
            IQueueClient queueClient,
            IFormatProvider formatProvider)
        {
            _queueClient = queueClient;
            _formatProvider = formatProvider;
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            SerilLogEventModel model = new SerilLogEventModel()
            {
                Level = logEvent.Level,
                Properties = logEvent.Properties,
                MessageTemplateName = logEvent.MessageTemplate.Text,
                MessageTemplate= logEvent.RenderMessage(),
                Exception = logEvent.Exception
            };
           
            var jsonStr = JsonConvert.SerializeObject(model);
            var message = new Message(Encoding.UTF8.GetBytes(jsonStr));
            _queueClient.SendAsync(message).Wait(_waitTimeoutInMilliseconds);
        }
    }
}
