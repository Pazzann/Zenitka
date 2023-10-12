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
			VBoxContainer default2D = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer/2DDefult");
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

		}
		private void CloseButton()
		{
			QueueFree();
		}
	}
}



