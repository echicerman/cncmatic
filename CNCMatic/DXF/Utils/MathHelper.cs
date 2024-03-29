﻿using System;
using System.Collections.Generic;
using DXF.Objetos;

namespace DXF.Utils
{
    /// <summary>
    /// Funciones y constantes de matematicas
    /// </summary>
    public class MathHelper
    {
        #region CoordinateSystem enum

        /// <summary>
        /// Defines the coordinate system reference.
        /// </summary>
        public enum CoordinateSystem
        {
            /// <summary>
            /// World coordinates.
            /// </summary>
            World,
            /// <summary>
            /// Object coordinates.
            /// </summary>
            Object
        }

        #endregion

        /// <summary>
        /// A doble precision number close to zero.
        /// </summary>
        public const double EpsilonD = 0.000000001d;

        /// <summary>
        /// A simple precision number close to zero.
        /// </summary>
        public const float EpsilonF = 0.00001f;

        /// <summary>
        /// Constant to transform an angle between degrees and radians.
        /// </summary>
        public const double DegToRad = Math.PI / 180.0;

        /// <summary>
        /// Constant to transform an angle between degrees and radians.
        /// </summary>
        public const double RadToDeg = 180.0 / Math.PI;

        /// <summary>
        /// PI/2 (90 degrees)
        /// </summary>
        public const double HalfPI = Math.PI * 0.5f;

        /// <summary>
        /// 2*PI (360 degrees)
        /// </summary>
        public const double TwoPI = 2 * Math.PI;

        /// <summary>
        /// Checks if a number is close to one.
        /// </summary>
        /// <param name="number">Simple precision number.</param>
        /// <param name="threshold">Tolerance.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        public static bool IsOne(float number, float threshold)
        {
            return IsZero(number - 1, threshold);
        }

        /// <summary>
        /// Checks if a number is close to one.
        /// </summary>
        /// <param name="number">Simple precision number.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        /// <remarks>By default a tolerance of the constant EPSILON_F will be used.</remarks>
        public static bool IsOne(float number)
        {
            return IsZero(number - 1);
        }

        /// <summary>
        /// Checks if a number is close to one.
        /// </summary>
        /// <param name="number">Double precision number.</param>
        /// <param name="threshold">Tolerance.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        public static bool IsOne(double number, double threshold)
        {
            return IsZero(number - 1, threshold);
        }

        /// <summary>
        /// Checks if a number is close to one.
        /// </summary>
        /// <param name="number">Double precision number.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        /// <remarks>By default a tolerance of the constant EPSILON_D will be used.</remarks>
        public static bool IsOne(double number)
        {
            return IsZero(number - 1);
        }

        /// <summary>
        /// Checks if a number is close to zero.
        /// </summary>
        /// <param name="number">Simple precision number.</param>
        /// <param name="threshold">Tolerance.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        public static bool IsZero(float number, float threshold)
        {
            return (number >= -threshold && number <= threshold);
        }

        /// <summary>
        /// Checks if a number is close to zero.
        /// </summary>
        /// <param name="number">Simple precision number.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        /// <remarks>By default a tolerance of the constant EPSILON_F will be used.</remarks>
        public static bool IsZero(float number)
        {
            return IsZero(number, EpsilonF);
        }

        /// <summary>
        /// Checks if a number is close to zero.
        /// </summary>
        /// <param name="number">Double precision number.</param>
        /// <param name="threshold">Tolerance.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        public static bool IsZero(double number, double threshold)
        {
            return number >= -threshold && number <= threshold;
        }

        /// <summary>
        /// Checks if a number is close to zero.
        /// </summary>
        /// <param name="number">Double precision number.</param>
        /// <returns>True if its close to one or false in anyother case.</returns>
        /// <remarks>By default a tolerance of the constant EPSILON_D will be used.</remarks>
        public static bool IsZero(double number)
        {
            return IsZero(number, EpsilonD);
        }

        /// <summary>
        /// Transforms a point between coordinate systems.
        /// </summary>
        /// <param name="point">Point to transform.</param>
        /// <param name="zAxis">Object normal vector.</param>
        /// <param name="from">Point coordinate system.</param>
        /// <param name="to">Coordinate system of the transformed point.</param>
        /// <returns>Transormed point.</returns>
        public static Vector3d Transform(Vector3d point, Vector3d zAxis, CoordinateSystem from, CoordinateSystem to)
        {
            Matrix3d trans = ArbitraryAxis(zAxis);
            if (from == CoordinateSystem.World && to == CoordinateSystem.Object)
            {
                trans = trans.Traspose();
                return trans * point;
            }
            if (from == CoordinateSystem.Object && to == CoordinateSystem.World)
            {
                return trans * point;
            }
            return point;
        }

        /// <summary>
        /// Transforms a point list between coordinate systems.
        /// </summary>
        /// <param name="points">Points to transform.</param>
        /// <param name="zAxis">Object normal vector.</param>
        /// <param name="from">Points coordinate system.</param>
        /// <param name="to">Coordinate system of the transformed points.</param>
        /// <returns>Transormed point list.</returns>
        public static IList<Vector3d> Transform(IList<Vector3d> points, Vector3d zAxis, CoordinateSystem from, CoordinateSystem to)
        {
            Matrix3d trans = ArbitraryAxis(zAxis);
            List<Vector3d> transPoints;
            if (from == CoordinateSystem.World && to == CoordinateSystem.Object)
            {
                transPoints = new List<Vector3d>();
                trans = trans.Traspose();
                foreach (Vector3d p in points)
                {
                    transPoints.Add(trans * p);
                }
                return transPoints;
            }
            if (from == CoordinateSystem.Object && to == CoordinateSystem.World)
            {
                transPoints = new List<Vector3d>();
                foreach (Vector3d p in points)
                {
                    transPoints.Add(trans * p);
                }
                return transPoints;
            }
            return points;
        }

        /// <summary>
        /// Gets the rotation matrix from the normal vector (extrusion direction) of an entity.
        /// </summary>
        /// <param name="zAxis">Normal vector.</param>
        /// <returns>Rotation matriz.</returns>
        public static Matrix3d ArbitraryAxis(Vector3d zAxis)
        {
            zAxis.Normalize();
            Vector3d wY = Vector3d.UnitarioY;
            Vector3d wZ = Vector3d.UnitarioZ;
            Vector3d aX;

            if ((Math.Abs(zAxis.X) < 1 / 64.0) && (Math.Abs(zAxis.Y) < 1 / 64.0))
                aX = Vector3d.CrossProduct(wY, zAxis);
            else
                aX = Vector3d.CrossProduct(wZ, zAxis);

            aX.Normalize();

            Vector3d aY = Vector3d.CrossProduct(zAxis, aX);
            aY.Normalize();

            return new Matrix3d(aX.X, aY.X, zAxis.X, aX.Y, aY.Y, zAxis.Y, aX.Z, aY.Z, zAxis.Z);
        }
    }
}
