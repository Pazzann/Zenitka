using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Target : RigidBody3D {
		private bool _isDetectable = false;
		private bool _isWithinRange = false;

		float _detectionRange = 0f;

		public override void _Ready()
		{}

		public void SetDetectionRange(float range) {
			_detectionRange = range;
		}

		public void ScheduleSelfDestroyWhenOffscreen() {
			_isDetectable = Position.LengthSquared() <= _detectionRange * _detectionRange;
		}

		public override void _Process(double delta)
		{
			if (Position.LengthSquared() <= _detectionRange * _detectionRange) {
				ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout)
					.OnCompleted(() => EmitSignal(SignalName.WentWithinRange3D));
			} else if (_isDetectable) {
				QueueFree();
			}
		}

		[Signal]
		public delegate void WentWithinRange3DEventHandler();
	}
}
