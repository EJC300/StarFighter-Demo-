using UnityEngine;
namespace DamageSystem
{
    public class Health : MonoBehaviour, IDamageComponent
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private GameObject deathPrefab;
        private float currentHealth;
        private float deathTime = 0.3f;
        
        public void LowerHealth(float amountToLower)
        {
            currentHealth -= amountToLower;
        }

        void Update()
        {
           currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
           if(currentHealth < 1)
            {
                deathTime-= Time.deltaTime;
                if (deathTime < deathTime * 0.5f)
                {
                  // Instantiate(deathPrefab,transform.position,Quaternion.identity);

                }
                else if(deathTime < 0)
                {
                    Destroy(this.gameObject);
                    deathTime = 0;
                }
            }
        }

    }
}