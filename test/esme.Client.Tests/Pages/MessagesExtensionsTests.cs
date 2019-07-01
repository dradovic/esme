using esme.Client.Pages;
using esme.Shared.Circles;
using System;
using System.Collections.Generic;
using Xunit;

namespace esme.Client.Tests.Pages
{
    public class MessagesExtensionsTests
    {
        [Fact]
        public void TestWithDateLabels()
        {
            var messages = new List<MessageViewModel>
            {
                new MessageViewModel { SentAt = new DateTime(2019, 6, 1, 14, 0, 0) },
            };
            var result = messages.WithDateLabels();
            Assert.Collection(result,
                i => Assert.Equal("6/1/2019", i),
                i => Assert.Equal(14, ((MessageViewModel)i).SentAt.Hour)
            );

            messages.Add(new MessageViewModel { SentAt = new DateTime(2019, 6, 1, 15, 0, 0) });
            result = messages.WithDateLabels();
            Assert.Collection(result,
                i => Assert.Equal("6/1/2019", i),
                i => Assert.Equal(14, ((MessageViewModel)i).SentAt.Hour),
                i => Assert.Equal(15, ((MessageViewModel)i).SentAt.Hour)
            );

            messages.Add(new MessageViewModel { SentAt = new DateTime(2019, 6, 2, 16, 0, 0) });
            result = messages.WithDateLabels();
            Assert.Collection(result,
                i => Assert.Equal("6/1/2019", i),
                i => Assert.Equal(14, ((MessageViewModel)i).SentAt.Hour),
                i => Assert.Equal(15, ((MessageViewModel)i).SentAt.Hour),
                i => Assert.Equal("6/2/2019", i),
                i => Assert.Equal(16, ((MessageViewModel)i).SentAt.Hour)
            );

            messages.Add(new MessageViewModel { SentAt = DateTime.Now.AddDays(-1) });
            messages.Add(new MessageViewModel { SentAt = DateTime.Now });
            result = messages.WithDateLabels();
            Assert.Collection(result,
                i => Assert.Equal("6/1/2019", i),
                i => Assert.Equal(14, ((MessageViewModel)i).SentAt.Hour),
                i => Assert.Equal(15, ((MessageViewModel)i).SentAt.Hour),
                i => Assert.Equal("6/2/2019", i),
                i => Assert.Equal(16, ((MessageViewModel)i).SentAt.Hour),
                i => Assert.Equal("Yesterday", i),
                i => Assert.IsType<MessageViewModel>(i),
                i => Assert.Equal("Today", i),
                i => Assert.IsType<MessageViewModel>(i)
            );

        }
    }
}
