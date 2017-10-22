namespace Sales
{
	using System;
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
		IHandleMessages<ChargeCustomerCommand>,
		IHandleMessages<PaymentCompletedEvent>,
		IHandleMessages<PaymentDeniedEvent>,
		IHandleMessages<SendDeliveryRequestCommand>,
		IHandleMessages<DeliveryRequestApprovedEvent>,
		IHandleMessages<DeliveryRequestRefusedEvent>,
		IHandleMessages<CreateOrderCommand>,
		IHandleMessages<OrderCreatedEvent>
	{

		static ILog log = LogManager.GetLogger<CheckoutSaga>();
		static Random random = new Random();

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
			//TODO: actual business logic goes here

			//randomly return negative events
			if (random.Next(0, 3) == 0)
			{
				var goodsNotInStock = new GoodsNotInStockEvent(message.CartId);
				return context.Publish(goodsNotInStock);
			}

			var goodsInStock = new GoodsInStockEvent(message.CartId);
			return context.Publish(goodsInStock);
		}

		public Task Handle(GoodsInStockEvent message, IMessageHandlerContext context)
		{
			var checkCustomerPaymentHistory = new CheckCustomerPaymentHistoryCommand(message.CartId);
			return context.Send(checkCustomerPaymentHistory);
		}

		public Task Handle(GoodsNotInStockEvent message, IMessageHandlerContext context)
		{
			//TODO: acutal business logic goes here
			//TODO: compensate and inform customer

			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(CheckCustomerPaymentHistoryCommand message, IMessageHandlerContext context)
		{
			//TODO: acutal business logic goes here

			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var paymentHistoryNegative = new PaymentHistoryNegativeEvent(message.CartId);
				return context.Publish(paymentHistoryNegative);
			}

			var paymentHistoryPositive = new PaymentHistoryPositiveEvent(message.CartId);
			return context.Publish(paymentHistoryPositive);
		}

		public Task Handle(PaymentHistoryPositiveEvent message, IMessageHandlerContext context)
		{
			var chargeCustomer = new ChargeCustomerCommand(message.CartId);
			return context.Send(chargeCustomer);
		}

		public Task Handle(PaymentHistoryNegativeEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here
			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(ChargeCustomerCommand message, IMessageHandlerContext context)
		{
			//TODO: acual charging logic goes here

			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var paymentDenied = new PaymentDeniedEvent(message.CartId);
				return context.Publish(paymentDenied);
			}

			var paymentCompleted = new PaymentCompletedEvent(message.CartId);
			return context.Publish(paymentCompleted);
		}

		public Task Handle(PaymentCompletedEvent message, IMessageHandlerContext context)
		{
			var sendDeliveryRequest = new SendDeliveryRequestCommand(message.CartId);
			return context.Send(sendDeliveryRequest);
		}

		public Task Handle(PaymentDeniedEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here

			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(SendDeliveryRequestCommand message, IMessageHandlerContext context)
		{
			//TODO: acual delivery logic goes here

			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var deliveryRequestRefused = new DeliveryRequestRefusedEvent(message.CartId);
				return context.Publish(deliveryRequestRefused);
			}

			var deliveryRequestApproved = new DeliveryRequestApprovedEvent(message.CartId);
			return context.Publish(deliveryRequestApproved);
		}

		public Task Handle(DeliveryRequestApprovedEvent message, IMessageHandlerContext context)
		{
			var createOrder = new CreateOrderCommand(message.CartId);
			return context.Send(createOrder);
		}

		public Task Handle(DeliveryRequestRefusedEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here

			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(CreateOrderCommand message, IMessageHandlerContext context)
		{
			//TODO: create order
			var orderCreated = new OrderCreatedEvent(message.CartId);
			return context.Publish(orderCreated);
		}

		public Task Handle(OrderCreatedEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here

			MarkAsComplete();
			return Task.CompletedTask;
		}
	}
}