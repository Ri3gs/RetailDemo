namespace Messages
{
	using System;
	using NServiceBus;

	public class PaymentHistoryNegativeEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}