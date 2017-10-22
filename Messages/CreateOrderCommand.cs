namespace Messages
{
	using System;
	using NServiceBus;

	public class CreateOrderCommand : ICommand
	{
		public Guid CartId { get; }

		public CreateOrderCommand(Guid cartId)
		{
			CartId = cartId;
		}
	}
}