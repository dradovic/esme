using NUnit.Framework;
using System;

namespace esme.Core.Tests
{
    [TestFixture]
    public class BlumeDesLebensGenerator
    {
        [Test]
        public void Generate()
        {
            const int ViewBoxWidth = 310;
            const int ViewBoxOffset = 10;
            const int Center = ViewBoxWidth / 2;
            const int OuterRadius = (ViewBoxWidth - ViewBoxOffset) / 2;
            const int InnerRadius = OuterRadius / 3;
            DrawCircle(Center, Center, OuterRadius);
            DrawCircle(Center, Center, InnerRadius);
            DrawCircle(Center, Center - InnerRadius, InnerRadius);
            DrawCircle(Center, Center + InnerRadius, InnerRadius);
        }

        private void DrawCircle(double x, double y, double r)
        {
            TestContext.Progress.WriteLine($"            <circle cx=\"{x}\" cy=\"{y}\" r=\"{r}\" stroke-width=\"1\" fill=\"none\" />");
        }
    }
}
