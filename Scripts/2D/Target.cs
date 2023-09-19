using Godot;
using System;

namespace Zenitka.Scripts._2D
{
	public partial class Target : RigidBody2D {
		private bool _isOffscreen = false;

		public override void _Ready()
		{

		}

		public void ScheduleSelfDestroyWhenOffscreen() {
			_isOffscreen = GetViewportRect().HasPoint(Position);
		}

		public override void _PhysicsProcess(double delta)
		{
			
		}
	
		private void OnVisibleOnScreenNotifier2dScreenEntered()
		{
			_isOffscreen = false;
		}

		private void OnVisibleOnScreenNotifier2dScreenExited()
		{
			if (!_isOffscreen) {
				GD.Print("target self-destroyed as it went offscreen");
				QueueFree();
			}
		}
	}
}
