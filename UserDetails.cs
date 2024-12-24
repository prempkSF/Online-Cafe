namespace OnlineCafe
{
    //UserDetails Multi-Inheritance PersonalDetails Class and IBalance Interface 
    /// <summary>
    /// UserDetails Class
    /// </summary>
    public class UserDetails : PersonalDetails, IBalance
    {
        /// <summary>
        /// <see cref="UserDetails"/> Static Unique User ID
        /// </summary>
        private static int s_userID = 1000;

        /// <summary>
        /// <see cref="UserDetails"/> Unique User ID Auto Generated
        /// </summary>
        /// <value>SF1001</value>
        public string UserID { get; set; }
        /// <summary>
        /// <see cref="UserDetails"/> User's Work Station 
        /// </summary>
        /// <value>WS101</value>
        public string WorkStationNumber { get; set; }
        /// <summary>
        /// <see cref="UserDetails"/> Private Wallet Balance
        /// </summary>
        /// <value></value>
        private double _walletBalance { get; set; }
        /// <summary>
        /// <see cref="UserDetails"/> Public Wallet Balance
        /// </summary>
        /// <value></value>
        public double WalletBalance { get { return _walletBalance; } set{ _walletBalance=value;}}

        public UserDetails()
        {

        }
        public UserDetails(string name,string fatherName,GenderDetails gender,string mobileNumber,string mailID,string workStation,double walletBalance):base(name,fatherName,gender,mobileNumber,mailID)
        {
            UserID=$"SF{++s_userID}";
            WorkStationNumber=workStation;
            _walletBalance=WalletBalance;
        }
        

   
    
        
        /// <summary>
        /// To Recharge User Wallet
        /// </summary>
        /// <param name="walletRecharge">Recharge Amount</param>
        public void WalletRecharge(double walletRecharge)
        {
            _walletBalance += walletRecharge;
        }
        /// <summary>
        /// To Deduct Amount from Wallet
        /// </summary>
        /// <param name="deductAmount">Deduct Amount</param>
        public void DeductAmount(double deductAmount)
        {
            _walletBalance -= deductAmount;
        }
    }
}