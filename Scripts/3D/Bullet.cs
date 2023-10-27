using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Bullet : DynamicBody
	{
		private Timer _timer;

		private float _lifespan = 10f;
		
		private Label _destroyedLabel;

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
			_timer = GetNode<Timer>("SuicideTimer");
			_timer.Start(Lifespan);
			
			_destroyedLabel = GetNode<Label>("../Statistics/ColorRect/DestroyedTargets");
		}

		private void OnSuicideTimerTimeout()
		{
			OnExploded?.Invoke(null);
			QueueFree();
		}

		private void OnBodyEntered(Node body)
		{
			_destroyedLabel.Text = (Int32.Parse(_destroyedLabel.Text) + 1).ToString();
			OnExploded?.Invoke(body as Node3D);
			QueueFree();
		}
	}
}
