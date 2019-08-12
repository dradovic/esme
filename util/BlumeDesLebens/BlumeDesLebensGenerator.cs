using System;
using System.Collections.Generic;

namespace BlumeDesLebens
{
    public static class BlumeDesLebensGenerator
    {
        private const double SixtyDegInRad = Math.PI / 3.0;
        private const int ViewBoxWidth = 310;
        private const int ViewBoxOffset = 10;
        private const int Center = ViewBoxWidth / 2;
        private const int OuterRadius = (ViewBoxWidth - ViewBoxOffset) / 2;
        private const int InnerRadius = OuterRadius / 3;

        public static void GenerateFlower()
        {

            HashSet<string> svgElements = new HashSet<string>();
            svgElements.Add(DrawCircle(Center, Center, OuterRadius)); // big outer
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
                Console.WriteLine(svgElement);
            }
        }

        const double letterHeight = InnerRadius / 3.0 + 0.5;
        const double letterWidth = letterHeight;
        const double thickness = letterHeight / 5.0;
        const double spacing = 10.0;

        public static void GenerateESME()
        {
            const double x = Center - (4 * letterWidth + 3 * spacing) / 2.0;
            const double y = Center - letterHeight / 2.0;
            List<string> svgElements = new List<string>();
            svgElements.AddRange(DrawE(x, y));
            svgElements.AddRange(DrawS(x + letterWidth + spacing, y));
            svgElements.AddRange(DrawM(x + 2 * letterWidth + 2 * spacing, y));
            svgElements.AddRange(DrawE(x + 3 * letterWidth + 3 * spacing, y));
            foreach (string svgElement in svgElements)
            {
                Console.WriteLine(svgElement);
            }
        }

        private static IEnumerable<string> DrawE(double x, double y, bool includeLeftRect = true)
        {
            yield return $"                <rect x=\"{x:0.####}\" y=\"{y:0.####}\" width=\"{letterWidth:0.####}\" height=\"{thickness:0.####}\" fill=\"black\" />";
            yield return $"                <rect x=\"{x:0.####}\" y=\"{y + letterHeight - thickness:0.####}\" width=\"{letterWidth:0.####}\" height=\"{thickness:0.####}\" fill=\"black\" />";
            yield return $"                <rect x=\"{x:0.####}\" y=\"{y + letterHeight / 2.0 - thickness / 2.0:0.####}\" width=\"{letterWidth:0.####}\" height=\"{thickness:0.####}\" fill=\"black\" />";
            if (includeLeftRect)
            {
                yield return $"                <rect x=\"{x:0.####}\" y=\"{y:0.####}\" width=\"{thickness:0.####}\" height=\"{letterHeight:0.####}\" fill=\"black\" />";
            }
        }

        private static IEnumerable<string> DrawS(double x, double y)
        {
            return DrawE(x, y, includeLeftRect: false);
        }

        private static IEnumerable<string> DrawM(double x, double y)
        {
            yield return $"                <rect x=\"{x:0.####}\" y=\"{y:0.####}\" width=\"{letterWidth:0.####}\" height=\"{thickness:0.####}\" fill=\"black\" />";
            yield return $"                <rect x=\"{x:0.####}\" y=\"{y:0.####}\" width=\"{thickness:0.####}\" height=\"{letterHeight:0.####}\" fill=\"black\" />";
            yield return $"                <rect x=\"{x + letterWidth - thickness:0.####}\" y=\"{y:0.####}\" width=\"{thickness:0.####}\" height=\"{letterHeight:0.####}\" fill=\"black\" />";
            yield return $"                <rect x=\"{x + letterWidth / 2.0 - thickness / 2.0:0.####}\" y=\"{y:0.####}\" width=\"{thickness:0.####}\" height=\"{letterHeight:0.####}\" fill=\"black\" />";
        }

        private static bool IsGreaterThan(double a, double b)
        {
            return a > b + 1.0;
        }

        private static double Distance((double x, double y) p1, (double x, double y) p2)
        {
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
        }

        private static IEnumerable<(double, double)> ComputeCenters(double X, double Y, double R, int level = 0)
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

        private static string DrawCircle(double x, double y, double r)
        {
            return $"            <circle cx=\"{x:0.####}\" cy=\"{y:0.####}\" r=\"{r}\" />";
        }

        private static string DrawArc(double x, double y, double radius, double startAngle, double endAngle)
        {
            var start = PolarToCartesian(x, y, radius, endAngle);
            var end = PolarToCartesian(x, y, radius, startAngle);

            string largeArcFlag = endAngle - startAngle <= Math.PI ? "0" : "1";

            var d = $"M {start.x:0.####} {start.y:0.####} A {radius} {radius} 0 {largeArcFlag} 0 {end.x:0.####} {end.y:0.####}";

            return $"            <path d=\"{d}\" />";
        }

        private static (double x, double y) PolarToCartesian(double centerX, double centerY, double radius, double angle)
        {
            return (centerX + (radius * Math.Cos(angle)), centerY + (radius * Math.Sin(angle)));
        }
    }
}
