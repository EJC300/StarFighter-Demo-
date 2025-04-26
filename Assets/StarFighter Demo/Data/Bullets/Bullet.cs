using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Objects/Bullet")]
public class Bullet : ScriptableObject
{
    /// Bullet properties
    /// Speed of the bullet
    [SerializeField] private float speed = 10f;
    /// Damage dealt by the bullet
    [SerializeField] private float damage = 10f;

    ///Lifetime of the bullet
    [SerializeField] private float lifetime = 5f;

    /// Model of the Bullet
    [SerializeField] private GameObject bulletModel;

    [SerializeField] private GameObject explosionPrefab;
    public GameObject BulletModel => bulletModel;
    public GameObject ExplosionPrefab => explosionPrefab;
    public float Speed => speed;

    public float Damage => damage;

    public float Lifetime => lifetime;


    //Explosion prefab
}
