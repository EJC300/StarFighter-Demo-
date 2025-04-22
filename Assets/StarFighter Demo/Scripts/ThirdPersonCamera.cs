using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //Third person chase camera
    public Transform cursorObject;
    public Transform target; // The target to follow
    private float distance = 2.0f; // Distance from the target
    private float height = 2.0f; // Height above the target
    private float heightDamping = 2.0f; // Damping for height
    private float rotationDamping = 10.0f; // Damping for rotation
    private float lookAtHeight = 1.0f; // Height to look at
    private AccelerationTracker accelerationTracker; // Acceleration tracker for the target


    private void KeepCameraZRotationOfTarget()
    {
        if (target != null)
        {
            // Get the target's Z rotation
            float targetZRotation = target.eulerAngles.z;

            // Decompose the camera's current rotation into a quaternion
            Quaternion currentRotation = transform.rotation;

            // Create a new rotation that preserves the camera's X and Y rotation but applies the target's Z rotation
            Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, targetZRotation);

            // Smoothly interpolate to the new rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationDamping);
        }
    }

    private void AffectFieldOfViewByAcceleration()
    {
        if (accelerationTracker != null)
        {
            // Calculate the field of view based on acceleration
            float acceleration = accelerationTracker.Acceleration.magnitude;
            Camera.main.fieldOfView = Mathf.Lerp(60, 90, (acceleration / 10) * Time.deltaTime * 2);
        }
    }
    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for ThirdPersonCamera.");
            return;
        }
        // Find the AccelerationTracker component on the target
        accelerationTracker = target.GetComponent<AccelerationTracker>();
        if (accelerationTracker == null)
        {
            Debug.LogError("No AccelerationTracker found on the target.");
            return;
        }

    }
    private void Update()
    {
       
        AffectFieldOfViewByAcceleration();
        
    }
    private void LateUpdate()
    {


        if (target == null)
            return;
        // Calculate the desired position

        Vector3 wantedPosition = (target.position - cursorObject.position) + Vector3.up * height - target.forward * distance;
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * heightDamping);
        // Calculate the rotation

        Quaternion baseRotation = Quaternion.LookRotation(((target.position) - transform.position) + Vector3.up * lookAtHeight);
        float targetZRotation = target.eulerAngles.z;
        Quaternion targetRotation = Quaternion.Euler(baseRotation.eulerAngles.x, baseRotation.eulerAngles.y, targetZRotation);

        // Smoothly interpolate to the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100);



    }
}
