using Godot;
using System;
using Zenitka.Scripts._2D;

public partial class Camera2DTarget : Camera2D
{
	private Target _target;
	private float _koef = 1f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{ 
		_target = GetNode<Target>("/root/Main2D/Target");
		if (Input.IsActionJustPressed("switch2"))
		{
			if (IsCurrent() == false)
			{
				MakeCurrent();
			}
		}

		if (_target != null)
		{
			var temp = _target.Position;
			var temp2 = Zoom;
			Position = temp;
			if (Input.IsActionJustPressed("cam_zoom_out_3d") && _koef>=1.0f)
			{
				_koef -= 0.5f;
				temp2.X = _koef;
				temp2.Y = _koef;
			}
			if (Input.IsActionJustPressed("cam_zoom_in_3d") && _koef<6f)
			{
				_koef += 0.5f;
				temp2.X = _koef;
				temp2.Y = _koef;
			}
			Zoom = temp2;
		}
	}
}
