namespace Messages
{
	using System;
	using NServiceBus;

	public class OrderCreatedEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}