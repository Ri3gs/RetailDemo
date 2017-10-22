namespace Messages
{
	using System;
	using NServiceBus;

	public class GoodsNotInStockEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}