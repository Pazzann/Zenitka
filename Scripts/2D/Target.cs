using Godot;
using System;

namespace Zenitka.Scripts._2D
{
	public partial class Target : RigidBody2D {
		private bool _isOffscreen = false;
		private bool _isWithinRange = false;

		public override void _Ready()
		{

		}

		public void ScheduleSelfDestroyWhenOffscreen() {
			_isOffscreen = GetViewportRect().HasPoint(Position);
		}

		public override void _PhysicsProcess(double delta)
		{
			
		}

		[Signal]
		public delegate void WentWithinRangeEventHandler();
	
		private void OnVisibleOnScreenNotifier2dScreenEntered()
		{
			_isOffscreen = false;

			if (!_isWithinRange) {
				_isWithinRange = true;

				ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout)
					.OnCompleted(() => EmitSignal(SignalName.WentWithinRange));
			}
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
