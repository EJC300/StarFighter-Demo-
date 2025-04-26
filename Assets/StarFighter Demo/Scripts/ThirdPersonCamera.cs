using UnityEngine;
namespace Cameras {
    public class ThirdPersonCamera : MonoBehaviour
    {

        [SerializeField] private Transform target; // The target to follow

        [SerializeField] private float distance = 5.0f; // Distance from the target

        [SerializeField] private float height = 2.0f; // Height above the target

        [SerializeField] private float damping = 5.0f; // Damping factor for smooth movement

        [SerializeField] private float rotationDamping = 10.0f; // Damping factor for smooth rotation
    


        void LateUpdate()
        {
            if (target == null)
                return;

            // Calculate the desired position
            Vector3 desiredPosition = target.position + Vector3.up * height - target.forward * distance;





            // Smoothly move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            // Calculate the rotation to look at the target
            //Smoothly change direction from current up direction slowly using cross product
            Vector3 currentUp = Vector3.Cross(transform.forward, target.transform.right);
            Vector3 targetUp = Vector3.Lerp(transform.up,currentUp, Time.deltaTime * rotationDamping * 20);
            Quaternion desiredRotation = Quaternion.LookRotation((target.position - transform.position).normalized,targetUp);
   
          
            // Extract the current Euler angles of the desired rotation
            Vector3 desiredEulerAngles = desiredRotation.eulerAngles;

            if(desiredEulerAngles == Vector3.zero)
            {
                return;
            }


            // Replace the Z rotation with the target's Z rotation
            //desiredEulerAngles.z = Mathf.Lerp(transform.eulerAngles.z, target.eulerAngles.z,rotationDamping * Time.deltaTime);

            // Create a new rotation with the modified Euler angles
           // desiredRotation = Quaternion.Euler(desiredEulerAngles);
            // Smoothly rotate the camera to the desired rotation
            

            Quaternion finalRotation = desiredRotation;
            

            transform.rotation = Quaternion.Slerp(transform.rotation,finalRotation,rotationDamping * Time.deltaTime);

            


        }
      
    }





}
