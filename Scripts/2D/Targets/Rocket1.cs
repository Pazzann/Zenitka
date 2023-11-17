using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Rocket1 : BallisticBody
	{
		private AnimatedSprite2D _animation;
		private CollisionShape2D _rocketCollision;
		private float _currentFuel;

		public override void _Ready()
		{
			_currentFuel = Settings.Settings2D.RocketTarget.FuelMass;

			_animation = GetChild(0) as AnimatedSprite2D;
			_rocketCollision = GetChild(1) as CollisionShape2D;
			_animation.Play("fly");

			_state.Velocity = _state.Velocity.Normalized() * Settings.Settings2D.RocketTarget.StartVelocity;
			_state.ConstantAcceleration = Vector2.Down * Settings.Settings2D.Gravity;
			_state.LinearDrag = Settings.Settings2D.RocketTarget.AirResistance;
			_state.SelfPropellingAcceleration = Settings.Settings2D.RocketTarget.RocketAcceleration;
			_state.Mass = Settings.Settings2D.RocketTarget.RocketMassWithoutFuel + Settings.Settings2D.RocketTarget.FuelMass;

			UseNumericalIntegration = true;

			Reset();
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);

			if (_currentFuel > 0)
			{
				_currentFuel -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
			}
			else
			{
				if (IsExploded)
					return;
				_currentFuel = 0;
				_state.SelfPropellingAcceleration = 0f;
				Destroy();
			}

			_state.Mass = Settings.Settings2D.RocketTarget.RocketMassWithoutFuel + _currentFuel;
		}
		private void _on_button_pressed()
		{
			Destroy();
		}

		public override void Destroy()
		{
			_animation.Play("explode");

			_animation.Connect("animation_looped", Callable.From(QueueFree));
			_rocketCollision.Disabled = true;
			IsExploded = true;
		}
	}
}




