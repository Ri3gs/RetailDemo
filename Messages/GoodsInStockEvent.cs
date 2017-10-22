namespace Messages
{
	using System;
	using NServiceBus;

	public class GoodsInStockEvent : IEvent
	{
		public Guid CartId { get; set; }
	}
}