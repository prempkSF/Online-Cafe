using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{   
    /// <summary>
    /// <see cref="IBalance"> Balance Interface
    /// </summary>
    public interface IBalance
    {
        /// <summary>
        /// <see cref="IBalance"/> User's Wallet Balance Declaration
        /// </summary>
        /// <value></value>
        public double WalletBalance { get; }
        /// <summary>
        /// <see cref="IBalance"/> User's Wallet Recharge Method Declaration
        /// </summary>
        /// <value></value>
        public void WalletRecharge(double rechargeAmount);
        /// <summary>
        /// <see cref="IBalance"/> User's Wallet Deduct Amount Method Declaration
        /// </summary>
        /// <value></value>
        public void DeductAmount(double deductAmount);
    }
}