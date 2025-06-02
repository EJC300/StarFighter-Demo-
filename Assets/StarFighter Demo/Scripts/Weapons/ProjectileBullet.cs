using UnityEngine;
using DamageSystem;
namespace Weapons
{
    public class ProjectileBullet : Projectile
    {
        public override void ProjectileRun()
        {
           this.rb.linearVelocity += (transform.forward * this.bullet.Speed); // Set the linear velocity of the projectile
        }

        //Apply Damage
        public void OnCollisionEnter(Collision collision)
        {
            GameObject target = collision.gameObject;
            if(target == this.gameObject)
            {
                return;
            }

            else if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.SetDamage(this.bullet.Damage);
            }
        }

        private void FixedUpdate()
        {
            ProjectileRun();
        }
    }
}
