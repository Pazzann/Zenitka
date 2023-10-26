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
			StartPosition = new Vector2(Position.X, Position.Y);
			ConstantAcceleration = 0;
			
			_animation = GetChild(1) as AnimatedSprite2D;
			
			Weight = Settings.Settings2D.DefaultTarget.Mass;
			DragCoefficient = Settings.Settings2D.DefaultTarget.AirResistance;
			StartVelocity = Settings.Settings2D.DefaultTarget.Velocity;
			
			_animation.Play("fly");
			_rocketCollision = GetChild(0) as CollisionShape2D;
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
