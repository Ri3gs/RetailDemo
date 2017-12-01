namespace Messages
{
	using System;
	using NServiceBus;

	public class CheckCustomerPaymentHistoryCommand : ICommand
	{
		public Guid CartId { get; set; }
		public Guid CustomerId { get; set; }
	}
}