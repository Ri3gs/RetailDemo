namespace Messages
{
	using System;
	using NServiceBus;

	public class PaymentDeniedEvent : IEvent
	{
		public Guid CartId { get; }

		public PaymentDeniedEvent(Guid cartId)
		{
			CartId = cartId;
		}
	}
}