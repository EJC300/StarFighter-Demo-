using System.Collections;
using UnityEngine;
using SpaceShip;
namespace Weapons
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private Rigidbody target;
        ///Reference to SpaceShipController
        [SerializeField] private SpaceShipController spaceShipController;

        [SerializeField] private bool IsMissileLauncher;

        [SerializeField] private int ammo;

        /// Reference to the projectile prefab
        [SerializeField] private GameObject projectilePrefab;
        /// Reference to the projectile spawn point
        [SerializeField] private Transform projectileSpawnPoint;
        /// Fire Rate of the projectile
        /// Energy drain of the launcher
        [SerializeField] private float fireRate = 0.5f; // Time between shots

        [SerializeField] private float energyDrain = 10f; // Energy drained per shot
        
        private float nextFire = 0f; // Time until the next shot can be fired
        /// Reference to the ship's Rigidbody
        private Rigidbody shipBody { get { return transform.parent.parent.GetComponent<Rigidbody>(); } } // Rigidbody component of the ship
        //Convert this into spawn missile and spawn bullet

    
        void SpawnProjectile()
        {
       

            if (IsMissileLauncher && ammo >= 0)
            {
                ammo -= 1;
                // Instantiate the projectile prefab at the spawn point
                GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                // Get the Projectile component of the instantiated projectile
                Projectile projectile = projectileInstance.GetComponent<Projectile>();
                // Setup the projectile with the ship's Rigidbody
                Missile missileToFire = projectile as Missile;
                projectile.SetupBullet(shipBody);
                if (missileToFire.targetBody == null)
                {
                    missileToFire.targetBody = target;
                }

               
             

                
            }
            else if(!IsMissileLauncher) 
            {
                // Instantiate the projectile prefab at the spawn point
                GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                // Get the Projectile component of the instantiated projectile
                Projectile projectile = projectileInstance.GetComponent<Projectile>();
                // Setup the projectile with the ship's Rigidbody
                Missile missileToFire = projectile as Missile;
                projectile.SetupBullet(shipBody);
            }

           

          
            
        }
     
        public void FireWapon()
        {
            if (Time.time > nextFire)
            {
                SpawnProjectile();
                // Check if the player has enough energy to fire
                if (spaceShipController.GetCurrentEnergy() > 5f && !IsMissileLauncher)
                {
                    // Fire the projectile
                
                    // Drain energy from the ship
                    

                        spaceShipController.DrainEnergy((int)energyDrain);
                    
                  
                        // Set the time for the next shot

                       

                }
                nextFire = Time.time + fireRate;
            }


        }
        private void Update()
        {
          
        }

    }
}
