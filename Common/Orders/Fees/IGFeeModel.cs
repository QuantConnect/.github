/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using QuantConnect.Securities;

namespace QuantConnect.Orders.Fees
{
    /// <summary>
    /// Provides an implementation of <see cref="FeeModel"/> that models IG Markets order fees.
    /// IG Markets is a spread-based broker and does not charge explicit commissions for most instruments.
    /// </summary>
    public class IGFeeModel : FeeModel
    {
        /// <summary>
        /// Get the fee for this order in units of the account currency
        /// </summary>
        /// <returns>The cost of the order in units of the account currency (Zero for IG Markets)</returns>
        public override OrderFee GetOrderFee(OrderFeeParameters parameters)
        {
            return OrderFee.Zero;
        }
    }
}
