namespace Messages
{
	using System;
	using NServiceBus;

	public class DeliveryRequestRefusedEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}