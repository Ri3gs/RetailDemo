namespace Messages
{
	using System;
	using NServiceBus;

	public class OrderCreatedEvent : IEvent
	{
		public Guid CartId { get; }

		public OrderCreatedEvent(Guid cartId)
		{
			CartId = cartId;
		}
	}
}