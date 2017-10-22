namespace Shipping
{
	using System.Threading.Tasks;
	using Messages;
	using NServiceBus;
	using NServiceBus.Logging;

	public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
	{
		static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

		public Task Handle(OrderPlaced message, IMessageHandlerContext context)
		{
			log.Info($"Shipping has received OrderPlaced, OrderId = {message.OrderId}");
			log.Info($"OrderId = {message.OrderId} is placed. Should we ship?");

			return Task.CompletedTask;
		}
	}
}