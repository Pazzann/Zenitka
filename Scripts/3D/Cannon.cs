using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Cannon : Node3D
	{
		public float GunRotationSpeedX = 0.1f;
		public float GunRotationSpeedY = 0.1f;
		
		private MeshInstance3D _gun;
		private Node3D _head;
		
		private float _targetAngleX = 0f;
		private float _targetAngleY =  0f;
		private bool _shouldSignal = false;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_gun = GetNode<MeshInstance3D>("Body/Gun");
			_head = GetNode<Node3D>("Body/Gun/Head");
		}
		
		public Vector3 GetHeadPosition() {
			return _head.GlobalPosition;
		}
		
		public void RotateToAndSignal(float targetAngleX, float targetAngleY) {
			_shouldSignal = true;
			
			_targetAngleX = 0.5f * Mathf.Pi - Mathf.PosMod(targetAngleX, 2f * Mathf.Pi);
			_targetAngleY = 0.5f * Mathf.Pi - Mathf.PosMod(targetAngleY, 2f * Mathf.Pi);
		}

		

		public Vector3 GetCannonPosition()
		{
			return _gun.Position;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			float deltaF = (float) delta;

			if (Mathf.IsEqualApprox(_gun.Rotation[0], _targetAngleX, deltaF * GunRotationSpeedX) && Mathf.IsEqualApprox(_gun.Rotation[1], _targetAngleY, deltaF * GunRotationSpeedY)) {
				if (_shouldSignal) {
					_shouldSignal = false;
					
					EmitSignal(SignalName.GunReady3D, 0.5f * Mathf.Pi - _targetAngleX, 0.5f * Mathf.Pi - _targetAngleY);
					
				}

				return;
			} else
				// _gun.RotateX(Mathf.Sign(_targetAngleX - _gun.Rotation[0]) * GunRotationSpeedX * deltaF);
				// _gun.RotateY(Mathf.Sign(_targetAngleY - _gun.Rotation[1]) * GunRotationSpeedY * deltaF);
				
				_gun.RotateX(_targetAngleX - _gun.Rotation[0]);
				_gun.RotateY(_targetAngleY - _gun.Rotation[1]);
		}

		[Signal]
		public delegate void GunReady3DEventHandler(float angleRadX, float angleRadY);
	}
}
