namespace Messages
{
	using System;
	using NServiceBus;

	public class CheckGoodsInStockCommand : ICommand
	{
		public Guid CartId { get; set; }
	}
}