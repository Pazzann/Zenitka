using Godot;
using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using Zenitka.Scripts._2D.Targets;
using Zenitka.Scripts.Math;
using Zenitka.Scripts.UI;


namespace Zenitka.Scripts._2D
{
	public partial class Main2D : Node2D
	{
		private static float BARREL_LENGTH = 200f;
		private static float MUZZLE_SPEED = 2000f;
		private static float TARGET_SPEED = 2000f;

		private PackedScene _targetScene;
		private PackedScene _bulletScene;

		private Cannon _cannon;

		private Node2D _anchor1;
		private Node2D _anchor2;


		private int _bulletCount = 0;

		private static Random _rng = new Random();

		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_anchor1 = GetNode<Node2D>("Anchor");
			_anchor2 = GetNode<Node2D>("Anchor2");

			_targetScene = GD.Load<PackedScene>("res://Prefabs/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/Bullet.tscn");
		}

		private void OnCannonGunReady(float angleRad, Vector2 headPosition, float timeOfCollision)
		{
			GD.Print(1);
			float angleRadF = angleRad;
			GD.Print(timeOfCollision);

			var bullet = _bulletScene.Instantiate() as Bullet;
			bullet.SelfDestructionTime = timeOfCollision - 0.05f;
			bullet.Rotate(Mathf.Pi * 0.5f - angleRadF);
			bullet.Rotation = angleRad;

			bullet.GlobalPosition = new Vector2(0.0f, 0.0f);

			bullet.GravityScale = 0f;
			bullet.StartAngle = angleRad;
			
			AddChild(bullet);
			if(_bulletCount++ < 5)
				ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout).OnCompleted(()=>{_cannon.RotateToAndSignal(angleRad + 0.02f , timeOfCollision);});
		}

		private void OnTargetSpawnTimerTimeout()
		{
			
			var target = _targetScene.Instantiate() as Target;
			_bulletCount = 0;
			
			var startPos = GenerateTargetSpawnlocation();

			// TODO: use actual object size
			target.GlobalPosition = startPos;
			target._Ready();
			

			var bullet = _bulletScene.Instantiate() as Target;
			bullet._Ready();
			float[] a = Math2D.GetAngle(_cannon.GlobalPosition, startPos, bullet, target, 9.8f,
				_cannon.GunRotationSpeed, _cannon.Rotation, 1.0f, new Vector2(5400.0f, 1000.0f));
			bullet.QueueFree();


			
			_cannon.RotateToAndSignal(a[0] - 0.06f , a[1]);
			
			AddChild(target);
		}

		private Vector2 GenerateTargetSpawnlocation()
		{
			var pos = new Vector2(0.0f, _anchor2.GlobalPosition.Y);


			Random rand = new Random();
			pos.X = (rand.Next(0, 2) == 0) ? _anchor1.GlobalPosition.X : _anchor2.GlobalPosition.X;

			pos.Y = rand.Next( -2000, -500);

			return pos;
		}

		private void MenuButton()
		{
			// var button = GetNode<Button>("Button");
			// var pos = button.GlobalPosition;
			//
			// PackedScene menuScene = GD.Load<PackedScene>("res://Prefabs/UI/Menu.tscn");
			// var menu = menuScene.Instantiate() as Menu;
			// menu.GlobalPosition = new Vector2(pos[0] - 450, pos[1] + 500);
			// GD.Print(pos[0] - 450, pos[1] + 500);
			// AddChild(menu);
			//
			// button.Hide();
			var settingsPanel = GetNode<Control>("menu");
			if (!settingsPanel.Visible)
			{
				GD.Print("in");
				settingsPanel.Show();
				var animation = GetNode<AnimationPlayer>("menu/Animation");
				animation.Play("in");
			}
			else
			{
				GD.Print("out");
				var animation = GetNode<AnimationPlayer>("menu/Animation");
				animation.Play("out");
				
				
			}
			
		}

		private void SettingsButton()
		{
			var settingsPanel = GetNode<Control>("SettingsPanel");
			if (!settingsPanel.Visible)
			{
				settingsPanel.Show();
				var animation = GetNode<AnimationPlayer>("SettingsPanel/Animation");
				animation.Play("in");
			}
			else
			{
				// GD.Print("out");
				var animation = GetNode<AnimationPlayer>("SettingsPanel/Animation");
				animation.Play("out");
				
				
			}

		}
	}
}

