using System.Threading.Tasks;

namespace esme.Shared.Events
{
    public interface IMessagesHub
    {
        Task MessagePosted(MessagePostedEvent messagePostedEvent);
    }
}
