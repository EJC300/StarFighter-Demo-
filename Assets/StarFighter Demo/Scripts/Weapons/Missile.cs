using UnityEngine;
namespace Weapons
{
    public class Missile : Projectile
    {
        //Get Target if applicable
        //TODO make a targetable object with a property of rigidbody
        [SerializeField] private Rigidbody targetBody;
        
        //Move in a constant line 

        private void Move()
        {
            rb.linearVelocity += transform.forward * bullet.Speed * Time.fixedDeltaTime;
        }

        //Clamp velocity for gameplay reasons
        private void ClampVelocity()
        {
            rb.maxLinearVelocity = this.bullet.Speed;
        }


        //Lead target very simple integration
        private void LeadTarget()
        {
            var travelTime = this.bullet.Speed / (targetBody.position - transform.position).sqrMagnitude;
            var lead = targetBody.position + targetBody.linearVelocity * travelTime;
         
            
            
            Quaternion lookAt = Quaternion.LookRotation( lead - transform.position,targetBody.transform.up);

            Quaternion slerpedRotation = Quaternion.Slerp(transform.rotation,lookAt,Time.deltaTime * bullet.Speed * 100);

            transform.rotation = slerpedRotation;
        }

        //Explode and do damage within radius after impact
        private void Explode()
        {
            //Query list of targets after sphercast 
               //If it is within range damage it
               //destroy self spawn explosion effect prefab
        }
        private void Update()
        {
            LeadTarget();
        }
        private void FixedUpdate()
        {
            Move();
            ClampVelocity();


        }
    }
}