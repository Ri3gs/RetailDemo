namespace Shipping
{
	using System;
	using System.Threading.Tasks;
	using NServiceBus;

	class Program
	{
		static async Task Main(string[] args)
		{
			Console.Title = "Delivery";

			var endpointConfiguration = new EndpointConfiguration("Delivery");

			endpointConfiguration.UseTransport<LearningTransport>();

			var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();

			await endpointInstance.Stop().ConfigureAwait(false);
		}
	}
}
