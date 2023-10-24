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
				
				public static float BulletMass { get; set; } // Масса пули
				
				static DefaultGun()
				{
					BulletSpeed = 1000.0f;
					InitialElevationAngle = 0.0f;
					AngularVelocity = 1.0f;
				}
			}
			
			
		public static class RocketGun{
			public static Vector2 ZenithDetectionCoordinates{ get; set; } // координати виявлення відносно зенітної установки //torad
			//public static Vector2 TargetEndCoordinates{ get; set; } // координати кінцевої точки польоту цілі
			public static Vector2 InitialVelocity{ get; set; } // вектор початкової швидкості (напрям, величина у м/с)
			//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
			public static float RocketMassWithoutFuel{ get; set; } // масса ракети без палива
			public static float FuelMass{ get; set; } // масса палива 
			public static float AngularVelocity{ get; set; } // кутова швидкість (1/с) (швидкість повороту)
			
			static RocketGun()
			{
				InitialVelocity = new Vector2(0f, -1000f);
				AngularVelocity = 1.0f;
			}
		}
		
		public static class DefaultTarget
		{
			public static float Velocity{ get; set; } // Вектор швидкості (напрям, величина у м/с)
			//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
			public static float Mass{ get; set; } // маса
			//public static float Altitude{ get; set; } // викривлення земної поверхні
			
			static DefaultTarget()
			{
				Velocity = 1000.0f;
				Mass = 10.0f;
				
			}

		}

		
		public static class RocketTarget
		{
			public static float RocketAcceleration { get; set; } // прискорення, що надається ракетним двигуном (м/с2)
			public static float MaxAngularVelocity { get; set; } // максимальну кутову швидкість (швидкість повороту, 1/c)
			public static float DetonationDistance { get; set; }// дистанція підриву (м)
			public static float ShrapnelVelocity { get; set; } // швидкість розльоту осколків
			public static float RocketMassWithoutFuel { get; set; } // власна маса ракети
			public static float FuelMass { get; set; } // маса палива

			static RocketTarget()
			{
				RocketAcceleration = 2.0f;
				
				// значения по умолчанию
			}
		}
	}
		
		
		public static class Settings3D
		{
			
		}
		
	}
}
