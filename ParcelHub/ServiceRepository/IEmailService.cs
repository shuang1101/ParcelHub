using ParcelHub.Models;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public interface IEmailService
    {
        Task SendtestEmail(UserEmailOption userEmailOption);
        Task SendConsumerAccountVerification(UserEmailOption userEmailOption);
    }
}