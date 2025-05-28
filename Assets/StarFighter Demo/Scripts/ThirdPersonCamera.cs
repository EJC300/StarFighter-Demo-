using UnityEngine;
using Player;
using SpaceShip;
namespace Cameras {
    public class ThirdPersonCamera : MonoBehaviour
    {

        [SerializeField] private Transform target; // The target to follow

        [SerializeField] private float distance = 5.0f; // Distance from the target

        [SerializeField] private float height = 2.0f; // Height above the target

        [SerializeField] private float damping = 5.0f; // Damping factor for smooth movement

        [SerializeField] private float rotationDamping = 10.0f; // Damping factor for smooth rotation

        [SerializeField] private float maxAcceleration = -1f; // 
        private Vector3 offset;                  
        private Quaternion prevRotation;
        private Vector3 previousPosition = Vector3.zero;
        private Vector3 targetVelocity;
        private SpaceShipController spaceShipController
        {
            get { return target.GetComponent<SpaceShipController>(); }
        }
      
        private AccelerationTracker accelerationTracker
        {
            get { return target.GetComponent<AccelerationTracker>(); }
        }
        private void OnDisable()
        {
            prevRotation = transform.rotation;
            previousPosition = transform.position;
        }
        private void OnEnable()
        {
            transform.rotation = prevRotation;
            transform.position = previousPosition;
        }
        private void Start()
        {
            offset = Vector3.up * height - Vector3.forward * (distance);
        }
        private void FixedUpdate()
        {
            if (target == null)
                return;

         
            // Calculate the desired position based on the target's position, height, and distance
           Vector3 desiredPosition = target.position + (target.rotation * offset);

           

        


            // Smoothly move the camera to the desired
            // SmoothDamp 
            Vector3 targetPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref targetVelocity, Time.deltaTime * maxAcceleration);
            // Calculate the rotation to look at the target
            //Smoothly change direction from current up direction slowly using cross product
            Vector3 currentUp = Vector3.Cross(transform.forward, target.transform.right);
            Vector3 targetUp = Vector3.Lerp(transform.up,currentUp, Time.deltaTime * rotationDamping * 2);
            Vector3 lookDirection = ((target.position + target.forward.normalized * 200) - transform.position);
            Quaternion desiredRotation = Quaternion.LookRotation(lookDirection,targetUp);
   
          
            // Extract the current Euler angles of the desired rotation
            Vector3 desiredEulerAngles = desiredRotation.eulerAngles;

            


         

         

           
         
            Quaternion finalRotation = Quaternion.Euler(desiredEulerAngles.x,desiredEulerAngles.y,desiredEulerAngles.z);
            

            transform.rotation = Quaternion.Slerp(transform.rotation,finalRotation,rotationDamping * Time.deltaTime);

            transform.Rotate(Vector3.zero);


        }
      
    }





}
