using esme.Admin.Shared.ViewModels;
using System.Threading.Tasks;

namespace esme.Admin.Shared.Services
{
    public interface IInvitationService
    {
        Task Invite(InvitationEditModel model, string baseUri);
    }
}
