using Godot;
using System;
using Zenitka.Scripts._2D.Targets;


namespace Zenitka.Scripts._2D
{
	public partial class Main2D : Node2D
	{
		private static float BARREL_LENGTH = 64f;
		private static float MUZZLE_SPEED = 2000f;
		private static float TARGET_SPEED = 2000f;

		private PackedScene _targetScene;
		private PackedScene _bulletScene;

		private Cannon _cannon;

		private Node2D _anchor1;
		private Node2D _anchor2;

		private int _firedBurstBulletCount = 0;

		private static Random _rng = new Random();

		private Label _ammoLabel;
		private Label _detectedLabel;
		
		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_anchor1 = GetNode<Node2D>("Anchor");
			_anchor2 = GetNode<Node2D>("Anchor2");

			_ammoLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/UsedAmmo");
			_detectedLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/DetectedTargets");
			

			_targetScene = GD.Load<PackedScene>("res://Prefabs/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/Bullet.tscn");
		}

		private void OnCannonGunReady(float angleRad, Vector2 headPosition, float timeOfCollision)
		{

			var bullet = _bulletScene.Instantiate() as Bullet;
			
			bullet.SelfDestructionTime = timeOfCollision - 0.07f;;
			bullet.GlobalPosition = Vector2.Zero;
			bullet.GravityScale = 0f;
			bullet.StartAngle = angleRad;

			AddChild(bullet);
			MoveChild(bullet, 0);

			_ammoLabel.Text = (Int32.Parse(_ammoLabel.Text)  + 1).ToString();

			// if (_firedBurstBulletCount++ < 5)
			// 	ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout).OnCompleted(() =>
			// 	{
			// 	_cannon.RotateToAndSignal(angleRad + 0.02f, timeOfCollision);
			// 	}
			// );
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var target = _targetScene.Instantiate() as Target;
			_firedBurstBulletCount = 0;

			var targetState = CreateTarget();
			//startPos.X += 5000f;

			target.GlobalPosition = targetState.Position;
			// target.StartVelocity = 100f;
			// target.StartAngle = Mathf.Pi / 3f;
			target.StartVelocity = targetState.Velocity.Length();
			target.StartAngle = -targetState.Velocity.Angle();

			AddChild(target);
			MoveChild(target, 0);
			
			_detectedLabel.Text = (Int32.Parse(_detectedLabel.Text)  + 1).ToString();

			//target.StartVelocity = -target.StartVelocity;

			var (angle, timeOfCollision) = new Solver2D(
				new CannonState2D(
					Vector2.Zero,
					0f,
					_cannon.GetAngle(),
					Settings.Settings2D.DefaultGun.AngularVelocity,
					Settings.Settings2D.DefaultGun.BulletSpeed,
					Settings.Settings2D.DefaultGun.AirResistance / Settings.Settings2D.DefaultGun.BulletMass),
				targetState,
				new Vector2(0f, Settings.Settings2D.DefaultGun.Gravity)
			).Aim();

			_cannon.RotateToAndSignal(angle, timeOfCollision);

			// var bullet = _bulletScene.Instantiate() as Target;
			// bullet._Ready();
			// float[] a = Math2D.GetAngle(_cannon.GlobalPosition, startPos, bullet, target, 9.8f,
			// 	_cannon.GunRotationSpeed, _cannon.Rotation, 1.0f, new Vector2(5400.0f, 1000.0f));
			// bullet.QueueFree();

			// _cannon.RotateToAndSignal(a[0] - 0.1f, a[0]);
		}
 
		private ParticleState2D CreateTarget()
		{
			Random rand = new Random();

			bool kind = rand.Next(2) == 0;
			//var angle = -0.1f;
			var angle = MathUtils.RandRange(-Mathf.Pi / 6f, Mathf.Pi / 6f);
			
			Vector2 pos, vel;

			if (kind) {
				pos = new Vector2(_anchor1.GlobalPosition.X, MathUtils.RandRange(_anchor2.GlobalPosition.Y, -300f));
				vel = Vector2.FromAngle(angle);
			} else {
				pos = new Vector2(_anchor2.GlobalPosition.X, MathUtils.RandRange(_anchor2.GlobalPosition.Y, -300f));
				vel = Vector2.FromAngle(Mathf.Pi - angle);
			}

			vel *= Settings.Settings2D.DefaultTarget.Velocity;

			return new ParticleState2D(pos, vel, Vector2.Zero, Settings.Settings2D.DefaultTarget.AirResistance / Settings.Settings2D.DefaultTarget.Mass);
		}

		private void SettingsButton()
		{
			var settingsButtonAnimation = GetNode<AnimationPlayer>("CanvasLayer/Button2/AnimationPlayer");
			var settingsPanel = GetNode<Control>("CanvasLayer/SettingsPanel");
			if (!settingsPanel.Visible)
			{
				settingsPanel.Show();
				var animation = GetNode<AnimationPlayer>("CanvasLayer/SettingsPanel/Animation");
				animation.Play("in");
				settingsButtonAnimation.Play("in");
			}
			else
			{
				// GD.Print("out");
				var animation = GetNode<AnimationPlayer>("CanvasLayer/SettingsPanel/Animation");
				animation.Play("out");
				settingsButtonAnimation.Play("out");


			}

		}
	}
}

