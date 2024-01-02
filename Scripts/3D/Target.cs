using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Target : BallisticBody
	{
		private PackedScene _explosionScene;
		
		public override void _Ready()
		{
			base._Ready();

			_explosionScene = GD.Load<PackedScene>("res://Prefabs/3D/Explosion.tscn");
		}

		private void OnBodyEntered(Node _)
		{
			Explode();
		}

		private void Explode() {
			var explosion = _explosionScene.Instantiate() as Explosion;
			GetParent()?.AddChild(explosion);
			explosion.GlobalPosition = GlobalPosition;
			QueueFree();
		}
	}
}
