namespace Zenitka.Scripts._2D;

public delegate void WeaponCallback(int ammoUsed, int targetsHit);

public readonly record struct WeaponProjectileProps(PBodyProps PBody, float Speed);

public interface IWeapon {
    public bool IsBusy { get; }
    
    public void OnTargetDetected(BallisticBody target, WeaponCallback callback);
}