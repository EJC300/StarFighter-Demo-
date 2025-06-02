
using UnityEngine;
using DamageSystem;
namespace SpaceShip
{
   
    public class SpaceShipController : MonoBehaviour
    {

        //Apply thrust and torque to the ship

        private SpaceShipThrusters thrusters;
        private Shields shield;
        private Health health;
        public SpaceShipThrusters Thrusters
        {
            get { return thrusters; }
            set { thrusters = value; }
        }
        private int maxEnergy = 100;
        [SerializeField] private float CurrentEnergy = 100;
        private int energyPerSecond = 5;

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
            if(shield.CurrentShield < shield.MaxShields)
            {
                DrainEnergy(Mathf.FloorToInt(shield.ChargeAmount));
                shield.RechargeShields();
            }
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

        public void Thrust(float thrustInput,Vector3 direction)
        {
            // Apply thrust to the ship
            Vector3 thrustDirection = direction * thrustInput;


            thrusters.ApplyThrust(thrustDirection);
        }




        public void ApplyAfterBurner(float input)
        {
            // Apply afterburner force
            if (CurrentEnergy > 0 && input > 0f)
            {
                DrainEnergy((int)thrusters.AfterBurnerDraineRate);
                Thrust(input, transform.forward);
            }
            else
            {
                return;
            }

        }
       

        void Start()
        {
            thrusters = GetComponent<SpaceShipThrusters>();
            shield = GetComponent<Shields>();
            health = GetComponent<Health>();
        }
        
        internal Vector3 GetAngularVelocity()
        {
            return thrusters.totalAngularVelocityInDegreesPerSecond * Mathf.Deg2Rad;
        }

        public void OnCollisionEnter(Collision collision)
        {
            //Test
            if (collision.collider.transform.tag == "Obstacle")
            {
                Debug.Log("Damage");
                SetDamage(100 * thrusters.totalLinearVelocityInMetersPerSecond.magnitude);
            }
        }
        public void SetDamage(float damage)
        {
     
            
            shield.DepleteShield(damage * 0.5f);
            if (shield.CurrentShield > 0.0f)
            {
                health.LowerHealth(damage);
            }
            
        }
    }
}
