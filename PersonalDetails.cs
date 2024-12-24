using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{
    /// <summary>
    /// User's Personal Details Class
    /// </summary>
    public class PersonalDetails
    {
        /// <summary>
        /// <see cref="PersonalDetails"/> User's Name
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// <see cref="PersonalDetails"/> User Father's Name
        /// </summary>
        /// <value></value>
        public string FatherName { get; set; }
        /// <summary>
        /// <see cref="PersonalDetails"/> User's Gender Details
        /// </summary>
        /// <value></value>
        public GenderDetails Gender { get; set; }
        /// <summary>
        /// <see cref="PersonalDetails"/> User's Mobile Number
        /// </summary>
        /// <value></value>
        public string MobileNumber { get; set; }
        /// <summary>
        /// <see cref="PersonalDetails"/> User's Email ID
        /// </summary>
        /// <value></value>
        public string MailID { get; set; }

        public PersonalDetails()
        {}
        public PersonalDetails(string name,string fatherName,GenderDetails gender,string mobileNumber,string mailID)
        {
            Name=name;
            FatherName=fatherName;
            Gender=gender;
            MobileNumber=mobileNumber;
            MailID=mailID;
        }
    }
}