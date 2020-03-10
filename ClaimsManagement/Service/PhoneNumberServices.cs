using PhoneNumbers;
using Service.Contracts;
using System;

namespace Service
{
    public class PhoneNumberServices : IPhoneNumberServices
    {
        public bool IsValidNumber(string number,string region)
        {
            PhoneNumber numberProto;
            var phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
            numberProto = phoneUtil.Parse(number, region);
            }
            catch (Exception)
            {
                return false;
            }

            if (!phoneUtil.IsValidNumber(numberProto))
            {
                return false;
            }

            return true;
        }
    }
}
