using UnityEngine;
using StarterTemplates;
public class CursorController : MonoBehaviour
{

    [SerializeField] private float smoothingFactor = 20f;
    private PlayerInput playerInput
    {
        get { return Singleton.instance.PlayerInput; }
    }

    [SerializeField] private Transform player;

    //Clamp cursor object to screen bounds
    private void ClampToScreenBounds()
    {
        if (player != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
            screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 150));
        
            transform.position = worldPoint;
        }

    }

    private void Update()
    {

        //  Vector3 cursorScreenPosition = new Vector3(Mathf.Clamp(playerInput.MousePositionInWorldWithJoystick(1).x,0,Screen.width),
        //    Mathf.Clamp(playerInput.MousePositionInWorldWithJoystick(1).y,0, Screen.height),Camera.main.farClipPlane);
        Vector3 cursorScreenPosition = playerInput.MouseJoystick();
        Vector3 offset = cursorScreenPosition;
        transform.position =  offset;
  

    }
}
