using Godot;
using System;

namespace Zenitka.Scripts._2D 
{
	public partial class Main2D : Node2D
	{
		private PackedScene _targetScene;

		private Cannon _cannon;
		private PathFollow2D _targetSpawnLocationSampler;
		
		private Random _rng;

		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_targetScene = GD.Load<PackedScene>("res://Prefabs/Target.tscn");

			_rng = new Random();
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var obstacle = _targetScene.Instantiate() as Target;

			obstacle.Position = NextTargetSpawnPosition();

			AddChild(obstacle);
		}

		private Vector2 NextTargetSpawnPosition() {
			var (width, height) = GetViewportRect().Size; 

			return new Vector2(
				(float) MathUtils.Extend((_rng.NextDouble() - 0.5) * 0.2, 0.3) * width,
				(float) ((flipped ? 0.4 : -1.0) - _rng.NextDouble() * 0.2) * height 
			);
		}
	}
}
