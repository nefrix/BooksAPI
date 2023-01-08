using Smd.InterviewAssignment.WebApi.Models;
using System.Threading.Tasks;

namespace Smd.InterviewAssignment.WebApi.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto email);
    }
}
