using UnityEngine;
namespace DamageSystem
{
    public class DeathObject : MonoBehaviour
    {
        [SerializeField] private float destroyTime;
        private void Start()
        {
            Destroy(gameObject,destroyTime);
        }
    }
}
