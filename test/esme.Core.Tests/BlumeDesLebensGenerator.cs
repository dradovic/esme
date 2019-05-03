using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace esme.Core.Tests
{
    [TestFixture]
    public class BlumeDesLebensGenerator
    {
        private const double SixtyDegInRad = Math.PI / 3.0;

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
                double distanceToCenter = Distance((Center, Center), (x, y));
                bool overlapsOuterCircle = IsGreaterThan(distanceToCenter, OuterRadius - InnerRadius);
                if (overlapsOuterCircle)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        double startAngle = i * SixtyDegInRad;
                        double endAngle = (i + 1) * SixtyDegInRad;
                        var start = PolarToCartesian(x, y, InnerRadius, endAngle);
                        var end = PolarToCartesian(x, y, InnerRadius, startAngle);
                        bool arcCrossesOuterCircle =
                            IsGreaterThan(Distance((Center, Center), start), OuterRadius) ||
                            IsGreaterThan(Distance((Center, Center), end), OuterRadius);
                        if (!arcCrossesOuterCircle)
                        {
                            svgElements.Add(DrawArc(x, y, InnerRadius, startAngle, endAngle));
                        }
                    }
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

        private static bool IsGreaterThan(double a, double b)
        {
            return a > b + 1.0;
        }

        private static double Distance((double x, double y) p1, (double x, double y) p2)
        {
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
        }

        private IEnumerable<(double, double)> ComputeCenters(double X, double Y, double R, int level = 0)
        {
            if (level > 3) yield break;

            for (int i = 0; i < 6; i++)
            {
                double x = X + Math.Cos(i * SixtyDegInRad) * R;
                double y = Y + Math.Sin(i * SixtyDegInRad) * R;
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

            var d = $"M {start.x:0.####} {start.y:0.####} A {radius} {radius} 0 {largeArcFlag} 0 {end.x:0.####} {end.y:0.####}";

            return $"            <path d=\"{d}\" stroke-width=\"1\" fill=\"none\" />";
        }

        private (double x, double y) PolarToCartesian(double centerX, double centerY, double radius, double angle)
        {
            return (centerX + (radius * Math.Cos(angle)), centerY + (radius * Math.Sin(angle)));
        }
    }
}
