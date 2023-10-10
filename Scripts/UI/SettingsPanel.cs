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

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			OptionButton optionButton = GetNode<OptionButton>("ScrollContainer/VBoxContainer/OptionButton");
			VBoxContainer default2D = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer/2DDefault");
			VBoxContainer rocket2D = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer/2DRocket");
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
			OptionButton optionButton2 = GetNode<OptionButton>("ScrollContainer/VBoxContainer/OptionButton2");
			VBoxContainer defaultTarget2D = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer/2DDefaultTarget");
			VBoxContainer rocketTarget2D = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer/2DRocketTarget");
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



