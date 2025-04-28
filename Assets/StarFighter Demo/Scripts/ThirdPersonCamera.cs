using UnityEngine;
namespace Cameras {
    public class ThirdPersonCamera : MonoBehaviour
    {

        [SerializeField] private Transform target; // The target to follow

        [SerializeField] private float distance = 5.0f; // Distance from the target

        [SerializeField] private float height = 2.0f; // Height above the target

        [SerializeField] private float damping = 5.0f; // Damping factor for smooth movement

        [SerializeField] private float rotationDamping = 10.0f; // Damping factor for smooth rotation

        [SerializeField] private float maxAcceleration = -1f; // 
                                                             //Camera shake based on velocity
        Vector3 shake = Vector3.zero;
        private Vector3 targetVelocity;
        private SpaceShipController spaceShipController
        {
            get { return target.GetComponent<SpaceShipController>(); }
        }
      
        private AccelerationTracker accelerationTracker
        {
            get { return target.GetComponent<AccelerationTracker>(); }
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;

            // Calculate the desired position
            //effect of the ship's velocity on the camera
            targetVelocity = transform.TransformDirection( spaceShipController.GetComponent<Rigidbody>().linearVelocity);
            // Calculate the desired position based on the target's position, height, and distance
            Vector3 desiredPosition = target.position + target.up * height - target.forward * (distance + targetVelocity.magnitude * 0.1f);


        

            if(accelerationTracker.TotalAccelerationInMetersPerSecond.z > maxAcceleration)
            {
                // Calculate the acceleration of the target
                float acceleration = accelerationTracker.Acceleration.magnitude;
                // Apply a shake effect based on the acceleration
                shake += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)) * acceleration * 20;
            }


            // Smoothly move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position , ( desiredPosition + shake), Time.deltaTime * damping);
            // Calculate the rotation to look at the target
            //Smoothly change direction from current up direction slowly using cross product
            Vector3 currentUp = Vector3.Cross(transform.forward, target.transform.right);
            Vector3 targetUp = Vector3.Lerp(transform.up,currentUp, Time.deltaTime * rotationDamping * 20);
            Vector3 lookDirection = ((target.position + target.forward * 200) - transform.position);
            Quaternion desiredRotation = Quaternion.LookRotation(lookDirection,targetUp);
   
          
            // Extract the current Euler angles of the desired rotation
            Vector3 desiredEulerAngles = desiredRotation.eulerAngles;

            


         

         

            // Replace the Z rotation with the target's Z rotation
            //desiredEulerAngles.z = Mathf.Lerp(transform.eulerAngles.z, target.eulerAngles.z,rotationDamping * Time.deltaTime);

            // Create a new rotation with the modified Euler angles
            // desiredRotation = Quaternion.Euler(desiredEulerAngles);
            // Smoothly rotate the camera to the desired rotation
            Vector3 currentRotationEulerAngles = transform.rotation.eulerAngles;
            // clamp the rotation to a certain range
          // desiredEulerAngles.y = Mathf.Clamp( transform.eulerAngles.y + desiredEulerAngles.y , -30, 30);
         
            Quaternion finalRotation = Quaternion.Euler(desiredEulerAngles.x,desiredEulerAngles.y,desiredEulerAngles.z);
            

            transform.rotation = Quaternion.Slerp(transform.rotation,finalRotation,rotationDamping * Time.deltaTime);

            


        }
      
    }





}
