using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Bullet : Target
	{
		private AnimatedSprite2D _animation;
		private Timer _timer;
		private float _selDestructionTime;

		private CollisionShape2D _bulletCollision;
		private CollisionShape2D _explosionCollision;
		
		private Label _destroyedLabel;
		public float SelfDestructionTime
		{
			set => _selDestructionTime = value + 0.1f;
			get => _selDestructionTime;
		}

		public override void _Ready()
		{
			StartPosition = new Vector2(Position.X, Position.Y);
			ConstantAcceleration = 0;
			
			
			Weight = Settings.Settings2D.DefaultGun.BulletMass;
			DragCoefficient = Settings.Settings2D.DefaultGun.AirResistance;
			StartVelocity = Settings.Settings2D.DefaultGun.BulletSpeed;
			_animation = GetChild(0) as AnimatedSprite2D;
			_timer = (GetChild(2) as Timer);
			_bulletCollision = GetChild(1) as CollisionShape2D;
			_explosionCollision = GetChild(3) as CollisionShape2D;
			_timer.WaitTime = ((double)SelfDestructionTime);
			_timer.Start();
			_animation.Play("fly2");
			
			_destroyedLabel = GetNode<Label>("../CanvasLayer/Statistics/ColorRect/DestroyedTargets");
		}

		private void OnBodyEntered(Node body)
		{
			body.QueueFree();
			_on_suicide_timer_timeout();
			_destroyedLabel.Text = (Int32.Parse(_destroyedLabel.Text)  + 1).ToString();
		}

		private void _on_suicide_timer_timeout()
		{
			_animation.Play("explode");
			
			_animation.Connect("animation_looped", Callable.From(_destroy));
			_bulletCollision.Disabled = true;
			_explosionCollision.Disabled = false;
			IsExploded = true;
		}

		private void _destroy()
		{
			QueueFree();
		}
	}
}


