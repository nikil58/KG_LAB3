using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    public class Point3D
    {
        /*
         * 
         * Bezie 3rd  -  t^3 + 3*t^2*(1-t) + 3*t*(1-t)^2 + (1-t)^3 = 1
         * 
         * point on curve  -  P1*t^3 + P2*3*t^2*(1-t) + P3*3*t*(1-t)^2 + P4*(1-t)^3 = Pnew
         * 
         */

        public float X, Y, Z;

        public Point3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point3D(double [] coordinates)
        {
            this.X = (float)coordinates[0];
            this.Y = (float)coordinates[1];
            this.Z = (float)coordinates[2];
        }
        public static Point3D operator +(Point3D a, Point3D b) => new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Point3D operator -(Point3D a, float d) => new Point3D(a.X - d, a.Y - d, a.Z - d);
        public static Point3D operator -(Point3D a, Point3D b) => new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Point3D operator *(float d, Point3D a) => new Point3D(a.X * d, a.Y * d, a.Z * d);
        public static Point3D operator *(Point3D a, float d) => new Point3D(a.X * d, a.Y * d, a.Z * d);
        public static Point3D operator *(Point3D a, Point3D b) => new Point3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Point3D operator /(Point3D a, float d) => new Point3D(a.X / d, a.Y / d, a.Z / d);
        public static Point3D operator /(Point3D a, Point3D b) => new Point3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        //returns basic coords
        public float GetX()
        {
            return X;
        }
        public float GetY()
        {
            return Y;
        }
        public float GetZ()
        {
            return Z;
        }

        public void SetX(float x)
        {
            this.X = x;
        }
        public void SetY(float y)
        {
            this.Y = y;
        }
        public void SetZ(float z)
        {
            this.Z = z;
        }

        //point addition (+)
        public static Point3D PointAdd(Point3D pointOne, Point3D pointTwo)
        {
            return new Point3D(pointOne.X + pointTwo.X, pointOne.Y + pointTwo.Y, pointOne.Z + pointTwo.Z);
        }
        //multiply to a constant
        public static Point3D PointTimes(Point3D point, float constant)
        {
            return new Point3D(point.X * constant, point.Y * constant, point.Z * constant);
        }

    }
}
