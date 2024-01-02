using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Bullet : BallisticBody
	{
		private Timer _timer;
		private PackedScene _explosionScene;

		private float _lifespan = 0f;

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

		public event Action<Node3D> OnExploded;

		public override void _Ready()
		{
			base._Ready();

			_timer = GetNode<Timer>("SuicideTimer");

			if (_lifespan != 0)
				_timer.Start(_lifespan);
		}

		public override void _Process(double delta)
		{
			base._Process(delta);

			if (GlobalPosition.Y < 0f)
				OnSuicideTimerTimeout();
		}

		private void OnSuicideTimerTimeout()
		{
			Explode(null);
		}

		private void OnBodyEntered(Node body)
		{
			Explode(body as Node3D);
		}

		private void Explode(Node3D target) {
			OnExploded?.Invoke(target);
			QueueFree();
		}
	}
}
