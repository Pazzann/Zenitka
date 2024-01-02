using Godot;
using System;

namespace Zenitka.Scripts._2D.Targets
{
	public partial class Rocket2 : RigidBody2D
	{
		private AnimatedSprite2D _animation;

		private CollisionShape2D _rocketCollision;

		private float _mainEngineForce;
		private float _sideEngineForce;

		private float _currentFuel;

		public Node2D FollowTarget { get; set; }

		private Label _destroyedLabel;
		public float Weight { get; set; }
		public float DragCoefficient { get; set; }
		public float StartVelocity { get; set; }
		public Vector2 StartPosition { get; set; }
		public float ConstantAcceleration { get; set; }


		public float CurrentTime = 0.0f;
		public float StartAngle = Mathf.Pi;

		public float ManeuveringEngineActivationDelaySec = 1f;

		public bool IsExploded;

		private bool _scoreUpdated = false;

		public override void _Ready()
		{
			_mainEngineForce = Settings.Settings2D.RocketGun.RocketForce;
			_sideEngineForce = Settings.Settings2D.RocketGun.RocketForce;

			StartPosition = new Vector2(GlobalPosition.X, GlobalPosition.Y);

			Weight = Settings.Settings2D.RocketGun.RocketMassWithoutFuel + Settings.Settings2D.RocketTarget.FuelMass;
			_currentFuel = Settings.Settings2D.RocketGun.FuelMass;
			DragCoefficient = Settings.Settings2D.RocketGun.AirResistance;
			StartVelocity = Settings.Settings2D.RocketGun.InitialVelocity;
			_animation = GetChild(0) as AnimatedSprite2D;
			_rocketCollision = GetChild(1) as CollisionShape2D;

			_animation.Play("fly");

			_destroyedLabel = GetNode<Label>("../CanvasLayer/Statistics/ColorRect/DestroyedTargets");
		}

		public override void _IntegrateForces(PhysicsDirectBodyState2D state)
		{
			if (IsExploded)
				state.LinearVelocity = new Vector2(0, 0);
			base._IntegrateForces(state);

			// if (!IsExploded)
			// 	Rotation = state.LinearVelocity.Normalized().Angle() + Mathf.Pi / 2f;

			var d = (FollowTarget.GlobalPosition - GlobalPosition).Normalized();

			if (GlobalTransform.X.Normalized().Cross(d) > 0.1f)
				ApplyForce(Mass * GlobalTransform.X * _mainEngineForce / Weight, new Vector2(-20f, 33f));
			else if (GlobalTransform.X.Normalized().Cross(d) < -0.1f)
				ApplyForce(Mass * GlobalTransform.X * _mainEngineForce / Weight, new Vector2(20f, 33f));

			// if (CurrentTime > ManeuveringEngineActivationDelaySec) {
			// 	ConstantForce = Mass * (FollowTarget.GlobalPosition - GlobalPosition).Normalized() * _mainEngineForce / Weight;
			// } else
			// 	ConstantForce = Mass * Vector2.Up * _mainEngineForce / Weight;
		}

		public override void _PhysicsProcess(double delta)
		{
			CurrentTime += (float)delta;
			if (_currentFuel > 0 && (FollowTarget.GlobalPosition - GlobalPosition).Length() > 10f)
			{
				_currentFuel -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
			}
			else
			{
				if (IsExploded)
					return;
				_currentFuel = 0;
				_mainEngineForce = 0;
				Destroy();
			}

			Weight = Settings.Settings2D.RocketTarget.RocketMassWithoutFuel + _currentFuel;
		}

		public override void _Process(double delta)
		{
			base._Process(delta);

			if (!IsInstanceValid(FollowTarget))
				QueueFree();
		}

		public void Destroy()
		{
			_animation.Play("explode");

			_rocketCollision.SetDeferred("disabled", true);

			if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
				_animation.AnimationLooped += QueueFree;

			IsExploded = true;
		}

		private void _on_body_entered(Node body)
		{
			(body as BallisticBody).Destroy();
			Destroy();

			if (!_scoreUpdated)
			{
				_destroyedLabel.Text = (Int32.Parse(_destroyedLabel.Text) + 1).ToString();
				_scoreUpdated = true;
			}
		}
		private void _on_button_pressed()
		{
			Destroy();
		}
	}
}







