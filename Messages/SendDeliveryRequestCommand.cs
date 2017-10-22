namespace Messages
{
	using System;
	using NServiceBus;

	public class SendDeliveryRequestCommand : ICommand
	{
		public Guid CartId { get; }

		public SendDeliveryRequestCommand(Guid cartId)
		{
			CartId = cartId;
		}
	}
}