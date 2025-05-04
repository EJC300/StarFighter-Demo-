using UnityEngine;

public class BasicLookAt : MonoBehaviour
{
    [SerializeField] private Transform target; // The target to look at

    [SerializeField] private float rotationSpeed = 5.0f; // Speed of rotation

    [SerializeField] private float heightOffset = 1.0f; // Height offset from the target

    [SerializeField] private float distance = 5.0f; // Distance from the target
    private Vector3 offset = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        offset = new Vector3(0, 2, -10);
    }
    void FixedUpdate()
    {
        if (target == null)
            return;
        // Calculate the desired position
        Vector3 desiredPosition = target.position + (target.rotation * offset);
        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition,ref velocity, Time.deltaTime * rotationSpeed);
        // Calculate the rotation to look at the target
       // Quaternion desiredRotation = Quaternion.LookRotation(target.position,target.up);
        // Smoothly rotate the camera to the desired rotation
       // transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
       transform.LookAt(target,target.up);
    }




}
