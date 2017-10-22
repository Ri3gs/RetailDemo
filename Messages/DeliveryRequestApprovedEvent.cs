namespace Messages
{
	using System;
	using NServiceBus;

	public class DeliveryRequestApprovedEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}