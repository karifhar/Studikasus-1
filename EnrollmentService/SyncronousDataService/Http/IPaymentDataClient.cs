using System.Threading.Tasks;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;

namespace PlatformService.SyncDataServices.Http
{
    public interface IPaymentDataClient
    {
        Task SendPlatformToCommand(CreateEnrollDto plat);
    }
}