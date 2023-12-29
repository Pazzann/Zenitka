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
			public static bool IsNotDefaultGun { get; set; }
			public static bool IsNotDefaultTarget { get; set; }
			public static float Gravity { get; set; } // Гравітація
			public static int Random { get; set; } // Коєфіціент випадковості

			static Settings2D()
			{
				IsNotDefaultGun = false;
				IsNotDefaultTarget = false;
				Gravity = 9.8f;
				Random = 0;
			}

			public static class DefaultGun
			{
				public static float BulletSpeed { get; set; } // швидкість снаряда
				public static float InitialElevationAngle { get; set; } // початковий кут підвищення каналу ствола
				public static float AngularVelocity { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
				public static float BulletMass { get; set; } // Масса пули
				public static float AirResistance { get; set; } // Коєфіціент опору повітря
				public static int SalvoSize { get; set; } // Розмір залпа

				static DefaultGun()
				{
					BulletSpeed = 1000.0f;
					InitialElevationAngle = 0.0f;
					AngularVelocity = 1.0f;
					BulletMass = 1.0f;
					AirResistance = 0.05f;
					SalvoSize = 1;
				}
			}

			public static class RocketGun
			{
				public static Vector2
					ZenithDetectionCoordinates
				{ get; set; } // координати виявлення відносно зенітної установки //torad

				//public static Vector2 TargetEndCoordinates{ get; set; } // координати кінцевої точки польоту цілі
				public static float
					InitialVelocity
				{ get; set; } // вектор початкової швидкості (напрям, величина у м/с)
				public static float
					RocketForce
				{ get; set; } // сила тяги, що надається ракетним двигуном (кг * м / с2)

				//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
				public static float RocketMassWithoutFuel { get; set; } // масса ракети без палива
				public static float FuelMass { get; set; } // масса палива 
				public static float AngularVelocity { get; set; } // кутова швидкість (1/с) (швидкість повороту)
				public static float AirResistance { get; set; } // Коєфіціент опору повітря
				public static float FuelCost { get; set; } // Затрата топлива

				static RocketGun()
				{
					InitialVelocity = 1000f;
					RocketForce = 4000f;
					RocketMassWithoutFuel = 1f;
					FuelMass = 4;
					FuelCost = 0.1f;
					AngularVelocity = 4.0f;
				}
			}

			public static class DefaultTarget
			{
				public static float Velocity { get; set; } // Вектор швидкості (напрям, величина у м/с)

				//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
				public static float Mass { get; set; } // маса

				//public static float Altitude{ get; set; } // викривлення земної поверхні
				public static float AirResistance { get; set; } // Коєфіціент опору повітря
				public static float Acceleration { get; set; } // прискорення

				static DefaultTarget()
				{
					Velocity = 1000.0f;
					Mass = 1.0f;
					AirResistance = 0.05f;
				}
			}

			public static class RocketTarget
			{
				public static float
					RocketAcceleration
				{ get; set; } // прискорення, що надається ракетним двигуном (м/с2)


				//remove
				public static float
					MaxAngularVelocity
				{ get; set; } // максимальну кутову швидкість (швидкість повороту, 1/c)
				public static float StartVelocity { get; set; } // Стартовая скорость
				public static float FuelCost { get; set; } // Затрата топлива

				public static float DetonationDistance { get; set; } // дистанція підриву (м)
				public static float ShrapnelVelocity { get; set; } // швидкість розльоту осколків
				public static float RocketMassWithoutFuel { get; set; } // власна маса ракети
				public static float FuelMass { get; set; } // маса палива
				public static float AirResistance { get; set; } // Коєфіціент опору повітря

				static RocketTarget()
				{
					RocketAcceleration = 200.0f;
					StartVelocity = 1000.0f;
					FuelCost = 0.1f;
					DetonationDistance = 1.0f;
					ShrapnelVelocity = 4.0f;
					RocketMassWithoutFuel = 1;
					FuelMass = 4f;
					AirResistance = 0.05f;
				}
			}
		}


		public static class Settings3D
		{
			public static bool IsNotDefaultGun { get; set; }
			public static bool IsNotDefaultTarget { get; set; }
			public static float Gravity { get; set; } // Гравітація
			public static class DefaultGun
			{
				public static float BulletSpeed { get; set; } // швидкість снаряда
				public static float InitialElevationAngle { get; set; } // початковий кут підвищення каналу ствола
				public static float HRotSpeed { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
				public static float VRotSpeed { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)

				public static float BulletMass { get; set; } // Масса пули

				public static float AirResistance { get; set; } // Коєфіціент опору повітря
				public static float Gravity { get; set; } // Гравітація

				static DefaultGun()
				{
					BulletSpeed = 20.0f;
					InitialElevationAngle = 0.0f;
					HRotSpeed = 3f;
					VRotSpeed = 3f;
					BulletMass = 1f;
					AirResistance = 0.05f;
				}
			}

			public static class RocketGun
			{
				public static Vector2
					ZenithDetectionCoordinates
				{ get; set; } // координати виявлення відносно зенітної установки //torad

				//public static Vector2 TargetEndCoordinates{ get; set; } // координати кінцевої точки польоту цілі
				public static float
					InitialVelocity
				{ get; set; } // вектор початкової швидкості (напрям, величина у м/с)

				//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
				public static float RocketMassWithoutFuel { get; set; } // масса ракети без палива
				public static float FuelMass { get; set; } // масса палива 
				public static float AngularVelocityX { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
				public static float AngularVelocityY { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
				public static float AirResistance { get; set; } // Коєфіціент опору повітря
				public static float Gravity { get; set; } // Гравітація

				static RocketGun()
				{


				}
			}

			public static class DefaultTarget
			{
				public static float Velocity { get; set; } // Вектор швидкості (напрям, величина у м/с)

				//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
				public static float Mass { get; set; } // маса

				//public static float Altitude{ get; set; } // викривлення земної поверхні
				public static float AirResistance { get; set; } // Коєфіціент опору повітря

				static DefaultTarget()
				{
					Velocity = 10.0f;
					Mass = 1f;
					AirResistance = 0.05f;
				}
			}

			public static class RocketTarget
			{
				public static float
					RocketAcceleration
				{ get; set; } // прискорення, що надається ракетним двигуном (м/с2)

				public static float
					MaxAngularVelocity
				{ get; set; } // максимальну кутову швидкість (швидкість повороту, 1/c)

				public static float DetonationDistance { get; set; } // дистанція підриву (м)
				public static float ShrapnelVelocity { get; set; } // швидкість розльоту осколків
				public static float RocketMassWithoutFuel { get; set; } // власна маса ракети
				public static float FuelMass { get; set; } // маса палива
				public static float AirResistance { get; set; } // Коєфіціент опору повітря

				static RocketTarget()
				{
					RocketAcceleration = 2.0f;

					// значения по умолчанию
				}
			}

		}
	}
}

