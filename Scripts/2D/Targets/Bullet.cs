using Godot;
namespace Zenitka.Scripts._2D.Targets
{
	public partial class Bullet : Target
	{
		public override void _Ready()
		{
			Weight = 1.0f;
			DragCoefficient = 0.05f;
			StartVelocity = 2000.0f;
		}
		private void OnBodyEntered(Node body)
		{
			body.QueueFree();
			QueueFree();
		}
	}
}

