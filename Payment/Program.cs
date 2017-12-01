using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages;

namespace Billing
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Payment";

            var endpointConfiguration = new EndpointConfiguration("Payment");

			var transport = endpointConfiguration.UseTransport<LearningTransport>();
			endpointConfiguration.UsePersistence<LearningPersistence>();
			var routing = transport.Routing();
			routing.RouteToEndpoint(typeof(PaymentHistoryPositiveEvent), "Sales");
			routing.RouteToEndpoint(typeof(PaymentHistoryNegativeEvent), "Sales");
			var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
			
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}