using UnityEngine;
namespace SpaceShip
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Scriptable Objects/Ship")]
    public class Ship : ScriptableObject
    {
        public Rigidbody rb;
        public float thrustForce = 10f;
        public float maneuveringThrustForce = 5.0f;

        public float rotationSpeed = 100f;
        public float maxSpeed = 20f;

        public float AfterBurnerForce = 20f;
        public float AfterSpeed = 100f;
        public float afterBurnerDrainRate = 1f;
    }
}