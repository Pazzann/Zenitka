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
			// var node9 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer5/LineEdit2");
			node8.Text = Settings.Settings2D.RocketGun.InitialVelocity.ToString();
			
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
			var node32 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer9/LineEdit");
			node32.Text = Settings.Settings2D.RocketGun.FuelCost.ToString();
			var node41 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer10/LineEdit");
			node41.Text = Settings.Settings2D.RocketGun.MainEThrust.ToString();
			
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
			var node36 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer5/LineEdit");
			node36.Text = Settings.Settings2D.DefaultTarget.QDrag.ToString();
			var node57 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer6/LineEdit");
			node57.Text = Settings.Settings2D.DefaultTarget.Acceleration.ToString();
			
			var node21 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer/LineEdit");
			node21.Text = Settings.Settings2D.RocketTarget.MainEThrust.ToString();
			var node22 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer2/LineEdit");
			node22.Text = Settings.Settings2D.RocketTarget.MaxAngularVelocity.ToString();
			var node23 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer5/LineEdit");
			node23.Text = Settings.Settings2D.RocketTarget.DetonationDistance.ToString();
			var node24 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer6/LineEdit");
			node24.Text = Settings.Settings2D.RocketTarget.ShrapnelVelocity.ToString();
			var node25 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit");
			var node26 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit2");
			node25.Text = Settings.Settings2D.RocketTarget.BaseMass.ToString();
			node26.Text = Settings.Settings2D.RocketTarget.FuelMass.ToString();
			var node37 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer8/LineEdit");
			node37.Text = Settings.Settings2D.RocketTarget.QDrag.ToString();
			var node39 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer9/LineEdit");
			node39.Text = Settings.Settings2D.RocketTarget.StartVelocity.ToString();
			var node40 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer10/LineEdit");
			node40.Text = Settings.Settings2D.RocketTarget.FuelCost.ToString();
			
			var nodeAuto = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/Auto/LineEdit");
			nodeAuto.Text = Settings.Settings2D.TargetSpawnInterval.ToString();
			var nodeNoAutoCoordinateX = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/NoAutoCoordinates/LineEdit");
			nodeNoAutoCoordinateX.Text = Settings.Settings2D.DefaultTarget.CoordinateX.ToString();
			var nodeNoAutoCoordinateY = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/NoAutoCoordinates/LineEdit2");
			nodeNoAutoCoordinateY.Text = Settings.Settings2D.DefaultTarget.CoordinateY.ToString();
			var nodeNoAutoAngle1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/NoAutoAngle/LineEdit");
			nodeNoAutoAngle1.Text =Settings.Settings2D.DefaultTarget.Angle.ToString();
			var Rate = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/FireRate/LineEdit");
			Rate.Text = Settings.Settings2D.DefaultGun.FireRate.ToString();
			
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
			Settings.Settings2D.RocketGun.InitialVelocity = node8.Text.ToFloat();
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
			var node32 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer9/LineEdit");
			Settings.Settings2D.RocketGun.FuelCost = node32.Text.ToFloat();
			var node41 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocket/HBoxContainer10/LineEdit");
			Settings.Settings2D.RocketGun.MainEThrust = node41.Text.ToFloat();
			
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
			Settings.Settings2D.DefaultTarget.QDrag = node36.Text.ToFloat();
			var node57 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DDefaultTarget/HBoxContainer6/LineEdit");
			Settings.Settings2D.DefaultTarget.Acceleration = node57.Text.ToFloat();
			
			var node21 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer/LineEdit");
			Settings.Settings2D.RocketTarget.MainEThrust = node21.Text.ToFloat();
			var node22 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer2/LineEdit");
			Settings.Settings2D.RocketTarget.MaxAngularVelocity = node22.Text.ToFloat();
			var node23 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer5/LineEdit");
			Settings.Settings2D.RocketTarget.DetonationDistance = node23.Text.ToFloat();
			var node24 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer6/LineEdit");
			Settings.Settings2D.RocketTarget.ShrapnelVelocity = node24.Text.ToFloat();
			var node25 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit");
			var node26 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer7/LineEdit2");
			Settings.Settings2D.RocketTarget.BaseMass = node25.Text.ToFloat();
			Settings.Settings2D.RocketTarget.FuelMass = node26.Text.ToFloat();
			var node37 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer8/LineEdit");
			Settings.Settings2D.RocketTarget.QDrag = node37.Text.ToFloat();
			var node39 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer9/LineEdit");
			Settings.Settings2D.RocketTarget.StartVelocity = node39.Text.ToFloat();
			var node40 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/2DRocketTarget/HBoxContainer10/LineEdit");
			Settings.Settings2D.RocketTarget.FuelCost = node40.Text.ToFloat();
			
			OptionButton optionButton = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton");
			OptionButton optionButton2 = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton2");
			OptionButton optionButton3 = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton3");
			Settings.Settings2D.IsNotDefaultGun = Convert.ToBoolean(optionButton.Selected);
			Settings.Settings2D.IsNotDefaultTarget = Convert.ToBoolean(optionButton2.Selected);
			Settings.Settings2D.Auto=Convert.ToBoolean(optionButton3.Selected);

			Settings.Settings2D.DefaultGun.Zenitki[0] = 0f;
			Settings.Settings2D.DefaultGun.Zenitki[1] = 0f;
			Settings.Settings2D.DefaultGun.Zenitki[2] = 0f;

			try
			{
				var nodeAuto = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/Auto/LineEdit");
				Settings.Settings2D.TargetSpawnInterval=nodeAuto.Text.ToFloat();
				
				var nodeNoAutoCoordinateX = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/NoAutoCoordinates/LineEdit");
				Settings.Settings2D.DefaultTarget.CoordinateX=nodeNoAutoCoordinateX.Text.ToFloat();
				
				var nodeNoAutoCoordinateY = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/NoAutoCoordinates/LineEdit2");
				Settings.Settings2D.DefaultTarget.CoordinateY=nodeNoAutoCoordinateY.Text.ToFloat();
				
				var nodeNoAutoAngle1 = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/NoAutoAngle/LineEdit");
				Settings.Settings2D.DefaultTarget.Angle=nodeNoAutoAngle1.Text.ToFloat();
				
				var Rate = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/FireRate/LineEdit");
				Settings.Settings2D.DefaultGun.FireRate=Rate.Text.ToFloat();
				
				var a = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/Multi/1/LineEdit");
				Settings.Settings2D.DefaultGun.Zenitki[0]=a.Text.ToFloat();
				
				var b = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/Multi/2/LineEdit");
				Settings.Settings2D.DefaultGun.Zenitki[1]=b.Text.ToFloat();
				
				var c = GetNode<LineEdit>("ColorRect/MarginContainer/VBoxContainer/Multi/3/LineEdit");
				Settings.Settings2D.DefaultGun.Zenitki[2]=c.Text.ToFloat();
			} catch (Exception) {}
			
			OptionButton optionButtonA = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/Multi/1/OptionButton");
			Settings.Settings2D.DefaultGun.ZenitkiState[0] = optionButtonA.Selected;
			
			OptionButton optionButtonB = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/Multi/2/OptionButton");
			Settings.Settings2D.DefaultGun.ZenitkiState[1] = optionButtonB.Selected;
			
			OptionButton optionButtonC = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/Multi/3/OptionButton");
			Settings.Settings2D.DefaultGun.ZenitkiState[2] = optionButtonC.Selected;

			Settings.Settings2D.InvokeOnSettingsChanged();
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
			OptionButton optionButton3 = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton3");
			HBoxContainer auto = GetNode<HBoxContainer>("ColorRect/MarginContainer/VBoxContainer/Auto");
			HBoxContainer noAutoCoordinates = GetNode<HBoxContainer>("ColorRect/MarginContainer/VBoxContainer/NoAutoCoordinates");
			HBoxContainer noAutoAngle = GetNode<HBoxContainer>("ColorRect/MarginContainer/VBoxContainer/NoAutoAngle");
			if (optionButton3.Selected == 0)
			{
				auto.Show();
				noAutoCoordinates.Hide();
				noAutoAngle.Hide();
			}

			if (optionButton3.Selected == 1)
			{
				auto.Hide();
				noAutoCoordinates.Show();
				noAutoAngle.Show();
			}
			OptionButton optionButton4 = GetNode<OptionButton>("ColorRect/MarginContainer/VBoxContainer/OptionButton4");
			VBoxContainer multi = GetNode<VBoxContainer>("ColorRect/MarginContainer/VBoxContainer/Multi");
			if (optionButton4.Selected == 1)
			{
				multi.Show();
			}
			else
			multi.Hide();
			

		}
		private void CloseButton()
		{
			var animation = GetNode<AnimationPlayer>("Animation");
			animation.Play("out");
		}
		
		
	}
}





