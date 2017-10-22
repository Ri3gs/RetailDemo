namespace Messages
{
	using System;
	using NServiceBus;

	public class PaymentHistoryPositiveEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}