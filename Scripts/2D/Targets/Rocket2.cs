using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Rocket2 : Target
	{
		private AnimatedSprite2D _animation;
		private Timer _timer;
		private float _selDestructionTime;

		private CollisionShape2D _rocketCollision;
		private CollisionShape2D _explosionCollision;

		private float _acceleration;

		private float _currentFuel;

		public Node2D FollowTarget { get; set; }
		
		private Label _destroyedLabel;
		public float SelfDestructionTime
		{
			set => _selDestructionTime = value + 0.1f;
			get => _selDestructionTime;
		}

		public override void _Ready()
		{
			_acceleration = Settings.Settings2D.RocketGun.RocketForce;
			StartPosition = new Vector2(Position.X, Position.Y);
			
			Weight = Settings.Settings2D.RocketGun.RocketMassWithoutFuel + Settings.Settings2D.RocketTarget.FuelMass;
			_currentFuel = Settings.Settings2D.RocketGun.FuelMass;
			DragCoefficient = Settings.Settings2D.RocketGun.AirResistance;
			StartVelocity = Settings.Settings2D.RocketGun.InitialVelocity;
			_animation = GetChild(0) as AnimatedSprite2D;
			_rocketCollision = GetChild(1) as CollisionShape2D;
			_explosionCollision = GetChild(2) as CollisionShape2D;
			_animation.Play("fly");
			
			_destroyedLabel = GetNode<Label>("../CanvasLayer/Statistics/ColorRect/DestroyedTargets");
		}

        public override void _IntegrateForces(PhysicsDirectBodyState2D state)
        {
            base._IntegrateForces(state);
			ConstantForce = Mass * (FollowTarget.GlobalPosition - GlobalPosition).Normalized() * _acceleration / Weight;
        }

        public override void _PhysicsProcess(double delta)
		{
			CurrentTime += (float)delta;
			if (_currentFuel > 0 && (FollowTarget.GlobalPosition - GlobalPosition).Length() > 10f)
			{
				_currentFuel -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
			}
			else
			{
				_currentFuel = 0;
				_acceleration = 0;
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


