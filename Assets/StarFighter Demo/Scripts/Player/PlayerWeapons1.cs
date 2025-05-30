using UnityEngine;
using Weapons;
using StarterTemplates;
namespace Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private ProjectileLauncher[] bulletLaunchers; // Array of bullet launchers
        [SerializeField ] private ProjectileLauncher[] missileLaunchers;
        private bool allGuns = true;
        private int selectedGunIndex;
      [SerializeField]  private int currentMissileIndex;
        

        private void FireAllGuns()
        {
            if(allGuns)
            {
                foreach (var launcher in bulletLaunchers)
                {
                    launcher.FireWapon();
                }
            }
        }
        private void ShowLockOn()
        {

        }
        private void SelectMissile()
        {
           if( Singleton.instance.PlayerInput.CycleMissiles())
           {
                currentMissileIndex = (currentMissileIndex + missileLaunchers.Length - 1) % missileLaunchers.Length;
           }
        }
        private void FireSelectedMissile()
        {
           if(Singleton.instance.PlayerInput.FireMissile())
            {
                missileLaunchers[currentMissileIndex].FireWapon();
            }
        }
        private void SwitchGun()
        {
            //Use switch gun key to switch between guns all guns switched off if the index is at end then all guns on
            //The value will also go with the hud icon and weapon switch sound
            if (Singleton.instance.PlayerInput.SwitchWeapon())
            {
                allGuns = false;
                selectedGunIndex = (selectedGunIndex + bulletLaunchers.Length-1) % bulletLaunchers.Length;
            }
            else if (Singleton.instance.PlayerInput.FullGuns())
            {
                allGuns = true;
            }
        }
        private void FireSelectedGun()
        {
            if (!allGuns)
            {
                bulletLaunchers[selectedGunIndex].FireWapon();
            }
        }
        //Missile Launchers;                                                                  //TODO : Add an ability to switch between different weapons
        private void Update()
        {
            SwitchGun();
            SelectMissile();
            FireSelectedMissile();
            if (Singleton.instance.PlayerInput.FireInput())
            {
                Debug.Log("Fire button pressed");
           
                FireAllGuns();
                FireSelectedGun();
            }
        }

    }
}
