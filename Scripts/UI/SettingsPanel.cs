using Godot;
using System;

namespace Zenitka.Scripts.UI
{
	public partial class SettingsPanel : Control
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}
		
		public void SaveButton()
		{
			
			var  node1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer/LineEdit");
			Settings.Settings2D.DefaultGun.BulletSpeed = node1.Text.ToFloat();
			var node2 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer2/LineEdit");
			Settings.Settings2D.DefaultGun.InitialElevationAngle = node2.Text.ToFloat();
			var node3 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer3/LineEdit");
			Settings.Settings2D.DefaultGun.AngularVelocity = node3.Text.ToFloat();
			
			var node4 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer/LineEdit");
			var node5 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer/LineEdit2");
			Settings.Settings2D.RocketGun.ZenithDetectionCoordinates = new Vector2(node4.Text.ToFloat(), node5.Text.ToFloat());
			var node6 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer2/LineEdit");
			var node7 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer2/LineEdit2");
			Settings.Settings2D.RocketGun.TargetEndCoordinates = new Vector2(node6.Text.ToFloat(), node7.Text.ToFloat());
			var node8 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit");
			var node9 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit2");
			Settings.Settings2D.RocketGun.InitialVelocity = new Vector2(node8.Text.ToFloat(), node9.Text.ToFloat());
			var node10 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer6/LineEdit");
			var node11 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer6/LineEdit2");
			Settings.Settings2D.RocketGun.Size = new Vector2(node10.Text.ToFloat(), node11.Text.ToFloat());
			var node12 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer7/LineEdit");
			var node13 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer7/LineEdit2");
			Settings.Settings2D.RocketGun.RocketMassWithoutFuel = node12.Text.ToFloat();
			Settings.Settings2D.RocketGun.FuelMass = node13.Text.ToFloat();
			var node14 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer3/LineEdit");
			Settings.Settings2D.RocketGun.AngularVelocity = node14.Text.ToFloat();
			
			var node15 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer/LineEdit");
			var node16 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer/LineEdit2");
			Settings.Settings2D.DefaultTarget.Velocity = new Vector2(node15.Text.ToFloat(), node16.Text.ToFloat());
			var node17 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer2/LineEdit");
			var node18 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer2/LineEdit2");
			Settings.Settings2D.DefaultTarget.Size = new Vector2(node17.Text.ToFloat(), node18.Text.ToFloat());
			var node19 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer3/LineEdit");
			Settings.Settings2D.DefaultTarget.Mass = node19.Text.ToFloat();
			var node20 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer4/LineEdit");
			Settings.Settings2D.DefaultTarget.Altitude = node20.Text.ToFloat();
			
			var node21 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer/LineEdit");
			Settings.Settings2D.RocketTarget.RocketAcceleration = node21.Text.ToFloat();
			var node22 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer2/LineEdit");
			Settings.Settings2D.RocketTarget.MaxAngularVelocity = node22.Text.ToFloat();
			var node23 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer5/LineEdit");
			Settings.Settings2D.RocketTarget.DetonationDistance = node23.Text.ToFloat();
			var node24 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer6/LineEdit");
			Settings.Settings2D.RocketTarget.ShrapnelVelocity = node24.Text.ToFloat();
			var node25 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit");
			var node26 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit2");
			Settings.Settings2D.RocketTarget.RocketMassWithoutFuel = node25.Text.ToFloat();
			Settings.Settings2D.RocketTarget.FuelMass = node26.Text.ToFloat();
			


		}


		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			VBoxContainer marginContainer = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer");
			ColorRect textureRect = GetNode<ColorRect>("ColorRect");

			textureRect.Size = new Vector2(marginContainer.Size[0] + 10, marginContainer.Size[1] + 10);
			
			
			OptionButton optionButton = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton");
			VBoxContainer default2D = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/2DDefault");
			VBoxContainer rocket2D = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/2DRocket");
			if (optionButton.Selected == 0)
			{
				default2D.Show();
				rocket2D.Hide();
			}

			if (optionButton.Selected == 1)
			{
				default2D.Hide();
				rocket2D.Show();
			}
			OptionButton optionButton2 = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton2");
			VBoxContainer defaultTarget2D = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget");
			VBoxContainer rocketTarget2D = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget");
			if (optionButton2.Selected == 0)
			{
				defaultTarget2D.Show();
				rocketTarget2D.Hide();
			}

			if (optionButton2.Selected == 1)
			{
				defaultTarget2D.Hide();
				rocketTarget2D.Show();
			}
			

		}
		private void CloseButton()
		{
			var animation = GetNode<AnimationPlayer>("Animation");
			animation.Play("out");
		}
		
		
	}
}





