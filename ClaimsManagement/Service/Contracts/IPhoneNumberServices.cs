namespace Service.Contracts
{
    public interface IPhoneNumberServices
    {
        bool IsValidNumber(string number, string region);
    }
}
