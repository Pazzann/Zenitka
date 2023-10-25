using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Rocket1 : Target
	{
		private AnimatedSprite2D _animation;
		private Timer _timer;
		private float _selDestructionTime;

		private CollisionShape2D _rocketCollision;
		private CollisionShape2D _explosionCollision;

		private float _currentFuel;
		
		private Label _destroyedLabel;
		public float SelfDestructionTime
		{
			set => _selDestructionTime = value + 0.1f;
			get => _selDestructionTime;
		}

		public override void _Ready()
		{
			ConstantAcceleration = Settings.Settings2D.RocketTarget.RocketAcceleration;
			StartPosition = new Vector2(Position.X, Position.Y);
			
			
			Weight = Settings.Settings2D.RocketTarget.RocketMassWithoutFuel + Settings.Settings2D.RocketTarget.FuelMass;
			_currentFuel = Settings.Settings2D.RocketTarget.FuelMass;
			DragCoefficient = Settings.Settings2D.RocketTarget.AirResistance;
			StartVelocity = Settings.Settings2D.RocketTarget.StartVelocity;
			_animation = GetChild(0) as AnimatedSprite2D;
			_rocketCollision = GetChild(1) as CollisionShape2D;
			_explosionCollision = GetChild(2) as CollisionShape2D;
			_animation.Play("fly");
			
			_destroyedLabel = GetNode<Label>("../CanvasLayer/Statistics/ColorRect/DestroyedTargets");
		}
		
		public override void _PhysicsProcess(double delta)
		{
			CurrentTime += (float)delta;
			if (_currentFuel > 0)
			{
				_currentFuel -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
			}
			else
			{
				_currentFuel = 0;
				ConstantAcceleration = 0;
				_destroy();
			}
			
			
			Weight = Settings.Settings2D.RocketTarget.RocketMassWithoutFuel + _currentFuel;
		}
		
		private void _destroy()
		{
			_animation.Play("explode");
			
			_animation.Connect("animation_looped", Callable.From(QueueFree));
			_rocketCollision.Disabled = true;
			_explosionCollision.Disabled = false;
			IsExploded = true;
		}
	}
}


