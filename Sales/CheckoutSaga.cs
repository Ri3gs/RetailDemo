namespace Sales
{
	using System.Threading.Tasks;
	using Messages;
	using NServiceBus;
	using NServiceBus.Logging;

	public class CheckoutSaga : Saga<CheckoutSagaData>,
		IAmStartedByMessages<CheckoutCommand>,
		IHandleMessages<CheckGoodsInStockCommand>,
		IHandleMessages<GoodsInStockEvent>,
		IHandleMessages<GoodsNotInStockEvent>,
		IHandleMessages<CheckCustomerPaymentHistoryCommand>,
		IHandleMessages<PaymentHistoryPositiveEvent>,
		IHandleMessages<PaymentHistoryNegativeEvent>,
		IHandleMessages<PaymentCompletedEvent>,
		IHandleMessages<PaymentDeniedEvent>,
		IHandleMessages<SendDeliveryRequestCommand>,
		IHandleMessages<DeliveryRequestApprovedEvent>,
		IHandleMessages<DeliveryRequestRefusedEvent>
	{

		static ILog log = LogManager.GetLogger<CheckoutSaga>();

		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CheckoutSagaData> mapper)
		{
			//TODO: configure mapping
			throw new System.NotImplementedException();
		}

		public Task Handle(CheckoutCommand message, IMessageHandlerContext context)
		{
			Data.Id = message.CartId;
			Data.CustomerId = message.CustomerId;

			var checkGoodsInStock = new CheckGoodsInStockCommand(message.CartId);

			return context.Send(checkGoodsInStock);
		}

		public Task Handle(CheckGoodsInStockCommand message, IMessageHandlerContext context)
		{
			//TODO: actually check for goods


		}

		public Task Handle(GoodsInStockEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(GoodsNotInStockEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(DeliveryRequestRefusedEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(DeliveryRequestApprovedEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(CheckCustomerPaymentHistoryCommand message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(PaymentHistoryPositiveEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(PaymentHistoryNegativeEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(PaymentCompletedEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(PaymentDeniedEvent message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}

		public Task Handle(SendDeliveryRequestCommand message, IMessageHandlerContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}