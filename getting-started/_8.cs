using System;

namespace _1;

public class _8
{
    /**
     * Write a class that holds 2 coordinates (x,y) which represents a rectangle. Write a method that
     * calculates the circumference and the area of the rectangle. X1 cannot be the same as X2, and Y1
     * cannot be the same as Y2.
     * 
     * Write a class that holds the center coordinate (x,y) and the radius of a circle with a method
     * that calculates the area of the circle. Use a field to store pi. The calculated area has to be
     * stored in a private field. Outside the class, you can only read the value of the area using the
     * getArea method.
     * The radius can only be a positive number.
     */
    public static void GetArea(string[] _)
    {
        Rectangle rec = new(1, 2, 10, 20);
        Console.WriteLine($"The rectangle has an area of {rec.Area} and a circumference of {rec.Circumference}");

        try
        {
            rec.TopLeft = new Coordinate(rec.BottomRight.X, rec.BottomRight.Y * 2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Circle cir = new(new Coordinate(10, 10), 50);
        Console.WriteLine($"The circle has an area of {cir.getArea()}");

        try
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Circle(new Coordinate(10, 10), 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private class Circle
    {
        private const double Pi = Math.PI;
        private double _area;
        private int _radius;
        public Coordinate Center;

        public Circle(Coordinate center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public int Radius
        {
            get => _radius;
            set
            {
                if (value <= 0) throw new ArgumentException($"{nameof(Radius)} can not be less than or equal to 0");
                _radius = value;
                _area = Radius * Radius * Pi;
            }
        }

        // ReSharper disable once InconsistentNaming
        public double getArea()
        {
            return _area;
        }
    }

    private class Rectangle
    {
        private Coordinate _bottomRight;
        private Coordinate _topLeft;

        public Rectangle(Coordinate topLeft, Coordinate bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Rectangle(int xTopLeft, int yTopLeft, int xBottomRight, int yBottomRight) :
            this(
                new Coordinate(xTopLeft, yTopLeft),
                new Coordinate(xBottomRight, yBottomRight)
            )
        {
        }

        public Coordinate TopLeft
        {
            get => _topLeft;
            set
            {
                if (BottomRight is not null && BottomRight.AreHorizontallyOrVerticallyAligned(value))
                    throw new ArgumentException(
                        $"{nameof(value)} could not be horizontally or vertically aligned with {nameof(BottomRight)}");
                _topLeft = value;
            }
        }

        public Coordinate BottomRight
        {
            get => _bottomRight;
            set
            {
                if (TopLeft is not null && TopLeft.AreHorizontallyOrVerticallyAligned(value))
                    throw new ArgumentException(
                        $"{nameof(value)} could not be horizontally or vertically aligned with {nameof(TopLeft)}");
                _bottomRight = value;
            }
        }

        public int Width => BottomRight.X - TopLeft.X;
        public int Height => BottomRight.Y - TopLeft.Y;

        public int Circumference => (Width + Height) * 2;

        public int Area => Width * Height;
    }

    private class Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool AreHorizontallyOrVerticallyAligned(Coordinate other)
        {
            return other.X == X || other.Y == Y;
        }
    }
}