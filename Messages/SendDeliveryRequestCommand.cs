namespace Messages
{
	using System;
	using NServiceBus;

	public class SendDeliveryRequestCommand : ICommand
	{
		public Guid CartId { get; set; }
	}
}