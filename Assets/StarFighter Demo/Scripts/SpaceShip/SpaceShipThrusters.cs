
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpaceShipThrusters : MonoBehaviour
    {

        private Rigidbody rb;
        float prevVeloX;
        float prevVeloY;
        float prevVeloZ;

        [SerializeField] Ship shipData;
        private Vector3 prevVelo;
        private float thrustForce
        {
            get { return shipData.thrustForce; }
            set { shipData.thrustForce = value; }
        }

        private float rotationSpeed
        {
            get { return shipData.rotationSpeed; }
            set { shipData.rotationSpeed = value; }
        }
        public float RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }

        private float maxSpeed
        {
            get { return shipData.maxSpeed; }
            set { shipData.maxSpeed = value; }
        }

        private float AfterBurnerForce
        {
            get { return shipData.AfterBurnerForce; }
            set { shipData.AfterBurnerForce = value; }
        }
        private float AfterSpeed
        {
            get { return shipData.AfterSpeed; }
            set { shipData.AfterSpeed = value; }
        }
        private float afterBurnerDrainRate
        {
            get { return shipData.afterBurnerDrainRate; }
            set { shipData.afterBurnerDrainRate = value; }
        }

        public float AfterBurnerDraineRate
        {
            get { return afterBurnerDrainRate; }
            set { afterBurnerDrainRate = value; }
        }
        public Vector3 totalLinearVelocityInMetersPerSecond
        {
            get { return rb.linearVelocity * 100; }
        }

        public Vector3 totalAngularVelocityInDegreesPerSecond
        {
            get { return rb.angularVelocity * Mathf.Rad2Deg; }
        }

        public void ApplyThrust(Vector3 direction)
        {
            // Apply thrust in the specified direction
            rb.AddForce((direction).normalized * thrustForce, ForceMode.Acceleration);
            rb.maxLinearVelocity = maxSpeed;
        }

        public void ApplyTorque(Vector3 torque)
        {
            // Apply torque using Quaternion
            Quaternion xAngle = Quaternion.AngleAxis(torque.x,Vector3.right);
            Quaternion yAngle = Quaternion.AngleAxis(torque.y,Vector3.up);
            Quaternion zAngle = Quaternion.AngleAxis(torque.z,Vector3.forward);
            rb.MoveRotation(transform.rotation * (xAngle * yAngle * zAngle));

         
        }

        public void ApplyAfterBurner(float input)
        {
            // Apply afterburner force
            rb.AddForce(rb.transform.rotation * rb.transform.TransformDirection(Vector3.forward) * AfterBurnerForce * input, ForceMode.VelocityChange);
            rb.maxLinearVelocity = AfterSpeed;
        }
        private void ApplyRotationalDrag()
        {
            // Apply rotational drag to the ship
            Vector3 angularVelocity = rb.angularVelocity;
            Vector3 dragTorque = -angularVelocity * 10;
            rb.AddTorque(dragTorque, ForceMode.Acceleration);
        }
      


        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.maxAngularVelocity = rotationSpeed;
            rb.maxLinearVelocity = maxSpeed;
        }
        private void ApplyDrag()
        {
            // Apply drag to the ship
            Vector3 dragForce = -rb.linearVelocity.normalized * rb.linearVelocity.magnitude * 0.1f;
            rb.AddForce(dragForce, ForceMode.Acceleration);
        }
    
        private void ApplySlowDownForce()
        {
            // Apply slow down force
            Vector3 slowDownForce = -rb.linearVelocity.normalized * thrustForce * 0.75f;
            rb.AddForce(slowDownForce, ForceMode.Acceleration);
        }
        private void FixedUpdate()
        {
            // Apply rotational drag
          
         
            //ApplyRotationalDrag();
            ApplySlowDownForce();
            ApplyDrag();
        
        }

    }

}