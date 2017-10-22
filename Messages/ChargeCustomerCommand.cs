namespace Messages
{
	using System;
	using NServiceBus;

	public class ChargeCustomerCommand : ICommand
	{
		public Guid CartId { get; }

		public ChargeCustomerCommand(Guid cartId)
		{
			CartId = cartId;
		}
	}
}