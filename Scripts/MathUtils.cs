
using System;
using Godot;

namespace Zenitka.Scripts
{
    public static class MathUtils {
        public static double Extend(double val, double delta) {
            return val + Mathf.Sign(val) * delta; 
        }
    }
} 

