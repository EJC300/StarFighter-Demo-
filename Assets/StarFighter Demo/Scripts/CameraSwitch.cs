using UnityEngine;
using StarterTemplates;
public class CameraSwitch : MonoBehaviour
{
    // This script is used to switch between different camera views in a space simulation game.
    // It allows the player to toggle between a cockpit view and an external view of the spaceship.
    [SerializeField] private GameObject cockpitCamera; // Reference to the cockpit camera GameObject
    [SerializeField] private GameObject externalCamera; // Reference to the external camera GameObject
    [SerializeField] private GameObject externalUI; // Reference to the external UI GameObject
    [SerializeField] private GameObject cockpitUI; // Reference to the cockpit UI GameObject
    [SerializeField] private PlayerInput playerInput; // Reference to the PlayerInput component
    private void Start()
    {
        // Initialize the camera views by setting the cockpit camera active and the external camera inactive
        cockpitCamera.SetActive(true);
        externalCamera.SetActive(false);
        externalUI.SetActive(false);
        cockpitUI.SetActive(true);
        if(playerInput == null)
        {
            playerInput = Singleton.instance.PlayerInput;
        }
    }
    private void SetCameraTagToPlayer()
    {
        // Set the tag of the cockpit camera to "Player" for identification
        if (cockpitCamera.tag != "Player")
        {
            cockpitCamera.tag = "Player";
        }
        else if(externalCamera.tag != "Player")
        {
            externalCamera.tag = "Player";
        }
    }
   
   private void Update()
    {
        // Check for user input to switch between camera views
        if (playerInput.SwitchCamera())
        {
            SwitchCamera();
        }
    }
    private void SwitchCamera()
    {
        // Toggle the active state of the cockpit and external cameras
        bool isCockpitActive = cockpitCamera.activeSelf;
        cockpitCamera.SetActive(!isCockpitActive);
        externalCamera.SetActive(isCockpitActive);
        externalUI.SetActive(isCockpitActive);
        cockpitUI.SetActive(!isCockpitActive);
        SetCameraTagToPlayer();
    }
}
