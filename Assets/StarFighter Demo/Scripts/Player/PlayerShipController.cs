using UnityEngine;
using SpaceShip;
using StarterTemplates;
using System.Diagnostics.Contracts;
using UnityEngine.UI;
using Utilties;
using Player;
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
    private SpaceShipController spaceShipController
    {
        get { return GetComponent<SpaceShipController>(); }
    }

    [SerializeField] private Ship ship;
  

  
  
    private void ApplyPlayerRotation()
    {
        // Get the screen center
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // Get the cursor position in screen space
        Vector3 cursorScreenPosition = playerInput.MousePosition();

        // Calculate the offset of the cursor from the screen center
        Vector3 cursorOffset = cursorScreenPosition - screenCenter;
        float speedFactor = Utilities.Remap(rb.linearVelocity.magnitude,ship.maxSpeed,0,0.3f,0.5f,true);
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
      float inputForward = playerInput.ThrustForwardAxis() * ship.thrustForce ;
      float inputUp = playerInput.ThrustAscendAxis() * ship.maneuveringThrustForce;
      float inputLeft = playerInput.ThrustStrafeAxis() * ship.maneuveringThrustForce;
      float inputAfterBurner = playerInput.ThrustForwardAxis() * ship.AfterBurnerForce * Time.deltaTime * playerInput.AfterBurnerAxis();
      spaceShipController.Thrust(inputForward, transform.forward);
      spaceShipController.Thrust(inputUp, transform.up);
      spaceShipController.Thrust(inputLeft, transform.right);
      spaceShipController.ApplyAfterBurner(inputAfterBurner);


      
    }
    
    private void FixedUpdate()
    {
        ApplyThrust();
        ApplyPlayerRotation();
      

    }
}
