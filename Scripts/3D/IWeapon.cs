using Godot;

namespace Zenitka.Scripts._3D;

public delegate void WeaponStatisticsCallback(int ammoUsed, int targetsHit);

public interface IWeapon {
    public void OnTargetDetected(BallisticBody target, WeaponStatisticsCallback callback);
}