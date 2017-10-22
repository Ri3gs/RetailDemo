namespace Messages
{
	using System;
	using NServiceBus;

	public class CheckoutCommand : ICommand
	{
		public Guid CartId { get; set; }
		public Guid CustomerId { get; set; }
	}
}