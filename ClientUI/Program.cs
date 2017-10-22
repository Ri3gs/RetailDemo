using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{
	class Program
	{
		static async Task Main()
		{
			Console.Title = "ClientUI";

			var endpointConfiguration = new EndpointConfiguration("ClientUI");
			endpointConfiguration.UsePersistence<LearningPersistence>();

			var transport = endpointConfiguration.UseTransport<LearningTransport>();

			var routing = transport.Routing();
			routing.RouteToEndpoint(typeof(CheckoutCommand), "Sales");

			var endpointInstance = await Endpoint.Start(endpointConfiguration)
				.ConfigureAwait(false);

			await RunLoop(endpointInstance);

			await endpointInstance.Stop()
				.ConfigureAwait(false);
		}

		static ILog log = LogManager.GetLogger<Program>();

		static async Task RunLoop(IEndpointInstance endpointInstance)
		{
			while (true)
			{
				log.Info("Press 'P' to place an order, or 'Q' to quit.");
				var key = Console.ReadKey();
				Console.WriteLine();

				switch (key.Key)
				{
					case ConsoleKey.P:
						// Instantiate the command
						var command = new CheckoutCommand
						{
							CartId = Guid.NewGuid(),
							CustomerId = Guid.NewGuid()
						};

						// Send the command
						log.Info($"Sending Checkout command, CartId = {command.CartId}");
						await endpointInstance.Send(command)
							.ConfigureAwait(false);

						break;

					case ConsoleKey.Q:
						return;

					default:
						log.Info("Unknown input. Please try again.");
						break;
				}
			}
		}
	}
}