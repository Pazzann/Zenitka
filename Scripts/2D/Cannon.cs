using System;
using Godot;


namespace Zenitka.Scripts._2D
{
	public partial class Cannon : Node
	{

		const float GunRotationSpeed = 1.0f;
	    
    	private Sprite2D _body;
        private Sprite2D _gun;
        private Node2D _bulletSpawn;

        private float _targetAngle = -0.5f;

        public override void _Ready()
    	{
    		_body = GetChild(0) as Sprite2D;
            _gun = _body.GetChild(0) as Sprite2D;
            _bulletSpawn = _gun.GetChild(0) as Node2D;
        }


        void RotateTo(float targetAngle)
        {
	        _targetAngle = targetAngle;
        }
        
    	public override void _Process(double delta)
        {
	        if(Math.Abs(_gun.Rotation - _targetAngle) == 0)
		        return;
	        if (Math.Abs(_gun.Rotation - _targetAngle) < Math.Abs(GunRotationSpeed * (float)delta))
	        {
		        _gun.Rotation = _targetAngle;
		        return;
	        }

	        if (_gun.Rotation > _targetAngle)
	        {
		        _gun.Rotation -= GunRotationSpeed * (float)delta ;
	        }
	        else
	        {
		        _gun.Rotation += GunRotationSpeed * (float)delta ;
	        }
        }
    }

}

