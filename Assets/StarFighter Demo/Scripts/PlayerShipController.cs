using UnityEngine;
using SpaceShip;
using System.Diagnostics.Contracts;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]

public class PlayerShipController : MonoBehaviour
{
    private PlayerInput playerInput
    {
        get { return Singleton.instance.PlayerInput; }
    }
    private Rigidbody rb
    {
        get { return GetComponent<Rigidbody>(); }
    }

    [SerializeField] private Ship ship;
  

  
    private void Bankship()
    {
        // Get the Y input from the player
        float yawInput = playerInput.MouseYaw();

        // Calculate the target banking angle based on the input
        float targetBankAngle = -yawInput * 15; // Adjust the multiplier to control the banking sensitivity
        Quaternion targetRotation = Quaternion.Euler(transform.GetChild(0).transform.localEulerAngles.x, transform.GetChild(0).transform.localEulerAngles.y, targetBankAngle);
        float clampedAngle = Mathf.Clamp(targetBankAngle, -15, 15); // Clamp the angle to a range of -15 to 15 degrees
        targetRotation = Quaternion.Euler(transform.GetChild(0).transform.localEulerAngles.x, transform.GetChild(0).transform.localEulerAngles.y, clampedAngle);

        transform.GetChild(0).localRotation= Quaternion.Slerp(transform.GetChild(0).localRotation, targetRotation, Time.deltaTime * ship.rotationSpeed * 0.5f);
    }
    private void ApplyPlayerRotation()
    {
        // Get the screen center
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // Get the cursor position in screen space
        Vector3 cursorScreenPosition = playerInput.MousePosition();

        // Calculate the offset of the cursor from the screen center
        Vector3 cursorOffset = cursorScreenPosition - screenCenter;
       float speedFactor = Utilities.Remap(rb.linearVelocity.magnitude,ship.maxSpeed,0,0.1f,0.5f,true);
        // Normalize the offset to get a value between -1 and 1
        float normalizedX = cursorOffset.x / (Screen.width / 2f);  // Horizontal offset
        float normalizedY = cursorOffset.y / (Screen.height / 2f); // Vertical offset

        // Calculate torque based on the normalized offset
        float pitchTorque = -normalizedY * ship.rotationSpeed; // Negative to pitch up when cursor is above
        float yawTorque = normalizedX * ship.rotationSpeed;    // Positive to yaw toward the cursor
        float rollTorque = playerInput.RollAxis() * ship.rotationSpeed * 5; // Roll input from the player

        // Apply torque to the ship
        float rollInput = playerInput.RollAxis();

        transform.Rotate(pitchTorque * speedFactor * Time.deltaTime, yawTorque * speedFactor * Time.deltaTime, rollTorque * speedFactor * Time.deltaTime);

    }

    void ApplyThrust()
    {
      float inputForward = ship.thrustForce * playerInput.ThrustAxis();
      float inputAfterBurner = ship.AfterBurnerForce * Time.deltaTime * playerInput.AfterBurnerAxis();
      rb.AddRelativeForce(Vector3.forward * (inputForward + inputAfterBurner ));
    }
    
    private void FixedUpdate()
    {
        ApplyThrust();
        ApplyPlayerRotation();
        // Align the ship's forward direction with the cursor direction
       // ApplyTorqueTowardsCursor(cursorDirection);

        // Apply thrust in the forward direction
    
       // hudController.BoreSightMarker(spaceShipController.Thrusters);
      //  hudController.UpdateSpeedometer(spaceShipController.Thrusters.totalLinearVelocityInMetersPerSecond);
        //hudController.UpdateEnergyBar(spaceShipController.GetCurrentEnergy(), spaceShipController.MaxEnergy);

    }
}
