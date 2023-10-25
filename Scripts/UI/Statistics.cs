using Godot;
using System;

public partial class Statistics : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label speedLabel = GetNode<Label>("ColorRect/speed");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Label speedLabel = GetNode<Label>("ColorRect/speed");
		if (Input.IsActionJustPressed("time+"))
		{
			Engine.TimeScale += 0.1;
			speedLabel.Text = Engine.TimeScale.ToString();
		}

		if (Input.IsActionJustPressed("time-"))
		{
			if (Engine.TimeScale > 0.1)
			{
				Engine.TimeScale -= 0.1;
				speedLabel.Text = Engine.TimeScale.ToString();
			}
		}
	}
}
