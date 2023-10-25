using Godot;
using System;

namespace Zenitka.Scripts.UI
{
	public partial class SettingsPanel : Control
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			// var node222 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/HBoxContainer5/LineEdit");
			// node222.Text = Settings.Settings2D.AirResistance.ToString();
			var node223 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/HBoxContainer6/LineEdit");
			node223.Text = Settings.Settings2D.Gravity.ToString();
			var node224 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/HBoxContainer7/LineEdit");
			node224.Text = Settings.Settings2D.Random.ToString();
			
			var  node1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer/LineEdit");
			node1.Text = Settings.Settings2D.DefaultGun.BulletSpeed.ToString();
			var node2 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer2/LineEdit");
			node2.Text = Settings.Settings2D.DefaultGun.InitialElevationAngle.ToString();
			var node3 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer3/LineEdit");
			node3.Text = Settings.Settings2D.DefaultGun.AngularVelocity.ToString();
			var node33 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer4/LineEdit");
			node33.Text = Settings.Settings2D.DefaultGun.BulletMass.ToString();
			var node34 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer5/LineEdit");
			node34.Text = Settings.Settings2D.DefaultGun.AirResistance.ToString();
			// var node40 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer6/LineEdit");
			// node40.Text = Settings.Settings2D.DefaultGun.Gravity.ToString();
			var node49 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer7/LineEdit");
			node49.Text = Settings.Settings2D.DefaultGun.SalvoSize.ToString();
			
			var node4 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer/LineEdit");
			var node5 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer/LineEdit2");
			node4.Text = Settings.Settings2D.RocketGun.ZenithDetectionCoordinates[0].ToString();
			node5.Text = Settings.Settings2D.RocketGun.ZenithDetectionCoordinates[1].ToString();
			// var node6 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer2/LineEdit");
			// var node7 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer2/LineEdit2");
			// Settings.Settings2D.RocketGun.TargetEndCoordinates = new Vector2(node6.Text.ToFloat(), node7.Text.ToFloat());
			var node8 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit");
			var node9 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit2");
			node8.Text = Settings.Settings2D.RocketGun.InitialVelocity[0].ToString();
			node9.Text = Settings.Settings2D.RocketGun.InitialVelocity[1].ToString();
			// var node10 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer6/LineEdit");
			// var node11 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer6/LineEdit2");
			// Settings.Settings2D.RocketGun.Size = new Vector2(node10.Text.ToFloat(), node11.Text.ToFloat());
			var node12 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer7/LineEdit");
			var node13 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer7/LineEdit2");
			node12.Text = Settings.Settings2D.RocketGun.RocketMassWithoutFuel.ToString();
			node13.Text = Settings.Settings2D.RocketGun.FuelMass.ToString();
			var node14 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer3/LineEdit");
			node14.Text = Settings.Settings2D.RocketGun.AngularVelocity.ToString();
			var node35 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer8/LineEdit");
			node35.Text = Settings.Settings2D.RocketGun.AirResistance.ToString();
			// var node41 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer9/LineEdit");
			// node41.Text = Settings.Settings2D.RocketGun.Gravity.ToString();
			
			var node15 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer/LineEdit");
			// var node16 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer/LineEdit2");
			node15.Text = Settings.Settings2D.DefaultTarget.Velocity.ToString();
			// var node17 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer2/LineEdit");
			// var node18 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer2/LineEdit2");
			// Settings.Settings2D.DefaultTarget.Size = new Vector2(node17.Text.ToFloat(), node18.Text.ToFloat());
			var node19 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer3/LineEdit");
			node19.Text = Settings.Settings2D.DefaultTarget.Mass.ToString();
			// var node20 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer4/LineEdit");
			// Settings.Settings2D.DefaultTarget.Altitude = node20.Text.ToFloat();
			var node36 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer8/LineEdit");
			node36.Text = Settings.Settings2D.DefaultTarget.AirResistance.ToString();
			
			var node21 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer/LineEdit");
			node21.Text = Settings.Settings2D.RocketTarget.RocketAcceleration.ToString();
			var node22 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer2/LineEdit");
			node22.Text = Settings.Settings2D.RocketTarget.MaxAngularVelocity.ToString();
			var node23 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer5/LineEdit");
			node23.Text = Settings.Settings2D.RocketTarget.DetonationDistance.ToString();
			var node24 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer6/LineEdit");
			node24.Text = Settings.Settings2D.RocketTarget.ShrapnelVelocity.ToString();
			var node25 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit");
			var node26 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit2");
			node25.Text = Settings.Settings2D.RocketTarget.RocketMassWithoutFuel.ToString();
			node26.Text = Settings.Settings2D.RocketTarget.FuelMass.ToString();
			var node37 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer8/LineEdit");
			node37.Text = Settings.Settings2D.RocketTarget.AirResistance.ToString();
			
		}
		
		public void SaveButton()
		{
			
			var node223 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/HBoxContainer6/LineEdit");
			Settings.Settings2D.Gravity = node223.Text.ToFloat();
			var node224 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/HBoxContainer7/LineEdit");
			Settings.Settings2D.Random = node224.Text.ToInt();
			
			var  node1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer/LineEdit");
			Settings.Settings2D.DefaultGun.BulletSpeed = node1.Text.ToFloat();
			var node2 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer2/LineEdit");
			Settings.Settings2D.DefaultGun.InitialElevationAngle = node2.Text.ToFloat();
			var node3 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer3/LineEdit");
			Settings.Settings2D.DefaultGun.AngularVelocity = node3.Text.ToFloat();
			var node33 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer4/LineEdit");
			Settings.Settings2D.DefaultGun.BulletMass = node33.Text.ToFloat();
			var node34 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer5/LineEdit");
			Settings.Settings2D.DefaultGun.AirResistance = node34.Text.ToFloat();
			// var node40 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer6/LineEdit");
			// Settings.Settings2D.DefaultGun.Gravity = node40.Text.ToFloat();
			var node49 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefault/HBoxContainer7/LineEdit");
			Settings.Settings2D.DefaultGun.SalvoSize = node49.Text.ToInt();
			
			var node4 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer/LineEdit");
			var node5 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer/LineEdit2");
			Settings.Settings2D.RocketGun.ZenithDetectionCoordinates = new Vector2(node4.Text.ToFloat(), node5.Text.ToFloat());
			// var node6 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer2/LineEdit");
			// var node7 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer2/LineEdit2");
			// Settings.Settings2D.RocketGun.TargetEndCoordinates = new Vector2(node6.Text.ToFloat(), node7.Text.ToFloat());
			var node8 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit");
			var node9 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit2");
			Settings.Settings2D.RocketGun.InitialVelocity = new Vector2(node8.Text.ToFloat(), node9.Text.ToFloat());
			// var node10 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer6/LineEdit");
			// var node11 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer6/LineEdit2");
			// Settings.Settings2D.RocketGun.Size = new Vector2(node10.Text.ToFloat(), node11.Text.ToFloat());
			var node12 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer7/LineEdit");
			var node13 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer7/LineEdit2");
			Settings.Settings2D.RocketGun.RocketMassWithoutFuel = node12.Text.ToFloat();
			Settings.Settings2D.RocketGun.FuelMass = node13.Text.ToFloat();
			var node14 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer3/LineEdit");
			Settings.Settings2D.RocketGun.AngularVelocity = node14.Text.ToFloat();
			var node35 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer8/LineEdit");
			Settings.Settings2D.RocketGun.AirResistance = node35.Text.ToFloat();
			// var node41 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer9/LineEdit");
			// Settings.Settings2D.RocketGun.Gravity = node41.Text.ToFloat();
			
			var node15 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer/LineEdit");
			// var node16 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer/LineEdit2");
			Settings.Settings2D.DefaultTarget.Velocity = node15.Text.ToFloat();
			// var node17 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer2/LineEdit");
			// var node18 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer2/LineEdit2");
			// Settings.Settings2D.DefaultTarget.Size = new Vector2(node17.Text.ToFloat(), node18.Text.ToFloat());
			var node19 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer3/LineEdit");
			Settings.Settings2D.DefaultTarget.Mass = node19.Text.ToFloat();
			// var node20 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer4/LineEdit");
			// Settings.Settings2D.DefaultTarget.Altitude = node20.Text.ToFloat();
			var node36 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer5/LineEdit");
			Settings.Settings2D.DefaultTarget.AirResistance = node36.Text.ToFloat();
			
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
			var node37 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer8/LineEdit");
			Settings.Settings2D.RocketTarget.AirResistance = node37.Text.ToFloat();
			
			
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





