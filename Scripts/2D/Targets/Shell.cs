using System.Data;
using Godot;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Shell : Target
	{
		private AnimatedSprite2D _animation;
		private CollisionShape2D _rocketCollision;

		public override void _Ready()
		{
			_animation = GetChild(1) as AnimatedSprite2D;

			_animation.Play("fly");
			_rocketCollision = GetChild(0) as CollisionShape2D;

			_state.Velocity = _state.Velocity.Normalized() * Settings.Settings2D.DefaultTarget.Velocity;
			_state.ConstantAcceleration = Vector2.Down * Settings.Settings2D.Gravity;
			_state.LinearDrag = Settings.Settings2D.DefaultTarget.AirResistance;
			_state.SelfPropellingAcceleration = 0f;
			_state.Mass = Settings.Settings2D.DefaultTarget.Mass;

			UseNumericalIntegration = false;

			Reset();
		}

		public override void Destroy()
		{
			_rocketCollision.Disabled = true;
			_animation.Play("explode");

			_animation.Connect("animation_looped", Callable.From(QueueFree));

			IsExploded = true;
		}
	}
}
