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
            HashSet<string> svgElements = new HashSet<string>();
            svgElements.Add(DrawCircle(Center, Center, OuterRadius)); // big outer
            //DrawCircle(Center, Center, InnerRadius); // center small one

            foreach (var (x, y) in ComputeCenters(Center, Center, InnerRadius))
            {
                double distanceToCenter = Math.Sqrt(Math.Pow(Center - x, 2) + Math.Pow(Center - y, 2));
                bool overlapsOuterCircle = distanceToCenter > OuterRadius - InnerRadius + 1.0;
                if (overlapsOuterCircle)
                {

                }
                else
                {
                    svgElements.Add(DrawCircle(x, y, InnerRadius));
                }
            }
            foreach (string svgElement in svgElements)
            {
                TestContext.Progress.WriteLine(svgElement);
            }
        }

        private IEnumerable<(double, double)> ComputeCenters(double X, double Y, double R, int level = 0)
        {
            if (level > 2) yield break;

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

        private string DrawCircle(double x, double y, double r)
        {
            return $"            <circle cx=\"{x:0.####}\" cy=\"{y:0.####}\" r=\"{r}\" stroke-width=\"1\" fill=\"none\" />";
        }

        private string DrawArc(double x, double y, double radius, double startAngle, double endAngle)
        {
            var start = PolarToCartesian(x, y, radius, endAngle);
            var end = PolarToCartesian(x, y, radius, startAngle);

            string largeArcFlag = endAngle - startAngle <= Math.PI ? "0" : "1";

            var d = $"M {start.x} {start.y} A {radius} {radius} 0 {largeArcFlag} 0 {end.x} {end.y}";

            return $"            <path d=\"{d}\" stroke-width=\"1\" fill=\"none\" />";
        }

        private (double x, double y) PolarToCartesian(double centerX, double centerY, double radius, double angle)
        {
            return (centerX + (radius * Math.Cos(angle)), centerY + (radius * Math.Sin(angle)));
        }
    }
}
