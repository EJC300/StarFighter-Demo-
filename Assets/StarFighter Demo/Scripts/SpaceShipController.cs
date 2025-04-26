using System;
using SpaceShip;
using UnityEngine;
[RequireComponent(typeof(SpaceShipThrusters))]
public class SpaceShipController : MonoBehaviour
{

    //Apply thrust and torque to the ship

    private SpaceShipThrusters thrusters;
    public SpaceShipThrusters Thrusters
    {
        get { return thrusters; }
        set { thrusters = value; }
    }
    private int maxEnergy = 100;
    [SerializeField] private float CurrentEnergy = 100;
    private int energyPerSecond = 1;

    public int MaxEnergy
    {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }
    private void Update()
    {
      
        // BankWhileTurning();
        // Regenerate energy over time
        CurrentEnergy += (int)(energyPerSecond) * Time.deltaTime;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        Debug.Log(GetCurrentEnergy());
    }

    public void DrainEnergy(int amount)
    {
        // Drain energy from the ship
        CurrentEnergy -= amount * Time.deltaTime;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        //Debug.Log($"Current Energy: {CurrentEnergy}");
    }
    public float GetCurrentEnergy()
    {
        // Get the current energy level of the ship
        return CurrentEnergy;
    }
    public void Roll(float rollInput)
    {
        // Roll the ship using quaternions
        Vector3 rollhRotation = 0.20f * rollInput * thrusters.RotationSpeed * transform.forward;
        thrusters.ApplyTorque(rollhRotation);
    }

    public void Pitch(float pitchInput)
    {
        // Pitch the ship using quaternions
        Vector3 pitchRotation = pitchInput * thrusters.RotationSpeed * Time.deltaTime * transform.right;
        thrusters.ApplyTorque(pitchRotation);
    }

    public void Yaw(float yawInput)
    {
        // Yaw the ship using quaternions
        Vector3 yawRotation = yawInput * thrusters.RotationSpeed * Time.deltaTime * transform.up;
        thrusters.ApplyTorque(yawRotation);
    }

    public void Thrust(float thrustInput)
    {
        // Apply thrust to the ship
        Vector3 thrustDirection = transform.forward * thrustInput;
    
    
        thrusters.ApplyThrust(thrustDirection);
    }
   
    
    
    
    public void ApplyAfterBurner(float input)
    {
        // Apply afterburner force
        if(CurrentEnergy > 0)
        {
           // DrainEnergy((int)thrusters.AfterBurnerDraineRate) ;
            thrusters.ApplyAfterBurner(input);
        }
        else
        {
            return;
        }
     
    }
    public void BankWhileTurning()
    {
        // Bank the ship while turning
        float bankAmount = thrusters.totalAngularVelocityInDegreesPerSecond.y;
        float angle = Vector3.SignedAngle(thrusters.totalAngularVelocityInDegreesPerSecond, transform.forward, transform.up);
        if (Mathf.Abs(thrusters.totalAngularVelocityInDegreesPerSecond.y) > 5)
        {


            bankAmount = Mathf.Sign(thrusters.totalAngularVelocityInDegreesPerSecond.y) * -45;
        }
        else
        {
            bankAmount = 0;
        }
        
        Vector3 bankRotation = new Vector3(0,0, bankAmount);

        if(Mathf.Abs( bankRotation.z) < 15)
        {
            transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, transform.eulerAngles - bankRotation, Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0), Time.deltaTime);
        }
       
    }
    
    void Start()
    {
        thrusters = GetComponent<SpaceShipThrusters>();
    }

    internal Vector3 GetAngularVelocity()
    {
       return thrusters.totalAngularVelocityInDegreesPerSecond * Mathf.Deg2Rad;
    }
}
