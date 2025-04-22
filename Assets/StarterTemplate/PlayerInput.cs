using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
   public bool OnExitToMenu()
    {       // This method is called when the player wants to exit to the main menu.
            // It returns true if the player pressed the exit button.
        return Keyboard.current.escapeKey.wasPressedThisFrame;
    }

    public bool OnLoadFirstLevel()
    {       // This method is called when the player wants to start the game.
            // It returns true if the player pressed the start button.
        return Keyboard.current.anyKey.wasPressedThisFrame;
    }

    public float MousePitch()
    {       // This method returns the pitch input from the mouse.
            // It uses the mouse delta to calculate the pitch.
        return Mouse.current.delta.ReadValue().y;
    }

    public float MouseYaw()
    {      // This method returns the yaw input from the mouse.
           // It uses the mouse delta to calculate the yaw.
        return Mouse.current.delta.ReadValue().x;
    }

    public Vector3 VirtualMouseJoystick(float Sensitivity)
    {
        // Get the mouse delta
        Vector3 joystickInput = new Vector3();
        joystickInput.x = Mouse.current.delta.ReadValue().x;
        joystickInput.y = Mouse.current.delta.ReadValue().y;

        // Apply a dead zone to prevent sticking
        float deadZone = 0.05f; // Adjust this value as needed
        joystickInput.x = Mathf.Abs(joystickInput.x) > deadZone ? joystickInput.x : 0;
        joystickInput.y = Mathf.Abs(joystickInput.y) > deadZone ? joystickInput.y : 0;

        // Clamp the joystick input to a range of -1 to 1
        joystickInput.x = Mathf.Clamp(joystickInput.x, -1f, 1f);
        joystickInput.y = Mathf.Clamp(joystickInput.y, -1f, 1f);

        return joystickInput;
    }


    public float ThrustAxis()
    {
        // This method returns the thrust input from the joystick.
        // It uses the joystick axis to calculate the thrust input.
        float thrustInput = 0;
        if (Keyboard.current.wKey.isPressed)
        {
            thrustInput = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            thrustInput = -1;
        }
        return thrustInput;
    }
    public float AfterBurnerAxis()
    {
        // This method returns the afterburner input from the joystick.
        // It uses the joystick axis to calculate the afterburner input.
        float afterBurnerInput = 0;
        if (Keyboard.current.wKey.isPressed && Keyboard.current.leftShiftKey.isPressed)
        {
            afterBurnerInput = 1;
        }
        return afterBurnerInput;
    }
    public float RollAxis()
    {
        // This method returns the roll input from the joystick.
        // It uses the joystick axis to calculate the roll input.
        float rollInput = 0;
        if (Keyboard.current.aKey.isPressed)
        {
            rollInput = -1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            rollInput = 1;
        }
        return rollInput;
    }

    public Vector3 MousePositionInWorldWithJoystick(float Sensitivity)
    {
        // Get the virtual joystick input
        Vector3 joystickInput = VirtualMouseJoystick(Sensitivity);

        // Convert joystick input to screen space coordinates
        Vector3 screenPosition = new Vector3(
            Screen.width * 0.5f + joystickInput.x * Screen.width * 0.5f,
            Screen.height * 0.5f + joystickInput.y * Screen.height * 0.5f,
            0f // Use 0 for the z-coordinate in screen space
        );

        // Convert screen position to world position using the camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            screenPosition.x,
            screenPosition.y,
           Camera.main.farClipPlane * 0.05f// Adjust this value as needed
        ));

        return worldPosition;
    }


}
