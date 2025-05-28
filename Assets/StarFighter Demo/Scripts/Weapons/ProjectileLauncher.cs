using System.Collections;
using UnityEngine;
using Player;
namespace Weapons
{
    public class ProjectileLauncher : MonoBehaviour
    {
        ///Reference to SpaceShipController
        [SerializeField] private SpaceShipController spaceShipController;


        /// Reference to the projectile prefab
        [SerializeField] private GameObject projectilePrefab;
        /// Reference to the projectile spawn point
        [SerializeField] private Transform projectileSpawnPoint;
        /// Fire Rate of the projectile
        /// Energy drain of the launcher
        [SerializeField] private float fireRate = 0.5f; // Time between shots

        [SerializeField] private float energyDrain = 10f; // Energy drained per shot

        /// Reference ot the Projectile prefab's projecile script
        private Projectile projectile;
        private float nextFire = 0f; // Time until the next shot can be fired
        /// Reference to the ship's Rigidbody
        private Rigidbody shipBody { get { return transform.parent.parent.GetComponent<Rigidbody>(); } } // Rigidbody component of the ship

        void SpawnProjectile()
        {
            // Instantiate the projectile prefab at the spawn point
            GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            // Get the Projectile component of the instantiated projectile
            projectile = projectileInstance.GetComponent<Projectile>();
            // Setup the projectile with the ship's Rigidbody
            projectile.SetupBullet(shipBody);
        }

        public void FireWapon()
        {
            if (Time.time > nextFire)
            {
                // Check if the player has enough energy to fire
                if (spaceShipController.GetCurrentEnergy() > 5f)
                {
                    // Fire the projectile
                    SpawnProjectile();
                    // Drain energy from the ship
                    spaceShipController.DrainEnergy((int)energyDrain);
                    // Set the time for the next shot
                    nextFire = Time.time + fireRate;

                }
            }


        }


    }
}
