using esme.Shared.Circles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace esme.Client.Pages
{
    public static class MessagesExtensions
    {
        public static IEnumerable<object> WithDateLabels(this IEnumerable<MessageViewModel> messages)
        {
            DateTime? previousDate = null;
            foreach (var message in messages.OrderBy(m => m.SentAt))
            {
                DateTime sentAtDate = message.SentAt.Date;
                if (previousDate == null || sentAtDate.Date > previousDate)
                {
                    if (sentAtDate == DateTime.Today.AddDays(-1))
                    {
                        yield return "Yesterday";
                    }
                    else if (sentAtDate == DateTime.Today)
                    {
                        yield return "Today";
                    }
                    else
                    {
                        yield return sentAtDate.Date.ToShortDateString();
                    }
                }
                yield return message;
                previousDate = sentAtDate.Date;
            }
        }
    }
}
