using System.Threading.Tasks;
using EnrollmentService.Data.DTO;

namespace PlatformService.SyncDataServices.Http
{
    public interface IPaymentDataClient
    {
        Task SendPlatformToCommand(CreateEnrollDto plat);
    }
}