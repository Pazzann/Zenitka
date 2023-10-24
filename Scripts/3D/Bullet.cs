using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Bullet : DynamicBody
	{
		private Timer _timer;

		private float _lifespan = 10f;

		public float Lifespan
		{
			get { return _lifespan; }
			set
			{
				_lifespan = value;

				if (_timer != null)
					_timer.Start(_lifespan);
			}
		}

		public event Action<Node2D> OnExploded;

		public override void _Ready()
		{
			_timer = GetNode<Timer>("SuicideTimer");
			_timer.Start(Lifespan);
		}

		private void OnSuicideTimerTimeout()
		{
			_timer.Stop();
			QueueFree();

			OnExploded?.Invoke(null);
		}
	}
}
