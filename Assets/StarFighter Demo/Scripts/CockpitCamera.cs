using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CockpitCamera : MonoBehaviour
{
    // This script is used to control the cockpit camera of a spaceship in a space simulation game.
    // It allows the camera to follow the spaceship's movements and provide a realistic cockpit view.
    [SerializeField] private Transform target; // The target spaceship to follow
    private Vector3 offset; // Offset position of the camera from the target
    [SerializeField] private float smoothSpeed = 0.125f; // Speed of camera smoothing
    [SerializeField] private float rotationSpeed = 50f; // Speed of camera rotation
    [SerializeField] private float distance = 5.0f; // Distance from the target
    [SerializeField] private float height = 2.0f; // Height above the target
    private void Start()
    {
        offset = transform.position;
    }

    private void FixedUpdate()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Calculate the desired position of the camera based on the target's position and the offset
            Vector3 desiredPosition = target.position + target.up.normalized * height - target.forward.normalized * (distance);
            //Target Up
            // Calculate the desired position of the camera based on the target's position and the offset
            Vector3 TargetUp = Vector3.Cross(transform.forward, target.right);
            Vector3 targetToUp = Vector3.Lerp(transform.up, TargetUp, rotationSpeed);
            // Smoothly move the camera towards the desired position

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            transform.position = smoothedPosition;
            // Rotate the camera to look at the target
            Quaternion targetRotation = Quaternion.LookRotation((target.position - transform.position).normalized,targetToUp);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

