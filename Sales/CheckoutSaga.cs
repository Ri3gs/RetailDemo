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
		IHandleMessages<PaymentHistoryPositiveEvent>,
		IHandleMessages<PaymentHistoryNegativeEvent>,
		IHandleMessages<StartPaymentCommand>,
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
			mapper.ConfigureMapping<CheckoutCommand>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<CheckGoodsInStockCommand>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<GoodsInStockEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<GoodsNotInStockEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<PaymentHistoryPositiveEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<PaymentHistoryNegativeEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<StartPaymentCommand>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<PaymentCompletedEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<PaymentDeniedEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<SendDeliveryRequestCommand>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<DeliveryRequestApprovedEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<DeliveryRequestRefusedEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<CreateOrderCommand>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
			mapper.ConfigureMapping<OrderCreatedEvent>(message => message.CartId).ToSaga(sagaData => sagaData.CartId);
		}

		public Task Handle(CheckoutCommand message, IMessageHandlerContext context)
		{
			Data.CustomerId = message.CustomerId;

			var checkGoodsInStock = new CheckGoodsInStockCommand
			{
				CartId = message.CartId
			};

			return context.SendLocal(checkGoodsInStock);
		}

		public Task Handle(CheckGoodsInStockCommand message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here

			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var goodsNotInStock = new GoodsNotInStockEvent
				{
					CartId = message.CartId
				};
				return context.Publish(goodsNotInStock);
			}

			var goodsInStock = new GoodsInStockEvent
			{
				CartId = message.CartId
			};
			return context.Publish(goodsInStock);
		}

		public Task Handle(GoodsInStockEvent message, IMessageHandlerContext context)
		{
			var checkCustomerPaymentHistory = new CheckCustomerPaymentHistoryCommand
			{
				CartId = message.CartId,
				CustomerId = Data.CustomerId
			};
			return context.Send(checkCustomerPaymentHistory);
		}

		public Task Handle(GoodsNotInStockEvent message, IMessageHandlerContext context)
		{
			//TODO: acutal business logic goes here
			//TODO: compensate and inform customer

			log.Info($"Goods are not in stock for CartId {message.CartId}");
			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(PaymentHistoryPositiveEvent message, IMessageHandlerContext context)
		{
			var startPayment = new StartPaymentCommand
			{
				CartId = message.CartId
			};
			return context.SendLocal(startPayment);
		}

		public Task Handle(PaymentHistoryNegativeEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here

			log.Info($"Payment history is negative for CartId {message.CartId}");
			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(StartPaymentCommand message, IMessageHandlerContext context)
		{
			//TODO: acual charging logic goes here

			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var paymentDenied = new PaymentDeniedEvent
				{
					CartId = message.CartId
				};
				return context.Publish(paymentDenied);
			}

			var paymentCompleted = new PaymentCompletedEvent
			{
				CartId = message.CartId
			};
			return context.Publish(paymentCompleted);
		}

		public Task Handle(PaymentCompletedEvent message, IMessageHandlerContext context)
		{
			var sendDeliveryRequest = new SendDeliveryRequestCommand
			{
				CartId = message.CartId
			};
			return context.SendLocal(sendDeliveryRequest);
		}

		public Task Handle(PaymentDeniedEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here

			log.Info($"Payment denied for CartId {message.CartId}");
			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(SendDeliveryRequestCommand message, IMessageHandlerContext context)
		{
			//TODO: acual delivery logic goes here

			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var deliveryRequestRefused = new DeliveryRequestRefusedEvent
				{
					CartId = message.CartId
				};
				return context.Publish(deliveryRequestRefused);
			}

			var deliveryRequestApproved = new DeliveryRequestApprovedEvent
			{
				CartId = message.CartId
			};
			return context.Publish(deliveryRequestApproved);
		}

		public Task Handle(DeliveryRequestApprovedEvent message, IMessageHandlerContext context)
		{
			var createOrder = new CreateOrderCommand
			{
				CartId = message.CartId
			};
			return context.SendLocal(createOrder);
		}

		public Task Handle(DeliveryRequestRefusedEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here
			log.Info($"Delivery request refused for CartId {message.CartId}");
			MarkAsComplete();
			return Task.CompletedTask;
		}

		public Task Handle(CreateOrderCommand message, IMessageHandlerContext context)
		{
			//TODO: create order
			var orderCreated = new OrderCreatedEvent
			{
				CartId = message.CartId
			};
			return context.Publish(orderCreated);
		}

		public Task Handle(OrderCreatedEvent message, IMessageHandlerContext context)
		{
			//TODO: actual business logic goes here
			log.Info($"Order created from CartId {message.CartId}");
			MarkAsComplete();
			return Task.CompletedTask;
		}
	}
}