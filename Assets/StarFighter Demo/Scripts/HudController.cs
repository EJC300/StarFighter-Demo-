using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using SpaceShip;
public class HudController : MonoBehaviour
{
    // HUD controller for the spaceship
    // This script manages the HUD elements such as energy bar and speedometer
    [SerializeField] private Slider energyBar;

    [SerializeField] private Text speedText;

    [SerializeField] private Text speedUnitText;

    [SerializeField] private Text energyText;

    [SerializeField] private Transform boreSight;


    //Later use an event system to update the HUD
    // Use an event to update the energy bar
    // Use an event to update the speedometer
    // Use an event to update the bore sight marker

    public event Action<int, int> OnEnergyBarUpdate;
    public event Action<Vector3> OnSpeedometerUpdate;
    public event Action<SpaceShipThrusters> OnBoreSightMarkerUpdate;
    public SpaceShipController playerShipController
    {
        get { return GetComponent<SpaceShipController>(); }
    }
    // Subscribe to the events in the Start method

    // Use an event to update the energy bar
    // Use an event to update the speedometer


    // Use an event to update the bore sight marker

  

    //Use an event to update the HUD


    public void UpdateEnergyBar(float currentEnergy, float maxEnergy)
    {
        // Update the energy bar and text
        energyBar.value = currentEnergy / maxEnergy;
      //  energyText.text = $"{currentEnergy}/{maxEnergy}";
    }


    public void UpdateSpeedometer(Vector3 speed)
    {
        // Update the speedometer text
        float speedInKmh = speed.magnitude * 3.6f; // Convert to km/h
        speedText.text = $"{speedInKmh:F1}"; // Format to one decimal place
        speedUnitText.text = "km/h"; // Set the unit text
    }

    public void BoreSightMarker(SpaceShipThrusters spaceShipThrusters)
    {
        // Update the bore sight marker
        // This method can be used to update the position of the bore sight marker
        // based on the ship's orientation and target position

        // Example: Align the bore sight marker with the ship's forward direction
        // and set its position to a point in front of the ship
        // Adjust the position of the bore sight marker based on speed of bullet
        Vector3 forwardDirection = (boreSight.forward*( 0 + 100) * spaceShipThrusters.totalLinearVelocityInMetersPerSecond.y);
        Vector3 targetPosition =  forwardDirection * 100f; // Adjust the distance as needed

        ///boreSight.position = targetPosition;
    }

    private void Update()
    {
        // Update the HUD elements based on the ship's state
        SpaceShipThrusters spaceShipThrusters = GetComponent<SpaceShipThrusters>();
        UpdateEnergyBar(playerShipController.GetCurrentEnergy(),playerShipController.MaxEnergy);
    }
}
