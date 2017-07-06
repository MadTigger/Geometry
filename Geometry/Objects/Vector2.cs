using System;
using System.Diagnostics;

namespace Geometry
{
    [DebuggerDisplay("Vector2 {_X},{_Y}")]
    public class Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);

        private float _X;
        private float _Y;

        public float X
        {
            get { return _X; }
            set { _X = value; }
        }

        public float Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public Vector2() : this(0, 0) { }

        public Vector2(Vector2 v1) : this(v1.X, v1.Y) { }

        public Vector2(Point2 p1) : this(p1.X, p1.Y) { }

        public Vector2(Point2 head, Point2 destination) : this(destination.X - head.X, destination.Y - head.Y) { }

        public Vector2(double rotation) : this((float)Math.Cos(rotation), (float)Math.Sin(rotation)) { }

        public Vector2(float x, float y)
        {
            _X = x;
            _Y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, float s2)
        {
            return new Vector2(v1.X * s2, v1.Y * s2);
        }

        public static Vector2 operator /(Vector2 v1, float s2)
        {
            return new Vector2(v1.X / s2, v1.Y / s2);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            if (ReferenceEquals(v1, v2))
                return true;

            // If one is null, but not both, return false.
            if (((object)v1 == null) || ((object)v2 == null))
                return false;

            return ((v1._X == v2._X) && (v1._Y == v2._Y));
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        public static Vector2 Normalize(Vector2 v1)
        {
            var length = v1.Length();

            if (length == 0)
                throw new DivideByZeroException("Cannot normalize a vector when it's magnitude is zero");

            float inverse = 1f / length;

            return new Vector2
            (
                v1.X * inverse,
                v1.Y * inverse
            );
        }

        public void Normalize()
        {
            float length = Length();

            if (length == 0)
                throw new DivideByZeroException("Cannot normalize a vector when it's magnitude is zero");

            float inverse = 1f / length;

            _X *= inverse;
            _Y *= inverse;
        }

        public static float DistanceTo(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
        }

        public float DistanceTo(Vector2 other)
        {
            return DistanceTo(this, other);
        }

        public float Length()
        {
            return (float)Math.Sqrt((float)(_X * _X) + (_Y * _Y));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return _X.GetHashCode() ^ (_Y.GetHashCode() * 17);
            }
        }

        public override string ToString()
        {
            return $"({_X},{_Y})";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            Vector2 new_obj = (Vector2)obj;
            return Equals(new_obj);
        }

        public bool Equals(Vector2 other)
        {
            return other._X == _X && other._Y == _Y;
        }

        public float VectorToRotation()
        {
            return (float)Math.Atan2(_Y, _X);
        }
    }
}