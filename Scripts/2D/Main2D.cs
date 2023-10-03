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

		private static Random _rng = new Random();

		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_anchor1 = GetNode<Node2D>("Anchor");
			_anchor2 = GetNode<Node2D>("Anchor2");

			_targetScene = GD.Load<PackedScene>("res://Prefabs/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/Bullet.tscn");
		}

		private void OnCannonGunReady(float angleRad, Vector2 headPosition)
		{
			float angleRadF = angleRad;

			var bullet = _bulletScene.Instantiate() as Bullet;

			bullet.Rotate(Mathf.Pi * 0.5f - angleRadF);
			bullet.Rotation = angleRad;

			bullet.GlobalPosition = headPosition;

			bullet.GravityScale = 0f;
			bullet.StartAngle = angleRad;

			// bullet.SetLifespan(10f);
			AddChild(bullet);
			// ToSignal(bullet, Bullet.SignalName.SelfDestroyed).OnCompleted(() => { bullet.QueueFree(); });
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var target = _targetScene.Instantiate() as Target;


			var startPos = GenerateTargetSpawnlocation();

			// TODO: use actual object size
			target.GlobalPosition = startPos;
			target._Ready();
			// target.Rotation = (System.Math.Abs(target.GlobalPosition.X - _anchor2.GlobalPosition.X) < 0.1f) ? -(float)System.Math.PI : 0.0f;

			// ToSignal(target, Target.SignalName.WentWithinRange).OnCompleted(() =>
			// {

			var bullet = _bulletScene.Instantiate() as Target;
			bullet._Ready();
			float a = Math2D.GetAngle(_cannon.GlobalPosition, target.GlobalPosition, bullet, target, 9.8f,
				_cannon.GunRotationSpeed, _cannon.Rotation, 100.0f, new Vector2(5400.0f, 1000.0f));
			bullet.QueueFree();
			GD.Print("cannon angle: ", a);

			// if (a <= 1.01f * (Mathf.Pi / 2.0f) && a >= -1.01f * (Mathf.Pi / 2.0f))
			// {
				_cannon.RotateToAndSignal(a);
			// }
			// });

			AddChild(target);
		}

		private Vector2 GenerateTargetSpawnlocation()
		{
			var pos = new Vector2(0.0f, _anchor2.GlobalPosition.Y);


			Random rand = new Random();
			pos.X = (rand.Next(0, 2) == 0) ? _anchor1.GlobalPosition.X : _anchor2.GlobalPosition.X;

			return pos;
		}

		private void MenuButton()
		{
			var button = GetNode<Button>("Button");
			var pos = button.GlobalPosition;

			PackedScene menuScene = GD.Load<PackedScene>("res://Prefabs/UI/Menu.tscn");
			var menu = menuScene.Instantiate() as Menu;
			menu.GlobalPosition = new Vector2(pos[0] - 450, pos[1] + 500);
			AddChild(menu);
		}

		private void SettingsButton()
		{
			var button = GetNode<Button>("Button2");
			var pos = button.GlobalPosition;

			PackedScene panelScene = GD.Load<PackedScene>("res://Prefabs/UI/SettingsPanel.tscn");
			var panel = panelScene.Instantiate() as SettingsPanel;
			panel.GlobalPosition = new Vector2(pos[0] + 250, pos[1] + 300);
			AddChild(panel);
		}
	}
}
