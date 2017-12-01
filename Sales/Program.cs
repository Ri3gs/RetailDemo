using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages;

namespace Sales
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Sales";

            var endpointConfiguration = new EndpointConfiguration("Sales");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
	        endpointConfiguration.UsePersistence<LearningPersistence>();

			var routing = transport.Routing();
			routing.RouteToEndpoint(typeof(CheckCustomerPaymentHistoryCommand), "Payment");

			var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}