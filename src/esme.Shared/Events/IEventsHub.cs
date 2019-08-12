using System.Threading.Tasks;

namespace esme.Shared.Events
{
    public interface IEventsHub
    {
        Task MessagePosted(MessagePostedEvent messagePostedEvent);
    }
}
