namespace ParcelHub.ServiceRepository
{
    public interface IUserSerivce
    {
        string GetUserId();
        bool IsAuthenticated();
        public string GetUserEmail();
    }
}