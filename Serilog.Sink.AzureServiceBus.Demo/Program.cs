using System;

namespace Serilog.Sink.AzureServiceBus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ConnectionString = "The real connection string for Azure Service Bus";
            var QueueName = "Name of the queue";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.AzureServiceBus(ConnectionString, QueueName)
                .CreateLogger();
            Log.Information("Hello World!"); 

        }
    }
}
