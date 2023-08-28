using Godot;
using System;


namespace Zenitka.Scripts._2D
{
	public partial class Bullet : Node
	{
		const float Velocity = 1.0f;

		private Timer _timer;
		
		
		public override void _Ready()
		{
			_timer = GetChild(1) as Timer;


		}
		public override void _Process(double delta)
		{
		}
		private void _on_timer_timeout()
		{
			// Replace with function body.
		}

	}
}


