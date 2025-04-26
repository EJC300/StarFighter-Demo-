using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    //TODO : Use Bullet Scriptable Object for different types of projectiles
    [SerializeField] private Bullet bullet; // Bullet Scriptable Object

    private Rigidbody shipBody;
    private Rigidbody rb { get { return GetComponent<Rigidbody>(); } } // Rigidbody component of the projectile

    private void Start()
    {
        GameObject model = Instantiate(bullet.BulletModel, transform.position, transform.rotation); // Instantiate the bullet model
        model.transform.parent = this.transform;
    }
    public void SetupBullet(Rigidbody shipBody)
    {
        // Set the ship's Rigidbody to the projectile
        this.shipBody = shipBody;
        // Set the projectile's speed and rotation to match the ship's
        transform.rotation = shipBody.rotation;

        // rb.linearVelocity = shipBody.linearVelocity; // Set the projectile's velocity to match the ship's
        shipBody.linearVelocity = shipBody.linearVelocity;

    }

    private void Update()
    {
   
        rb.linearVelocity += (transform.forward * bullet.Speed ); // Set the linear velocity of the projectile

    }
    private void DestroyProjectile()
    {
        // Destroy the projectile after its lifetime
        Destroy(gameObject);
    }
    //TODO : Use Bullet Pooling
    private void OnEnable()
    {
        // Start the lifetime countdown when the projectile is enabled
        Invoke("DestroyProjectile", bullet.Lifetime);
    }
}
