using Godot;
using System;
using Zenitka.Scripts._3D;

public partial class CameraTarget2 : Camera3D
{
	private const float ZOOM_SPEED = 0.2f;
	
	private float _zoom = 1f;
	private Target _target;
	private Vector3 _targetLinearVelocityNormalized;
	private Basis _targetBasis;
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
			if (_target.LinearVelocity.Normalized() != new Vector3(0, 0, 0))
			{
				_targetLinearVelocityNormalized = _target.LinearVelocity.Normalized();
			}
			Basis = _targetBasis;
			Position = _target.Position;
			if (_targetLinearVelocityNormalized != new Vector3(0, 0, 0))
			{
				Translate(new Vector3(-_targetLinearVelocityNormalized.X * 5, 10, -_targetLinearVelocityNormalized.Z * 5));
			}
			else
			{ Translate(new Vector3(-5, 10, -5)); }
			Transform = Transform.LookingAt(_target.Position, Vector3.Up);

		}
	}
	private void _on_target_spawn_timer_timeout()
	{
		_target = GetNodeOrNull<Target>("/root/Main3D/Target");
		_targetLinearVelocityNormalized = _target.LinearVelocity.Normalized();
		_targetBasis = _target.Basis;
	}
}



