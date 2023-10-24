using Godot;
using System;
using Zenitka.Scripts._3D;

public partial class CameraTarget2 : Camera3D
{
	private const float ZOOM_SPEED = 0.2f;
	
	private float _zoom = 1f;
	private int _timeBeforeStart;
	private Target _target;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
			_target = GetNodeOrNull<Target>("/root/Main3D/Target");

			if (_target != null)
			{

				var targetPosition = _target.Position;
				var targetBasis = _target.Basis;
				var targetLinearVelocity = _target.LinearVelocity;
				Basis = targetBasis;
				targetPosition.X -= targetLinearVelocity.X;
				targetPosition.Z -= targetLinearVelocity.Z;
				targetPosition.Y += 10;
				Position = targetPosition;
				Transform = Transform.LookingAt(_target.Position, Vector3.Up);

			}
	}
}
