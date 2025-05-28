using UnityEngine;
using Weapons;
using StarterTemplates;
namespace Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private ProjectileLauncher[] projectileLaunchers; // Array of projectile launchers
                                                                           //TODO : Add an ability to switch between different weapons
        private void Update()
        {
            // Check if the player is pressing the fire button
            if (Singleton.instance.PlayerInput.FireInput())
            {
                Debug.Log("Fire button pressed");
                // Loop through each projectile launcher and fire it
                foreach (var launcher in projectileLaunchers)
                {
                    launcher.FireWapon();
                }
            }
        }

    }
}
