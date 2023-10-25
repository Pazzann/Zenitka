using System.Data;
using Godot;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Shell : Target
	{
		public override void _Ready()
		{
			StartPosition = new Vector2(Position.X, Position.Y);
			ConstantAcceleration = 0;
			
			Weight = Settings.Settings2D.DefaultTarget.Mass;
			DragCoefficient = Settings.Settings2D.DefaultTarget.AirResistance;
			StartVelocity = Settings.Settings2D.DefaultTarget.Velocity;
		}
		public override void Destroy()
		{
			QueueFree();
		}
	}
}
