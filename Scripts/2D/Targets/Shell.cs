using System.Data;
using Godot;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Shell : BallisticBody
	{
		private AnimatedSprite2D _animation;
		private CollisionShape2D _rocketCollision;

		public override void _Ready()
		{
			_animation = (GetChild(1) as AnimatedSprite2D)!;
			_rocketCollision = (GetChild(0) as CollisionShape2D)!;
			
			_animation.Play("fly");

			base._Ready();
		}

		public override void Destroy()
		{
			if (HasExploded)
				return;
			
			_rocketCollision.SetDeferred("disabled", true);

			_animation.Play("explode");

			if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
				_animation.AnimationLooped += QueueFree;

			HasExploded = true;
		}

		private void OnBodyEntered(Node body)
		{
			if (body is StaticBody2D)
				Destroy();
		}
	}
}
