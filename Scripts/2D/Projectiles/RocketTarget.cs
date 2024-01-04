using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class RocketTarget : BallisticBody
	{
		private AnimatedSprite2D _animation;
		private CollisionShape2D _rocketCollision;
		private float _currentFuel;

		public override void _Ready()
		{
			_animation = (GetChild(0) as AnimatedSprite2D)!;
			_rocketCollision = (GetChild(1) as CollisionShape2D)!;

			_currentFuel = Settings.Settings2D.RocketTarget.FuelMass;

			_animation.Play("fly");

			base._Ready();
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
				if (HasExploded)
					return;
				
				_currentFuel = 0;
				Destroy();
			}
		}

		private void _on_button_pressed()
		{
			Destroy();
		}

		public override void Destroy()
		{
			_animation.Play("explode");

			if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
				_animation.AnimationLooped += QueueFree;

			_rocketCollision.SetDeferred("disabled", true);

			HasExploded = true;
		}
	}
}
