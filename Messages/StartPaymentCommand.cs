namespace Messages
{
	using System;
	using NServiceBus;

	public class StartPaymentCommand : ICommand
	{
		public Guid CartId { get; set; }
	}
}