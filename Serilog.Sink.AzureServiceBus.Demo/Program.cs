using System;

namespace Serilog.Sink.AzureServiceBus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ConnectionString = "The real connection string for Azure Service Bus";
            //var QueueName = "Name of the queue";
            var ConnectionString = "Endpoint=sb://vessel-registry-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Cu2uP3XLV4GiH3bIsu7R7yztSNJpV74nE68g7l1Hknc=";
            var QueueName = "vr-serilog-queue-test";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.AzureServiceBus(ConnectionString, QueueName)
                .CreateLogger();
            Log.Information("Hello World!"); 

        }
    }
}
