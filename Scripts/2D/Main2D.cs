using Godot;
using System;
using Zenitka.Scripts._2D.Targets;


namespace Zenitka.Scripts._2D
{
	public partial class Main2D : Node2D
	{
		private PackedScene _targetScene;
		private PackedScene _bulletScene;
		private PackedScene _rocketTargetScene;
		private PackedScene _rocketAmmoScene;

		private Cannon _cannon;
		private Node2D _rocketCannon;

		private Node2D _anchor1;
		private Node2D _anchor2;

		private int _firedBurstBulletCount = 0;

		private Label _ammoLabel;
		private Label _detectedLabel;

		private const float BURST_STEP = 0.01f;

		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_anchor1 = GetNode<Node2D>("Anchor");
			_anchor2 = GetNode<Node2D>("Anchor2");

			_ammoLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/UsedAmmo");
			_detectedLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/DetectedTargets");

			_targetScene = GD.Load<PackedScene>("res://Prefabs/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/Bullet.tscn");
			_rocketTargetScene = GD.Load<PackedScene>("res://Prefabs/Rocket1.tscn");
			
			_rocketAmmoScene = GD.Load<PackedScene>("res://Prefabs/Rocket2.tscn");
			_rocketCannon = GetNode<Node2D>("RocketCannon");
		}

		private void OnCannonGunReady(float angleRad, Vector2 headPosition, float timeOfCollision)
		{

			var bullet = _bulletScene.Instantiate() as Bullet;

			bullet.SelfDestructionTime = timeOfCollision - 0.07f;
			bullet.GlobalPosition = Vector2.Zero;
			bullet.GravityScale = 0f;
			bullet.StartAngle = angleRad;

			AddChild(bullet);

			_ammoLabel.Text = (Int32.Parse(_ammoLabel.Text) + 1).ToString();

			if (++_firedBurstBulletCount < Settings.Settings2D.DefaultGun.SalvoSize)
			{
				ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout).OnCompleted(() =>
				{
					_cannon.RotateToAndSignal(angleRad + BURST_STEP, timeOfCollision);
				});
			}
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var scene = (Settings.Settings2D.IsNotDefaultTarget) ? _rocketTargetScene : _targetScene;
			var target = scene.Instantiate() as Target;
			_firedBurstBulletCount = 0;

			var targetState = CreateTarget();

			target.GlobalPosition = targetState.Position;
			target.StartVelocity = targetState.Velocity.Length();
			target.StartAngle = -targetState.Velocity.Angle();

			AddChild(target);

			_detectedLabel.Text = (Int32.Parse(_detectedLabel.Text) + 1).ToString();
			
			if (Settings.Settings2D.IsNotDefaultGun)
			{
				Marker2D pos = _rocketCannon.GetChild(0) as Marker2D;

				Rocket2 rocket = _rocketAmmoScene.Instantiate() as Rocket2;
				rocket.GlobalPosition = new Vector2(0,0);
				rocket.FollowTarget = target;
				
				GD.Print(1);
				
				AddChild(rocket);
				
				
				return;
			}

			var (angle, timeOfCollision) = new Solver2D(
				new CannonState2D(
					Vector2.Zero,
					0f,
					_cannon.GetAngle(),
					Settings.Settings2D.DefaultGun.AngularVelocity,
					Settings.Settings2D.DefaultGun.BulletSpeed,
					0f,
					Settings.Settings2D.DefaultGun.AirResistance / Settings.Settings2D.DefaultGun.BulletMass),
				targetState,
				new Vector2(0f, Settings.Settings2D.Gravity)
			).Aim();

			_cannon.RotateToAndSignal(angle - BURST_STEP * (Settings.Settings2D.DefaultGun.SalvoSize - 1) / 2f, timeOfCollision);
		}

		private ParticleState2D CreateTarget()
		{
			Random rand = new Random();

			bool kind = rand.Next(2) == 0;
			var angle = MathUtils.RandRange(-Mathf.Pi / 24f, Mathf.Pi / 6f);

			Vector2 pos, vel;

			if (kind)
			{
				pos = new Vector2(_anchor1.GlobalPosition.X, MathUtils.RandRange(_anchor1.GlobalPosition.Y, -1000f));
				vel = Vector2.FromAngle(angle);
			}
			else
			{
				pos = new Vector2(_anchor2.GlobalPosition.X, MathUtils.RandRange(_anchor2.GlobalPosition.Y, -1000f));
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

		public override void _Process(double delta)
		{
			var roc = GetNode<Node2D>("RocketCannon");
			var def = GetNode<Node2D>("Cannon");
			if (Settings.Settings2D.IsNotDefaultGun && roc.Visible == false)
			{
				roc.Show();
				def.Hide();
			}

			if (!Settings.Settings2D.IsNotDefaultGun && roc.Visible == true)
			{
				def.Show();
				roc.Hide();
			}
		}
	}
}

