using UnityEngine;

namespace DamageSystem
{
    public class Shields : MonoBehaviour,IDamageable
    {
        [Range(0,1)]
        [SerializeField] private float shieldAbsortion;
        [SerializeField] private float maxShields;
        [SerializeField] private float chargeAmount;
        private float currentShield;
        public float CurrentShield { get { return currentShield; } }
        public float MaxShields { get { return maxShields; } }

        public float ChargeAmount {  get { return chargeAmount; } }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentShield = maxShields;
        }
        public void DepleteShield(float amount)
        {
            currentShield -= amount * shieldAbsortion * Time.deltaTime;
        }
       public void RechargeShields()
        {
            
               
           currentShield += chargeAmount * Time.deltaTime;
            
        }
        // Update is called once per frame
        void Update()
        {
            currentShield = Mathf.Clamp(currentShield,0, maxShields);
          
        }

        public void SetDamage(float damage)
        {
           DepleteShield(damage);
        }
    }
}
