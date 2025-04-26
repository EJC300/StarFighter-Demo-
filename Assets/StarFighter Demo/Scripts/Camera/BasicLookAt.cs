using UnityEngine;

public class BasicLookAt : MonoBehaviour
{
    [SerializeField] private Transform target; // The target to look at

    [SerializeField] private float rotationSpeed = 5.0f; // Speed of rotation

    [SerializeField] private float heightOffset = 1.0f; // Height offset from the target

    [SerializeField] private float distance = 5.0f; // Distance from the target

    void Update()
    {
        if (target == null)
            return;
        // Calculate the desired position
        Vector3 desiredPosition = target.position + Vector3.up * heightOffset - target.forward * distance;
        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);
        // Calculate the rotation to look at the target
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        // Smoothly rotate the camera to the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }




}
