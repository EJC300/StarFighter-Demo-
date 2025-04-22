using UnityEngine;
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

    [SerializeField] private Transform CursorObject;
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
        // Calculate the direction to the cursor object
        Vector3 cursorDirection = (CursorObject.position - transform.position).normalized;

        // Get the ship's local axes
        Vector3 shipForward = transform.forward;
        Vector3 shipRight = transform.right;
        Vector3 shipUp = transform.up;

        // Calculate dot products to determine alignment
        float forwardDot = Vector3.Dot(cursorDirection, shipForward); // Alignment with forward
        float rightDot = Vector3.Dot(cursorDirection, shipRight);     // Alignment with right
        float upDot = Vector3.Dot(cursorDirection, shipUp);           // Alignment with up

        // Calculate torque based on cursor position
        float pitchTorque = -upDot * Mathf.Abs(upDot); // Negative to pitch up when cursor is above
        float yawTorque = rightDot * Mathf.Abs(rightDot); // Positive to yaw toward the cursor
        float rollTorque = playerInput.RollAxis(); // Roll input from the player

        // Apply torque to the ship
        spaceShipController.Pitch(pitchTorque);
        spaceShipController.Yaw(yawTorque);
        spaceShipController.Roll(rollTorque);
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
        // Calculate the direction to the cursor object
        Vector3 cursorDirection = (CursorObject.position - transform.position).normalized;

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
    /*
    private void ApplyTorqueTowardsCursor(Vector3 targetDirection)
    {
    
        // Calculate the rotation needed to align with the target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion currentRotation = transform.rotation;

        // Calculate the difference in rotation
        Quaternion rotationDifference = targetRotation * Quaternion.Inverse(currentRotation);
 
    
        // Apply damping based on angular velocity
        // Convert the rotation difference to an angular velocity
        Vector3 angularVelocity = new Vector3(
            Mathf.DeltaAngle(0, rotationDifference.eulerAngles.x),
            Mathf.DeltaAngle(0, rotationDifference.eulerAngles.y),
            Mathf.DeltaAngle(0, rotationDifference.eulerAngles.z)
        );
        float dampingFactor = 0.5f; // Adjust this value to control damping strength
        angularVelocity -= spaceShipController.GetAngularVelocity() * dampingFactor;
        // Apply torque to align the ship
        spaceShipController.Pitch(angularVelocity.x);
        spaceShipController.Yaw(angularVelocity.y);
       // spaceShipController.PreventGimbalLockPhysics();
    }
}
    */
