using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
	public class PaymentHistorySaga : Saga<PaymentHistorySagaData>,
		IAmStartedByMessages<CheckCustomerPaymentHistoryCommand>
	{
		static ILog log = LogManager.GetLogger<PaymentHistorySaga>();

		public Task Handle(CheckCustomerPaymentHistoryCommand message, IMessageHandlerContext context)
		{
			//TODO: acutal business logic goes here
			var random = new Random();
			//randomly return negative events
			if (random.Next(0, 5) == 0)
			{
				var paymentHistoryNegative = new PaymentHistoryNegativeEvent
				{
					CartId = message.CartId
				};
				return context.Publish(paymentHistoryNegative);
			}

			var paymentHistoryPositive = new PaymentHistoryPositiveEvent
			{
				CartId = message.CartId
			};
			return context.Publish(paymentHistoryPositive);
		}

		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PaymentHistorySagaData> mapper)
		{
			mapper.ConfigureMapping<CheckCustomerPaymentHistoryCommand>(message => message.CustomerId).ToSaga(sagaData => sagaData.CustomerId);
		}
	}
}
