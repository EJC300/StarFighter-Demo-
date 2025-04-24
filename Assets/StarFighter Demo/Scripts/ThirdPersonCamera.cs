using UnityEngine;
namespace Cameras {
    public class ThirdPersonCamera : MonoBehaviour
    {

        [SerializeField] private Transform target; // The target to follow

        [SerializeField] private float distance = 5.0f; // Distance from the target

        [SerializeField] private float height = 2.0f; // Height above the target

        [SerializeField] private float damping = 5.0f; // Damping factor for smooth movement

        [SerializeField] private float rotationDamping = 10.0f; // Damping factor for smooth rotation

        private Vector3 velocity = Vector3.zero; // Used for smoothing the camera movement
        
     

        void FixedUpdate()
        {
            if (target == null)
                return;

            // Calculate the desired position
            Vector3 desiredPosition = target.position + Vector3.up * height - target.forward * distance;





            // Smoothly move the camera to the desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition,ref velocity, Time.deltaTime * damping);
            // Calculate the rotation to look at the target
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);

            // Extract the current Euler angles of the desired rotation
            Vector3 desiredEulerAngles = desiredRotation.eulerAngles;

            // Replace the Z rotation with the target's Z rotation
            desiredEulerAngles.z = target.eulerAngles.z;

            // Create a new rotation with the modified Euler angles
            desiredRotation = Quaternion.Euler(desiredEulerAngles);
            // Smoothly rotate the camera to the desired rotation


      
     
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);

        }

    }





}
