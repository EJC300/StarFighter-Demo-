using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //Third person chase camera
    public Transform cursorObject;
    public Transform target; // The target to follow
    private float distance = 2.0f; // Distance from the target
    private float height = 5.0f; // Height above the target
    private float heightDamping = 2.0f; // Damping for height
    private float lookAtHeight = 1.0f; // Height to look at
    private AccelerationTracker accelerationTracker; // Acceleration tracker for the target


    private void LookAtTargetWithDotProduct()
    {
        if (target == null)
            return;
        // Calculate the direction to the target
        Vector3 targetDirection = (target.position - transform.position).normalized;
        // Get the ship's local axes
        Vector3 shipForward = target.transform.forward;
        Vector3 shipRight = transform.right;
        Vector3 shipUp = transform.up;
        // Calculate dot products to determine alignment
        float forwardDot = Vector3.Dot(targetDirection, shipForward); // Alignment with forward
        float rightDot = Vector3.Dot(targetDirection, shipRight);     // Alignment with right
        float upDot = Vector3.Dot(targetDirection, shipUp);           // Alignment with up
        // Calculate torque based on target position
        float pitchTorque = -upDot * Mathf.Abs(upDot); // Negative to pitch up when target is above
        float yawTorque = rightDot * Mathf.Abs(rightDot); // Positive to yaw toward the target
 
        // Apply torque to the camera
        transform.Rotate(pitchTorque, yawTorque, 0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, target.transform.eulerAngles.z);
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

        LookAtTargetWithDotProduct();

        // Smoothly interpolate to the desired rotation
   



    }
}
