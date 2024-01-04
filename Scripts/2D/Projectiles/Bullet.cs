using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Bullet : BallisticBody
	{
		private AnimatedSprite2D _animation;

		private Timer _timer;
		private float _maxLifespan;

		private CollisionShape2D _bulletCollision;
		private CollisionShape2D _explosionCollision;

		public event Action<BallisticBody> OnExploded;

		public float MaxLifespan
		{
			set => _maxLifespan = value;
			get => _maxLifespan;
		}

		public override void _Ready()
		{
			_animation = (GetChild(0) as AnimatedSprite2D)!;
			_timer = (GetNode("SelfDestructionTimer") as Timer)!;
			_bulletCollision = (GetChild(1) as CollisionShape2D)!;
			_explosionCollision = (GetChild(3) as CollisionShape2D)!;

			_timer.WaitTime = MaxLifespan;
			_timer.Start();

			_animation.Play("fly2");

			base._Ready();
		}

		private void OnBodyEntered(Node body)
		{
			if (body is BallisticBody target)
			{
				OnExploded?.Invoke(target);
				target.Destroy();
			}
			else
				OnExploded?.Invoke(null);
			
			Explode();
		}

		private void SelfDestroy()
		{
			OnExploded?.Invoke(null);
			Explode();
		}

		private void Explode()
		{
			_animation.Play("explode");

			if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
				_animation.AnimationLooped += QueueFree;

			_bulletCollision.SetDeferred("disabled", true);
			_explosionCollision.SetDeferred("disabled", false);

			HasExploded = true;
		}
	}
}
