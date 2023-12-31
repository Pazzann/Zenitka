using Godot;
using System;

namespace Zenitka.Scripts.UI
{
	public partial class Menu : Control
	{
		private void ContinueButon()
		{
			Pause();
		}
	

		private void MenuButton()
		{
			GetTree().ChangeSceneToFile("res://Scenes/MainUI.tscn");
			GetTree().Paused = false;
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		private void Pause()
		{
			if (!Visible)
			{
				GetTree().Paused = true;
				Input.MouseMode = Input.MouseModeEnum.Visible;
				Show();
			}
			else
			{
				GetTree().Paused = false;
				Hide();
			}

		}

		public override void _Process(double delta)
		{
			if (Input.IsActionJustPressed("pause"))
			{
				Pause();
			}
		}

		private void ExitButton()
		{
			GetTree().Quit();
		}
	}
}













