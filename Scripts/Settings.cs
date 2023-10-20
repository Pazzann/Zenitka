using Godot;
using System;

namespace Zenitka
{
	public partial class Settings : Node
	{
		public bool Is2D = true;
		public bool IsGun = true;
		public bool IsDefaultTarget = true;


		public static class Settings2D
		{
			public static class DefaultGun
			{
				public static float BulletSpeed { get; set; } // швидкість снаряда
				public static float InitialElevationAngle{ get; set; } // початковий кут підвищення каналу ствола
				public static float AngularVelocity{ get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
				
				static DefaultGun()
				{
					// значения по умолчанию
				}
			}
			
			
		public static class RocketGun{
			public static Vector2 ZenithDetectionCoordinates{ get; set; } // координати виявлення відносно зенітної установки
			public static Vector2 TargetEndCoordinates{ get; set; } // координати кінцевої точки польоту цілі
			public static Vector2 InitialVelocity{ get; set; } // вектор початкової швидкості (напрям, величина у м/с)
			public static Vector2 ObjectSize{ get; set; } // розміри (довжина, товщина у м)
			public static float RocketMassWithoutFuel{ get; set; } // масса ракети без палива
			public static float FuelMass{ get; set; } // масса палива 
			public static float AngularVelocity{ get; set; } // кутова швидкість (1/с) (швидкість повороту)
			
			static RocketGun()
			{
				// значения по умолчанию
			}
		}
		
		public static class DefaultTarget
		{
			public static Vector2 Velocity{ get; set; } // Вектор швидкості (напрям, величина у м/с)
			public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
			public static double Mass{ get; set; } // маса
			public static double Altitude{ get; set; } // викривлення земної поверхні
			
			static DefaultTarget()
			{
				// значения по умолчанию
			}

		}

		
		public static class RocketTarget
		{
			public static double RocketAcceleration { get; set; } // прискорення, що надається ракетним двигуном (м/с2)
			public static double MaxAngularVelocity { get; set; } // максимальну кутову швидкість (швидкість повороту, 1/c)
			public static double DetonationDistance { get; set; }// дистанція підриву (м)
			public static double ShrapnelVelocity { get; set; } // швидкість розльоту осколків
			public static double RocketMassWithoutFuel { get; set; } // власна маса ракети
			public static double FuelMass { get; set; } // маса палива

			static RocketTarget()
			{
				// значения по умолчанию
			}
		}
	}
		
		
		public static class Settings3D
		{
			
		}
		
	}
}
