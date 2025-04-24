using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class AccelerationTracker : MonoBehaviour
{
    // This script tracks the acceleration of the GameObject it is attached to.
    // It uses the Rigidbody component to calculate the acceleration based on the velocity.

    private Rigidbody rb;

    private Vector3 lastVelocity;
    private Vector3 acceleration;

    private Vector3 velocity;
    public Vector3 Acceleration { get { return acceleration; } }

    public Vector3 TotalAccelerationInMetersPerSecond
    {
        get
        {
            return acceleration; // Convert from cm/s^2 to m/s^2
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastVelocity = rb.linearVelocity;
    }

    private void FixedUpdate()
    {
        velocity = rb.linearVelocity;
        acceleration = (velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = velocity;
        // You can use the acceleration variable for your logic here
        Debug.Log("Acceleration in Meters Per Second: " + TotalAccelerationInMetersPerSecond);
    }
}
