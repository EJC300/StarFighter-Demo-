using UnityEngine;
using SpaceShip;
[RequireComponent(typeof(SpaceShipController),typeof(HudController))]

public class PlayerShipController : MonoBehaviour
{
    private HudController hudController
    {
        get { return GetComponent<HudController>(); }
    }
    private PlayerInput playerInput
    {
        get { return Singleton.instance.PlayerInput; }
    }
    public SpaceShipController spaceShipController
    {
        get { return GetComponent<SpaceShipController>(); }
    }

    
    private void ApplyThrustToShip(Vector3 direction)
    {

        spaceShipController.Thrust(direction.z * playerInput.ThrustAxis());
    }

    private void ApplyAfterBurnerToShip(Vector3 direction)
    {
        spaceShipController.ApplyAfterBurner(direction.z * playerInput.AfterBurnerAxis());
    }

    private void AlignShipToCursor()
    {
        // Get the screen center
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // Get the cursor position in screen space
        Vector3 cursorScreenPosition = playerInput.MousePosition();

        // Calculate the offset of the cursor from the screen center
        Vector3 cursorOffset = cursorScreenPosition - screenCenter;

        // Normalize the offset to get a value between -1 and 1
        float normalizedX = cursorOffset.x / (Screen.width / 2f);  // Horizontal offset
        float normalizedY = cursorOffset.y / (Screen.height / 2f); // Vertical offset

        // Calculate torque based on the normalized offset
        float pitchTorque = -normalizedY; // Negative to pitch up when cursor is above
        float yawTorque = normalizedX;   // Positive to yaw toward the cursor
        float rollTorque = playerInput.RollAxis(); // Roll input from the player

        // Apply torque to the ship
        spaceShipController.Pitch(pitchTorque * spaceShipController.Thrusters.RotationSpeed);
        spaceShipController.Yaw(yawTorque * spaceShipController.Thrusters.RotationSpeed);
        spaceShipController.Roll(rollTorque * spaceShipController.Thrusters.RotationSpeed);
    }
    private void Roll()
    {
        float rollInput = playerInput.RollAxis();
        spaceShipController.Roll(rollInput);
    }
    private void Update()
    {
      

    }
    private void FixedUpdate()
    {
       
   

        // Align the ship's forward direction with the cursor direction
       // ApplyTorqueTowardsCursor(cursorDirection);

        // Apply thrust in the forward direction
        ApplyThrustToShip(transform.forward);
        AlignShipToCursor();
        // Apply afterburner if needed
        ApplyAfterBurnerToShip(transform.forward);
        // Handle roll input
        Roll();
       // hudController.BoreSightMarker(spaceShipController.Thrusters);
      //  hudController.UpdateSpeedometer(spaceShipController.Thrusters.totalLinearVelocityInMetersPerSecond);
        hudController.UpdateEnergyBar(spaceShipController.GetCurrentEnergy(), spaceShipController.MaxEnergy);

    }
}
   
    
