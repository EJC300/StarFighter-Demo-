using UnityEngine;
namespace Weapons
{
    public class ProjectileBullet : Projectile
    {
        public override void ProjectileRun()
        {
           this.rb.linearVelocity += (transform.forward * this.bullet.Speed); // Set the linear velocity of the projectile
        }
       
        //Apply Damage


        private void FixedUpdate()
        {
            ProjectileRun();
        }
    }
}
