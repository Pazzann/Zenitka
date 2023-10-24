using Godot;
using System;
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

		private int _firedBurstBulletCount = 0;

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
			float angleRadF = angleRad;

			var bullet = _bulletScene.Instantiate() as Bullet;
			
			//bullet.Weight = Settings.Settings2D.DefaultGun.We
			
			bullet.SelfDestructionTime = timeOfCollision - 0.05f;
			bullet.Rotate(Mathf.Pi * 0.5f - angleRadF);
			bullet.Rotation = angleRad;

			bullet.GlobalPosition = new Vector2(0.0f, 0.0f);

			bullet.GravityScale = 0f;
			bullet.StartAngle = angleRad;

			AddChild(bullet);
			MoveChild(bullet, 0);

			// if (_firedBurstBulletCount++ < 5)
			// 	ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout).OnCompleted(() =>
			// 		{
			// 			_cannon.RotateToAndSignal(angleRad + 0.02f, timeOfCollision);
			// 		}
			// 	);
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var target = _targetScene.Instantiate() as Target;
			_firedBurstBulletCount = 0;

			var startPos = GenerateTargetSpawnlocation();
			//startPos.X += 5000f;

			target.GlobalPosition = startPos;
			AddChild(target);
			MoveChild(target, 0);

			//target.StartVelocity = -target.StartVelocity;

			var (angle, timeOfCollision) = new Solver2D(
				new CannonState2D(
					Vector2.Zero,
					BARREL_LENGTH,
					_cannon.GetAngle(),
					_cannon.GunRotationSpeed,
					1000f,
					0.05f),
				new ParticleState2D(
					startPos,
					new Vector2(target.StartVelocity, 0f),
					Vector2.Zero,
					0.05f),
				new Vector2(0f, 9.8f)
			).Aim();

			_cannon.RotateToAndSignal(angle, timeOfCollision - 0.05f);

			// var bullet = _bulletScene.Instantiate() as Target;
			// bullet._Ready();
			// float[] a = Math2D.GetAngle(_cannon.GlobalPosition, startPos, bullet, target, 9.8f,
			// 	_cannon.GunRotationSpeed, _cannon.Rotation, 1.0f, new Vector2(5400.0f, 1000.0f));
			// bullet.QueueFree();

			// _cannon.RotateToAndSignal(a[0] - 0.1f, a[0]);
		}
 
		private Vector2 GenerateTargetSpawnlocation()
		{
			var pos = new Vector2(0.0f, _anchor2.GlobalPosition.Y);


			Random rand = new Random();
			pos.X = (rand.Next(0, 2) == 0) ? _anchor1.GlobalPosition.X : _anchor2.GlobalPosition.X;

			pos.Y = rand.Next(-2000, -500);

			return pos;
		}

		

		private void SettingsButton()
		{
			var settingsButtonAnimation = GetNode<AnimationPlayer>("Button2/AnimationPlayer");
			var settingsPanel = GetNode<Control>("SettingsPanel");
			if (!settingsPanel.Visible)
			{
				settingsPanel.Show();
				var animation = GetNode<AnimationPlayer>("SettingsPanel/Animation");
				animation.Play("in");
				settingsButtonAnimation.Play("in");
			}
			else
			{
				// GD.Print("out");
				var animation = GetNode<AnimationPlayer>("SettingsPanel/Animation");
				animation.Play("out");
				settingsButtonAnimation.Play("out");


			}

		}
	}
}

