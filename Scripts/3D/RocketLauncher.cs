using Godot;

namespace Zenitka.Scripts._3D;

// public readonly struct RocketConfig
// {
// 	public readonly float Speed, Acceleration, SelfPropellingAcceleration, LinearDrag, Mass;

// 	public ProjectileConfig(float velocity,
// 							float acceleration,
// 							float selfPropellingAcceleration,
// 							float linearDrag,
// 							float mass)
// 	{
// 		Speed = velocity;
// 		Acceleration = acceleration;
// 		SelfPropellingAcceleration = selfPropellingAcceleration;
// 		LinearDrag = linearDrag;
// 		Mass = mass;
// 	}
// }

public partial class RocketLauncher : StaticBody3D, IWeapon
{
	public override void _Ready()
	{

	}

	public void Fire(DynamicBody target) {
		
	}
}
