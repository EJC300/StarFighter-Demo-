using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class SpaceShipRCS : MonoBehaviour
{
    
    [SerializeField] private Ship spaceShip;
    private Rigidbody rb { get { return GetComponent<Rigidbody>(); } }
    private Vector3 Torque;


    public void Roll(float input)
    {
        Torque.z = input;
    }

    public void Pitch(float input)
    {
        Torque.x = input;
    }

    public void Yaw(float input)
    {
        Torque.y = input;
    }

    private void FixedUpdate()
    {
        Vector3 localTorque = transform.TransformDirection(Torque);
        rb.AddTorque(localTorque);
    }
}
