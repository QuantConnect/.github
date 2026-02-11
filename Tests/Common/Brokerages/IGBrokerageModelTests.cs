using System;
using NUnit.Framework;
using QuantConnect.Brokerages;
using QuantConnect.Orders.Fees;
using QuantConnect.Orders.Slippage;
using QuantConnect.Securities;

namespace QuantConnect.Tests.Common.Brokerages
{
    [TestFixture]
    public class IGBrokerageModelTests
    {
        [Test]
        public void GetFeeModelReturnsIGFeeModel()
        {
            var model = new IGBrokerageModel();
            var feeModel = model.GetFeeModel(null);
            Assert.IsInstanceOf<IGFeeModel>(feeModel);
        }

        [Test]
        public void IGFeeModelReturnsZeroFee()
        {
            var feeModel = new IGFeeModel();
            var fee = feeModel.GetOrderFee(null);
            Assert.AreEqual(0, fee.Value.Amount);
        }

        [Test]
        public void GetSlippageModelReturnsConstantSlippageModelZero()
        {
            var model = new IGBrokerageModel();
            var slippageModel = model.GetSlippageModel(null);
            Assert.IsInstanceOf<ConstantSlippageModel>(slippageModel);
        }

        [TestCase(SecurityType.Forex, true)]
        [TestCase(SecurityType.Cfd, true)]
        [TestCase(SecurityType.Crypto, true)]
        [TestCase(SecurityType.Index, true)]
        [TestCase(SecurityType.Equity, true)]
        [TestCase(SecurityType.Option, false)]
        [TestCase(SecurityType.Future, false)]
        public void IsSecurityTypeSupported(SecurityType securityType, bool expected)
        {
            var model = new IGBrokerageModel();
            var security = new Security(
                SecurityExchangeHours.AlwaysOpen(TimeZones.Utc),
                new QuantConnect.Data.SubscriptionDataConfig(typeof(QuantConnect.Data.Market.TradeBar), Symbol.Create("ABC", securityType, Market.IG), Resolution.Minute, TimeZones.Utc, TimeZones.Utc, true, true, false),
                new Cash(Currencies.USD, 0, 1m),
                SymbolProperties.GetDefault(Currencies.USD),
                ErrorCurrencyConverter.Instance,
                RegisteredSecurityDataTypesProvider.Null,
                new SecurityCache()
            );

            var order = new QuantConnect.Orders.MarketOrder(security.Symbol, 1, DateTime.UtcNow);
            BrokerageMessageEvent message;
            var result = model.CanSubmitOrder(security, order, out message);

            Assert.AreEqual(expected, result);
        }
    }
}
