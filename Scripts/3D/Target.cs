using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Target : DynamicBody
	{
		private PackedScene _explosionScene;

		public Vector3 CannonPosition { get; set; } = Vector3.Zero;
		public float CannnonRange { get; set; } = 0f;

		public Action<bool> OnCannonVisiblityChanged { get; set; }
		public bool WithinCannonRange { get; set; } = false;

		public override void _Ready()
		{
			base._Ready();

			_explosionScene = GD.Load<PackedScene>("res://Prefabs/3D/Explosion.tscn");
		}

		public override void _Process(double delta)
		{
			base._Process(delta);

			if (GlobalPosition.Y < 0f && WithinCannonRange) {
				WithinCannonRange = false;
				OnCannonVisiblityChanged?.Invoke(false);
				Explode();
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			UpdateVisibility();
		}

		public void Explode() {
			var explosion = _explosionScene.Instantiate() as Explosion;
			GetParent()?.AddChild(explosion);
			explosion.GlobalPosition = GlobalPosition;

			QueueFree();
		}

		private void UpdateVisibility()
		{
			float distance = (GlobalPosition - CannonPosition).Length();

			if (distance < CannnonRange && !WithinCannonRange)
			{
				WithinCannonRange = true;
				OnCannonVisiblityChanged?.Invoke(true);
			}
			else if (distance > 3f * CannnonRange && WithinCannonRange)
			{
				WithinCannonRange = false;
				OnCannonVisiblityChanged?.Invoke(false);
				Explode();
			}
		}
	}
}
