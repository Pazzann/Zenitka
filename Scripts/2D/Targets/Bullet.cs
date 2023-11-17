using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Bullet : BallisticBody
	{
		private AnimatedSprite2D _animation;
		private Timer _timer;
		private float _selDestructionTime;

		private CollisionShape2D _bulletCollision;
		private CollisionShape2D _explosionCollision;

		private bool _scoreUpdated = false;

		private Label _destroyedLabel;
		public float SelfDestructionTime
		{
			set => _selDestructionTime = value + 0.1f;
			get => _selDestructionTime;
		}

		public override void _Ready()
		{
			_animation = GetChild(0) as AnimatedSprite2D;
			_timer = GetChild(2) as Timer;
			_bulletCollision = GetChild(1) as CollisionShape2D;
			_explosionCollision = GetChild(3) as CollisionShape2D;
			_timer.WaitTime = ((double)SelfDestructionTime);
			_timer.Start();
			_animation.Play("fly2");

			_destroyedLabel = GetNode<Label>("../CanvasLayer/Statistics/ColorRect/DestroyedTargets");

			_state.Position = GlobalPosition;
			_state.Velocity = _state.Velocity.Normalized() * Settings.Settings2D.DefaultGun.BulletSpeed;
			_state.ConstantAcceleration = Vector2.Down * Settings.Settings2D.Gravity;
			_state.LinearDrag = Settings.Settings2D.DefaultGun.AirResistance;
			_state.SelfPropellingAcceleration = 0f;
			_state.Mass = Settings.Settings2D.DefaultGun.BulletMass;

			UseNumericalIntegration = true;

			Reset();
		}

		private void OnBodyEntered(Node body)
		{
			(body as BallisticBody).Destroy();
			_on_suicide_timer_timeout();

			if (!_scoreUpdated)
			{
				_destroyedLabel.Text = (Int32.Parse(_destroyedLabel.Text) + 1).ToString();
				_scoreUpdated = true;
			}
		}

		private void _on_suicide_timer_timeout()
		{
			_animation.Play("explode");

			_animation.Connect("animation_looped", Callable.From(QueueFree));
			_bulletCollision.Disabled = true;
			_explosionCollision.Disabled = false;
			IsExploded = true;
		}

		public override void Destroy()
		{
			QueueFree();
		}
	}
}


