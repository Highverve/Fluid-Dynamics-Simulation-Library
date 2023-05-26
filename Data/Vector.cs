using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidDynamics.Data
{
    //References: https://mathinsight.org/vector_introduction
    /// <summary>
    /// A simplified form of MonoGame.Framework's Vector3.
    /// </summary>
    public struct Vector
    {
        #region Statics
        public static readonly Vector Zero = new Vector(0.0, 0.0, 0.0);
        public static readonly Vector One = new Vector(1.0, 1.0, 1.0);
        public static readonly Vector Up = new Vector(0.0, 1.0, 0.0);
        public static readonly Vector Down = new Vector(0.0, -1.0, 0.0);
        public static readonly Vector Left = new Vector(-1.0, 0.0, 0.0);
        public static readonly Vector Right = new Vector(0.0, 1.0, 0.0);
        public static readonly Vector Forward = new Vector(0.0, 0.0, -1.0);
        public static readonly Vector Backward = new Vector(0.0, 0.0, 1.0);
        #endregion

        #region Variables and constructors
        public double X { get; set; } = 0.0;
        public double Y { get; set; } = 0.0;
        public double Z { get; set; } = 0.0;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(double x, double y) : this(x, y, 0) { }
        public Vector(double value) : this(value, value, value) { }
        #endregion

        #region Operators
        public static bool operator ==(Vector value1, Vector value2)
        {
            return (value1.X == value2.X) &&
                (value1.Y == value2.Y) &&
                (value1.Z == value2.Z);
        }
        public static bool operator !=(Vector value1, Vector value2)
        {
            return (value1 == value2) == false;
        }
        public static bool operator >(Vector value1, Vector value2)
        {
            return (value1.X > value2.X) &&
                (value1.Y > value2.Y) &&
                (value1.Z > value2.Z);
        }
        public static bool operator <(Vector value1, Vector value2)
        {
            return (value1.X < value2.X) &&
                (value1.Y < value2.Y) &&
                (value1.Z < value2.Z);
        }

        public static Vector operator +(Vector value1, Vector value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;

            return value1;
        }
        public static Vector operator -(Vector value)
        {
            return new Vector(0.0 - value.X, 0.0 - value.Y, 0.0 - value.Z);
        }
        public static Vector operator -(Vector value1, Vector value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }
        public static Vector operator *(Vector value1, Vector value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }
        public static Vector operator *(Vector value1, double factor)
        {
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }
        public static Vector operator /(Vector value1, Vector value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }
        public static Vector operator /(Vector value1, double factor)
        {
            double percent = 1.0 / factor;
            value1.X *= percent;
            value1.Y *= percent;
            value1.Z *= percent;
            return value1;
        }
        #endregion

        #region Restrictions
        public void Floor()
        {
            X = Math.Floor(X);
            Y = Math.Floor(Y);
            Z = Math.Floor(Z);
        }
        public void Round()
        {
            X = Math.Round(X);
            Y = Math.Round(Y);
            Z = Math.Round(Z);
        }
        public void Min(Vector value)
        {
            X = Math.Min(X, value.X);
            Y = Math.Min(Y, value.Y);
            Z = Math.Min(Z, value.Z);
        }
        public void Max(Vector value)
        {
            X = Math.Max(X, value.X);
            Y = Math.Max(Y, value.Y);
            Z = Math.Max(Z, value.Z);
        }
        public void Clamp(Vector min, Vector max)
        {
            X = Math.Clamp(X, min.X, max.X);
            Y = Math.Clamp(Y, min.Y, max.Y);
            Z = Math.Clamp(Z, min.Z, max.Z);
        }
        #endregion

        #region Methods
        public void Normalize()
        {
            double number = 1.0 / Length();
            X *= number;
            Y *= number;
            Z *= number;
        }
        public Vector Cross(Vector value)
        {
            double x = (Y * value.Z) - (value.Y * Z);
            double y = 0 - ((X * value.Z) - (value.X * Z));
            double z = (X * value.Y) - (value.X * Y);
            return new Vector(x, y, z);
        }
        public double Dot(Vector value)
        {
            return X * (value.X) + (Y * value.Y) + (Z * value.Z);
        }
        public Vector Reflect(Vector value, Vector normal)
        {
            double number = (value.X * normal.X) + (value.Y * normal.Y) + (value.Z * normal.Z);
            return new Vector(
                value.X - 2.0 * normal.X * number,
                value.Y - 2.0 * normal.Y * number,
                value.Z - 2.0 * normal.Z * number);
        }
        public void Negate()
        {
            X = 0 - X;
            Y = 0 - Y;
            Z = 0 - Z;
        }
        public Vector Interpolate(Vector target, double amount)
        {
            return new Vector(Interpolate(X, target.X, amount),
                Interpolate(Y, target.Y, amount),
                Interpolate(Z, target.Z, amount));
        }
        private double Interpolate(double value1, double value2, double amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }
        public double LengthSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }
        public double Distance(Vector target)
        {
            return Math.Sqrt(DistanceSquared(target));
        }
        public double DistanceSquared(Vector target)
        {
            double compareX = X - target.X;
            double compareY = Y - target.Y;
            double compareZ = Z - target.Z;
            return (compareX * compareX) + (compareY * compareY) + (compareZ * compareZ);
        }

        public bool Within(Vector positionA, Vector positionB)
        {
            return (this > positionA) && (this < positionB);
        }
        public bool Without(Vector positionA, Vector positionB)
        {
            return (this < positionA) && (this > positionB);
        }
        public bool IsZero() { return this == Zero; }
        public bool IsOne() { return this == One; }
        #endregion

        #region Overrides
        public override int GetHashCode()
        {
            return (((X.GetHashCode() * 397) ^ Y.GetHashCode()) * 397) ^ Z.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Vector)
                return Equals((Vector)obj);
            return false;
        }
        public bool Equals(Vector vector)
        {
            return (X == vector.X && Y == vector.Y);
        }
        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }
        #endregion
    }
}
