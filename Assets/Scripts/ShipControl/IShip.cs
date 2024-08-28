namespace ShipControl
{
    public interface IShip
    {
        void ThrustForward();
        void ThrustBackward();
        void ThrustLeft();
        void ThrustRight();
        void EndThrustForward();
        void EndThrustBackward();
        void EndThrustLeft();
        void EndThrustRight();
        void FireMainWeaponOnce(bool isFiring);
        void FireMainWeaponContinuous(bool isFiring);
        void HandleBrakingThrusters();
        void HandleBrakingAngularThrusters();
    }
}
