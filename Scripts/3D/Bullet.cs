using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Bullet : RigidBody3D
	{
		private Timer _timer;
		private float _lifespanSec = 0f;
		
		public override void _Ready()
		{
			_timer = GetNode<Timer>("SuicideTimer");
			_timer.Start(_lifespanSec);
		}

		public void SetLifespan(float lifespanSec) {
			_lifespanSec = lifespanSec;
		}

		[Signal]
		public delegate void SelfDestroyed3DEventHandler();

		private void OnSuicideTimerTimeout()
		{
			_timer.Stop();
			EmitSignal(SignalName.SelfDestroyed3D);
		}
	}
}


