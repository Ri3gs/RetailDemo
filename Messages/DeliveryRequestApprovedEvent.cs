namespace Messages
{
	using System;
	using NServiceBus;

	public class DeliveryRequestApprovedEvent : IEvent
	{
		public Guid CartId { get; set; }

		public DeliveryRequestApprovedEvent(Guid cartId)
		{
			CartId = cartId;
		}
	}
}