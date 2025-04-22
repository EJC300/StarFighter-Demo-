using System;
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
    private int CurrentEnergy = 100;
    private int energyPerSecond = 1;

    public int MaxEnergy
    {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }
    private void Update()
    {
    
        // Regenerate energy over time
        CurrentEnergy += (int)(energyPerSecond * Time.deltaTime);
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
    }

    public void DrainEnergy(int amount)
    {
        // Drain energy from the ship
        CurrentEnergy -= amount;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
    }
    public int GetCurrentEnergy()
    {
        // Get the current energy level of the ship
        return CurrentEnergy;
    }
    public void Roll(float rollInput)
    {
        // Roll the ship using quaternions
        Vector3 rollhRotation = 0.20f * rollInput * thrusters.RotationSpeed * Time.deltaTime * transform.forward;
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
        Vector3 thrustDirection = new Vector3(0, 0, thrustInput);
        float dotProduct = Vector3.Dot(thrustDirection, transform.forward);
        if (dotProduct < 0)
        {
            // If the thrust direction is opposite to the forward direction, negate it by half the thrust
            thrustDirection = -thrustDirection * 0.5f;
        }
        thrusters.ApplyThrust(thrustDirection);
    }
   
    
    
    
    public void ApplyAfterBurner(float input)
    {
        // Apply afterburner force
        if(CurrentEnergy > 0)
        {
            DrainEnergy((int)thrusters.AfterBurnerDraineRate) ;
            thrusters.ApplyAfterBurner(input);
        }
        else
        {
            return;
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
