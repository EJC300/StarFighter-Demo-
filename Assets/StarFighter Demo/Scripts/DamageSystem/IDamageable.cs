using UnityEngine;
namespace DamageSystem
{
    public interface IDamageable
    {
        //This allows damage to either a ship's shields or health as well as anything to be damaged.

        public void SetDamage(float damage, IDamageComponent damageComponent);
    }
}