using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion ShortestRotation(this Quaternion a, Quaternion b)
    {
        var predicate = Quaternion.Dot(a, b) < 0
            ? Multiply(b, -1)
            : b;

        return a * Quaternion.Inverse(predicate);
    }

    private static Quaternion Multiply(Quaternion input, float scalar)
    {
        return new Quaternion(
            input.x * scalar,
            input.y * scalar,
            input.z * scalar,
            input.w * scalar);
    }
}

