using Godot;
using System;

namespace Zenitka.Scripts.UI
{
	public partial class Menu : Control
	{
		private void ContinueButon()
		{
			QueueFree();
		}


		private void MenuButton()
		{
			GetTree().ChangeSceneToFile("res://Scenes/MainUI.tscn");
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}











