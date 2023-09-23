using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Target : RigidBody3D
	{
		private bool _isOffscreen = false;
		
		private Rect2 _field = new Rect2(Vector2.Zero, new Vector2(20, 20));
		
		private bool _isWithinRange = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		public void ScheduleSelfDestroyWhenOffscreen()
		{
			_isOffscreen = _field.HasPoint(new Vector2(Position[0], Position[2]));
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		
		[Signal]
		public delegate void WentWithinRange3DEventHandler();
		
		private void OnVisibleOnScreenNotifier2dScreenEntered()
		{
			_isOffscreen = false;

			if (!_isWithinRange) {
				_isWithinRange = true;

				ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout)
					.OnCompleted(() => EmitSignal(SignalName.WentWithinRange3D));
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



