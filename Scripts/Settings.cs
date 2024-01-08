using System;
using Godot;

namespace Zenitka.Scripts;

public delegate void SettingsChanged();

public partial class Settings : Node
{
	public bool Is2D = true;
	public bool IsGun = true;
	public bool IsDefaultTarget = true;

	public static class Settings2D
	{
		public static event SettingsChanged OnSettingsChanged;
		
		public static void InvokeOnSettingsChanged()
		{
			OnSettingsChanged?.Invoke();
		}

		public static bool IsNotDefaultGun { get; set; }
		public static bool IsNotDefaultTarget { get; set; }
		public static float Gravity { get; set; } // Гравітація
		public static int Random { get; set; } // Коєфіціент випадковості

		public static Vector2 GravityVector => Gravity * Vector2.Down;
			
		public static bool Auto { get; set; }
		public static float TargetSpawnInterval { get; set; }

		static Settings2D()
		{
			IsNotDefaultGun = false;
			IsNotDefaultTarget = false;
			Auto = true;
			Gravity = 9.8f;
			Random = 0;
			TargetSpawnInterval = 3f;
		}

		public static class DefaultGun
		{
			public static float BulletSpeed { get; set; } // швидкість снаряда
			public static float InitialElevationAngle { get; set; } // початковий кут підвищення каналу ствола
			public static float AngularVelocity { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
			public static float BulletMass { get; set; } // Масса пули
			public static float AirResistance { get; set; } // Коєфіціент опору повітря
			public static int SalvoSize { get; set; } // Розмір залпа
			public static float FiringDelay { get; set; }
			public static float[] Zenitki { get; set; }
			public static int[] ZenitkiState { get; set; } // 0-відсутня 1-гармата 2-ракетниця
			public static float FireRate { get; set; }

			static DefaultGun()
			{
				FiringDelay = 0.04f;
				BulletSpeed = 1000.0f;
				InitialElevationAngle = 0.0f;
				AngularVelocity = 1.0f;
				BulletMass = 1.0f;
				AirResistance = 0.05f;
				SalvoSize = 1;
				Zenitki = new float[3];
				ZenitkiState = new int[3];
				FireRate = 600f;
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
				MainEThrust
			{ get; set; } // сила тяги, що надається ракетним двигуном (кг * м / с2)

			public static float SideEThrust => MainEThrust;

			//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
			public static float RocketMassWithoutFuel { get; set; } // масса ракети без палива
			public static float FuelMass { get; set; } // масса палива 
			public static float AngularVelocity { get; set; } // кутова швидкість (1/с) (швидкість повороту)
			public static float AirResistance { get; set; } // Коєфіціент опору повітря
			public static float FuelCost { get; set; } // Затрата топлива
			public static float SideEActivationDelay { get; set; }

			static RocketGun()
			{
				InitialVelocity = 1f;
				MainEThrust = 4000f;
				RocketMassWithoutFuel = 1f;
				FuelMass = 4;
				FuelCost = 0.1f;
				AngularVelocity = 4.0f;
				SideEActivationDelay = 0.7f; 
			}
		}

		public static class DefaultTarget
		{
			public static float Velocity { get; set; } // Вектор швидкості (напрям, величина у м/с)

			//public static Vector2 Size{ get; set; } // розміри (довжина, товщина у м)
			public static float Mass { get; set; } // маса

			//public static float Altitude{ get; set; } // викривлення земної поверхні
			public static float QDrag { get; set; } // Коєфіціент опору повітря
			public static float Acceleration { get; set; } // прискорення
				
			public static float CoordinateX { get; set; }
			public static float CoordinateY { get; set; }
				
			public static float Angle { get; set; }
			public static float FireRatePerMinute { get; set; }

			static DefaultTarget()
			{
				Velocity = 1000.0f;
				Mass = 1.0f;
				QDrag = 0.05f;
				FireRatePerMinute = 40f;
				CoordinateX = 0f;
				CoordinateY = 0f;
			}
		}

		public static class RocketTarget
		{
			public static float
				MainEThrust
			{ get; set; } // прискорення, що надається ракетним двигуном (м/с2)


			//remove
			public static float
				MaxAngularVelocity
			{ get; set; } // максимальну кутову швидкість (швидкість повороту, 1/c)
			public static float StartVelocity { get; set; } // Стартовая скорость
			public static float FuelCost { get; set; } // Затрата топлива

			public static float DetonationDistance { get; set; } // дистанція підриву (м)
			public static float ShrapnelVelocity { get; set; } // швидкість розльоту осколків
			public static float BaseMass { get; set; } // власна маса ракети
			public static float FuelMass { get; set; } // маса палива
			public static float QDrag { get; set; } // Коєфіціент опору повітря

			static RocketTarget()
			{
				MainEThrust = 200.0f;
				StartVelocity = 1000.0f;
				FuelCost = 0.1f;
				DetonationDistance = 1.0f;
				ShrapnelVelocity = 4.0f;
				BaseMass = 1;
				FuelMass = 4f;
				QDrag = 0.05f;
			}
		}
	}
	
	public static class Settings3D
	{
		public static event SettingsChanged OnSettingsChanged;
		
		public static void InvokeOnSettingsChanged()
		{
			OnSettingsChanged?.Invoke();
		}
			
		public static bool Auto { get; set; }
		public static bool IsNotDefaultGun { get; set; }
		public static bool IsNotDefaultTarget { get; set; }
		public static float Gravity { get; set; } // Гравітація
		public static float TargetSpawnInterval { get; set; }

		static Settings3D()
		{
			Auto = true;
			TargetSpawnInterval = 3f;
		}
		public static class DefaultGun
		{
			public static float BulletSpeed { get; set; } // швидкість снаряда
			public static float InitialElevationAngle { get; set; } // початковий кут підвищення каналу ствола
			public static float HRotSpeed { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)
			public static float VRotSpeed { get; set; } // Кутова швидкість змінення напрямку ствола (1/с)

			public static float BulletMass { get; set; } // Масса пули

			public static float AirResistance { get; set; } // Коєфіціент опору повітря
			public static float Gravity { get; set; } // Гравітація

			public static float[] ZenitkiX { get; set; }
			public static float[] ZenitkiZ { get; set; }
			public static float[] ZenitkiState { get; set; }

			static DefaultGun()
			{
				BulletSpeed = 20.0f;
				InitialElevationAngle = 0.0f;
				HRotSpeed = 3f;
				VRotSpeed = 3f;
				BulletMass = 1f;
				AirResistance = 0.05f;
				ZenitkiState = new float[3];
				ZenitkiX = new float[3];
				ZenitkiZ = new float[3];
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
			public static float CoordinateX { get; set; }
			public static float CoordinateY { get; set; }
			public static float CoordinateZ { get; set; }
			public static float Angle1 { get; set; }
			public static float Angle2 { get; set; }

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
