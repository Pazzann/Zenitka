using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Target : DynamicBody
	{
		public Vector3 CannonPosition { get; set; } = Vector3.Zero;
		public float CannnonRange { get; set; } = 0f;

		public Action<bool> OnCannonVisiblityChanged { get; set; }
		public bool WithinCannonRange { get; set; } = false;

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			UpdateVisibility();
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
				QueueFree();
			}
		}
	}
}
