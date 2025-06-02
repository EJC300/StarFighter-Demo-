using UnityEngine;
namespace DamageSystem
{
    public class Health : MonoBehaviour,IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private GameObject deathPrefab;
        private float currentHealth;
    
        public float CurrentHealth
        {
            get { return currentHealth; }
        }
        public float MaxHealth
        {
            get { return maxHealth; }
        }

        public void LowerHealth(float amountToLower)
        {
            currentHealth -= amountToLower;
        }
        void Start()
        {
            currentHealth = maxHealth;
        }
        public void SetDamage(float damage)
        {
            LowerHealth(damage);
           
        }
        void OnDestroy()
        {
        }
        void Update()
        {
           currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
           if(currentHealth < 1)
            {

                Instantiate(deathPrefab,transform.position,Quaternion.identity);
                Destroy(gameObject,0.5f);
                
             
            }
        }

    }
}