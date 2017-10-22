namespace Messages
{
	using System;
	using NServiceBus;

	public class PaymentCompletedEvent : IEvent
	{
		public Guid CartId { get; }

		public PaymentCompletedEvent(Guid cartId)
		{
			CartId = cartId;
		}
	}
}