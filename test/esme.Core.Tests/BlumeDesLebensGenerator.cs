using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace esme.Core.Tests
{
    [TestFixture]
    public class BlumeDesLebensGenerator
    {
        [Test] // FIXME: da, change to Explicit
        public void Generate()
        {
            const int ViewBoxWidth = 310;
            const int ViewBoxOffset = 10;
            const int Center = ViewBoxWidth / 2;
            const int OuterRadius = (ViewBoxWidth - ViewBoxOffset) / 2;
            const int InnerRadius = OuterRadius / 3;
            DrawCircle(Center, Center, OuterRadius); // big outer
            DrawCircle(Center, Center, InnerRadius); // center small one

            HashSet<(double, double)> centers = new HashSet<(double, double)>();
            foreach (var center in ComputeCenters(Center, Center, InnerRadius))
            {
                centers.Add(center); // eliminates doubles
            }
            foreach (var (x, y) in centers)
            {
                DrawCircle(x, y, InnerRadius);
            }
        }

        //[Test]
        //public void HashWithTuplesWorksAsExpected()
        //{
        //    var set = new HashSet<(double, double)>();
        //    set.Add((Math.PI, Math.PI));
        //    set.Add((Math.PI, Math.PI));
        //    set.Add((Math.PI, 0.0));
        //    Assert.AreEqual(2, set.Count);
        //}

        private IEnumerable<(double, double)> ComputeCenters(double X, double Y, double R, int level = 0)
        {
            if (level > 1) yield break;

            const double sixtyDegInRad = Math.PI / 3.0;
            for (int i = 0; i < 6; i++)
            {
                double x = X + Math.Sin(i * sixtyDegInRad) * R;
                double y = Y + Math.Cos(i * sixtyDegInRad) * R;
                yield return (x, y);
                foreach (var center in ComputeCenters(x, y, R, level + 1))
                {
                    yield return center;
                }
            }
        }

        private void DrawCircle(double x, double y, double r)
        {
            TestContext.Progress.WriteLine($"            <circle cx=\"{x:0.####}\" cy=\"{y:0.####}\" r=\"{r}\" stroke-width=\"1\" fill=\"none\" />");
        }
    }
}
