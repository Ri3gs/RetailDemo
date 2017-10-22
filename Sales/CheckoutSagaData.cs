namespace Sales
{
	using System;
	using NServiceBus;

	public class CheckoutSagaData : IContainSagaData
	{
		public Guid Id { get; set; }
		public string Originator { get; set; }
		public string OriginalMessageId { get; set; }
		public Guid CartId { get; set; }
		public Guid CustomerId { get; set; }
	}
}