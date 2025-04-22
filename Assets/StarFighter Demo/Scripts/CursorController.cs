using UnityEngine;

public class CursorController : MonoBehaviour
{

    [SerializeField] private float smoothingFactor = 20f;
    private PlayerInput playerInput
    {
        get { return Singleton.instance.PlayerInput; }
    }

  
   


    private void Update()
    {
       
      //  Vector3 cursorScreenPosition = new Vector3(Mathf.Clamp(playerInput.MousePositionInWorldWithJoystick(1).x,0,Screen.width),
        //    Mathf.Clamp(playerInput.MousePositionInWorldWithJoystick(1).y,0, Screen.height),Camera.main.farClipPlane);
        Vector3 cursorScreenPosition = playerInput.MousePositionInWorldWithJoystick(1);
        transform.position = Vector3.Lerp(transform.position, cursorScreenPosition - transform.position, Time.deltaTime * smoothingFactor);

     
    }
}
