namespace Shipping
{
	using System.Threading.Tasks;
	using Messages;
	using NServiceBus;
	using NServiceBus.Logging;

	public class OrderBilledHandler : IHandleMessages<OrderBilled>
	{
		static ILog log = LogManager.GetLogger<OrderBilledHandler>();

		public Task Handle(OrderBilled message, IMessageHandlerContext context)
		{
			log.Info($"Shipping has received OrderPlaced, OrderId = {message.OrderId}");
			log.Info($"OrderId = {message.OrderId} is paid. Should we ship?");

			return Task.CompletedTask;
		}
	}
}