namespace Messages
{
	using System;
	using NServiceBus;

	public class CheckoutCommand : ICommand
	{
		public Guid CartId { get; }
		public Guid CustomerId { get; }

		public CheckoutCommand(Guid cartId, Guid customerId)
		{
			CartId = cartId;
			CustomerId = customerId;
		}
	}
}