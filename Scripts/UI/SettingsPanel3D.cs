using Godot;
using System;

namespace Zenitka.Scripts.UI
{
	public partial class SettingsPanel3D : Control
	{
		public override void _Ready()
		{
			
			var node1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer/LineEdit");
			node1.Text = Settings.Settings3D.DefaultGun.BulletSpeed.ToString();
			var node2 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer2/LineEdit");
			node2.Text = Settings.Settings3D.DefaultGun.InitialElevationAngle.ToString();
			var node3 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer3/LineEdit");
			node3.Text = Settings.Settings3D.DefaultGun.AngularVelocityX.ToString();
			var node311 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer4/LineEdit");
			node311.Text = Settings.Settings3D.DefaultGun.AngularVelocityY.ToString();
			var node33 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer5/LineEdit");
			node33.Text = Settings.Settings3D.DefaultGun.BulletMass.ToString();
			var node34 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer6/LineEdit");
			node34.Text = Settings.Settings3D.DefaultGun.AirResistance.ToString();
			var node40 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer7/LineEdit");
			node40.Text = Settings.Settings3D.DefaultGun.Gravity.ToString();

			var node4 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer/LineEdit");
			var node5 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer/LineEdit2");
			node4.Text = Settings.Settings3D.RocketGun.ZenithDetectionCoordinates[0].ToString();
			node5.Text = Settings.Settings3D.RocketGun.ZenithDetectionCoordinates[1].ToString();
			// var node6 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer2/LineEdit");
			// var node7 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer2/LineEdit2");
			// Settings.Settings3D.RocketGun.TargetEndCoordinates = new Vector2(node6.Text.ToFloat(), node7.Text.ToFloat());
			var node8 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer5/LineEdit");
			var node9 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer5/LineEdit2");
			node8.Text = Settings.Settings3D.RocketGun.InitialVelocity[0].ToString();
			node9.Text = Settings.Settings3D.RocketGun.InitialVelocity[1].ToString();
			// var node10 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer6/LineEdit");
			// var node11 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer6/LineEdit2");
			// Settings.Settings3D.RocketGun.Size = new Vector2(node10.Text.ToFloat(), node11.Text.ToFloat());
			var node12 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer7/LineEdit");
			var node13 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer7/LineEdit2");
			node12.Text = Settings.Settings3D.RocketGun.RocketMassWithoutFuel.ToString();
			node13.Text = Settings.Settings3D.RocketGun.FuelMass.ToString();
			var node14 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer3/LineEdit");
			node14.Text = Settings.Settings3D.RocketGun.AngularVelocityX.ToString();
			var node141 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer4/LineEdit");
			node141.Text = Settings.Settings3D.RocketGun.AngularVelocityY.ToString();
			var node35 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer8/LineEdit");
			node35.Text = Settings.Settings3D.RocketGun.AirResistance.ToString();
			var node41 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer9/LineEdit");
			node41.Text = Settings.Settings3D.RocketGun.Gravity.ToString();

			var node15 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer/LineEdit");
			// var node16 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer/LineEdit2");
			node15.Text = Settings.Settings3D.DefaultTarget.Velocity.ToString();
			// var node17 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer2/LineEdit");
			// var node18 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer2/LineEdit2");
			// Settings.Settings3D.DefaultTarget.Size = new Vector2(node17.Text.ToFloat(), node18.Text.ToFloat());
			var node19 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer3/LineEdit");
			node19.Text = Settings.Settings3D.DefaultTarget.Mass.ToString();
			// var node20 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer4/LineEdit");
			// Settings.Settings3D.DefaultTarget.Altitude = node20.Text.ToFloat();
			var node36 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer8/LineEdit");
			node36.Text = Settings.Settings3D.DefaultTarget.AirResistance.ToString();

			var node21 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer/LineEdit");
			node21.Text = Settings.Settings3D.RocketTarget.RocketAcceleration.ToString();
			var node22 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer2/LineEdit");
			node22.Text = Settings.Settings3D.RocketTarget.MaxAngularVelocity.ToString();
			var node23 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer5/LineEdit");
			node23.Text = Settings.Settings3D.RocketTarget.DetonationDistance.ToString();
			var node24 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer6/LineEdit");
			node24.Text = Settings.Settings3D.RocketTarget.ShrapnelVelocity.ToString();
			var node25 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer7/LineEdit");
			var node26 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer7/LineEdit2");
			node25.Text = Settings.Settings3D.RocketTarget.RocketMassWithoutFuel.ToString();
			node26.Text = Settings.Settings3D.RocketTarget.FuelMass.ToString();
			var node37 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer8/LineEdit");
			node37.Text = Settings.Settings3D.RocketTarget.AirResistance.ToString();
		}

		public void SaveButton()
		{
			var node1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer/LineEdit");
			Settings.Settings3D.DefaultGun.BulletSpeed = node1.Text.ToFloat();
			var node2 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer2/LineEdit");
			Settings.Settings3D.DefaultGun.InitialElevationAngle = node2.Text.ToFloat();
			var node3 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer3/LineEdit");
			Settings.Settings3D.DefaultGun.AngularVelocityX = node3.Text.ToFloat();
			var node311 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer4/LineEdit");
			Settings.Settings3D.DefaultGun.AngularVelocityY = node311.Text.ToFloat();
			var node33 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer5/LineEdit");
			Settings.Settings3D.DefaultGun.BulletMass = node33.Text.ToFloat();
			var node34 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer6/LineEdit");
			Settings.Settings3D.DefaultGun.AirResistance = node34.Text.ToFloat();
			var node40 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefault/HBoxContainer7/LineEdit");
			Settings.Settings3D.DefaultGun.Gravity = node40.Text.ToFloat();

			var node4 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer/LineEdit");
			var node5 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer/LineEdit2");
			Settings.Settings3D.RocketGun.ZenithDetectionCoordinates =
				new Vector2(node4.Text.ToFloat(), node5.Text.ToFloat());
			// var node6 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer2/LineEdit");
			// var node7 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer2/LineEdit2");
			// Settings.Settings3D.RocketGun.TargetEndCoordinates = new Vector2(node6.Text.ToFloat(), node7.Text.ToFloat());
			var node8 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer5/LineEdit");
			var node9 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer5/LineEdit2");
			Settings.Settings3D.RocketGun.InitialVelocity = new Vector2(node8.Text.ToFloat(), node9.Text.ToFloat());
			// var node10 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer6/LineEdit");
			// var node11 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer6/LineEdit2");
			// Settings.Settings3D.RocketGun.Size = new Vector2(node10.Text.ToFloat(), node11.Text.ToFloat());
			var node12 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer7/LineEdit");
			var node13 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer7/LineEdit2");
			Settings.Settings3D.RocketGun.RocketMassWithoutFuel = node12.Text.ToFloat();
			Settings.Settings3D.RocketGun.FuelMass = node13.Text.ToFloat();
			var node14 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer3/LineEdit");
			Settings.Settings3D.RocketGun.AngularVelocityX = node14.Text.ToFloat();
			var node141 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer4/LineEdit");
			Settings.Settings3D.RocketGun.AngularVelocityY = node141.Text.ToFloat();
			var node35 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer8/LineEdit");
			Settings.Settings3D.RocketGun.AirResistance = node35.Text.ToFloat();
			var node41 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocket/HBoxContainer9/LineEdit");
			Settings.Settings3D.RocketGun.Gravity = node41.Text.ToFloat();

			var node15 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer/LineEdit");
			// var node16 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer/LineEdit2");
			Settings.Settings3D.DefaultTarget.Velocity = node15.Text.ToFloat();
			// var node17 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer2/LineEdit");
			// var node18 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer2/LineEdit2");
			// Settings.Settings3D.DefaultTarget.Size = new Vector2(node17.Text.ToFloat(), node18.Text.ToFloat());
			var node19 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer3/LineEdit");
			Settings.Settings3D.DefaultTarget.Mass = node19.Text.ToFloat();
			// var node20 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer4/LineEdit");
			// Settings.Settings3D.DefaultTarget.Altitude = node20.Text.ToFloat();
			var node36 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget/HBoxContainer8/LineEdit");
			Settings.Settings3D.DefaultTarget.AirResistance = node36.Text.ToFloat();

			var node21 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer/LineEdit");
			Settings.Settings3D.RocketTarget.RocketAcceleration = node21.Text.ToFloat();
			var node22 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer2/LineEdit");
			Settings.Settings3D.RocketTarget.MaxAngularVelocity = node22.Text.ToFloat();
			var node23 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer5/LineEdit");
			Settings.Settings3D.RocketTarget.DetonationDistance = node23.Text.ToFloat();
			var node24 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer6/LineEdit");
			Settings.Settings3D.RocketTarget.ShrapnelVelocity = node24.Text.ToFloat();
			var node25 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer7/LineEdit");
			var node26 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer7/LineEdit2");
			Settings.Settings3D.RocketTarget.RocketMassWithoutFuel = node25.Text.ToFloat();
			Settings.Settings3D.RocketTarget.FuelMass = node26.Text.ToFloat();
			var node37 =
				GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget/HBoxContainer8/LineEdit");
			Settings.Settings3D.RocketTarget.AirResistance = node37.Text.ToFloat();


		}


		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			VBoxContainer marginContainer = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer");
			ColorRect textureRect = GetNode<ColorRect>("ColorRect");

			textureRect.Size = new Vector2(marginContainer.Size[0] + 10, marginContainer.Size[1] + 10);


			OptionButton optionButton = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton");
			VBoxContainer default2D = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/3DDefault");
			VBoxContainer rocket2D = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/3DRocket");
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
			VBoxContainer defaultTarget2D =
				GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/3DDefaultTarget");
			VBoxContainer rocketTarget2D =
				GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/3DRocketTarget");
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




