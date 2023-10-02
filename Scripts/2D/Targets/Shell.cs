using Godot;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Shell : Target
	{
		public override void _Ready()
		{
			Weight = 1.0f;
			DragCoefficient = 0.05f;
			StartVelocity = 200.0f;
		}

	}
}
