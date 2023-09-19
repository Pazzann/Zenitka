using Godot;
using System;

namespace Zenitka.Scripts._2D
{
	public partial class Bullet : RigidBody2D
	{
		private Timer _timer;
		
		public override void _Ready()
		{
			_timer = GetNode<Timer>("SuicideTimer");
		}

		public void Fire(float lifespanSec) {
			_timer.Start(lifespanSec);
			Freeze = false;
		}

		[Signal]
		public delegate void SelfDestroyedEventHandler();

		private void OnSuicideTimerTimeout()
		{
			_timer.Stop();
			EmitSignal(SignalName.SelfDestroyed);
		}
	}
}
