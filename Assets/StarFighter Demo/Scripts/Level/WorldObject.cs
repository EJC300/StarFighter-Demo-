using UnityEngine;
using DamageSystem;
public class WorldObject : MonoBehaviour
{
    //World Objects are things such as asteroids  debris or AI Pods that can be placed in a grid randomly and spaced 
    [SerializeField] private Vector3 randomOffset;
    [SerializeField] private Vector3 randomScale;
    [SerializeField] private float randomOffsetMultiplier;
    [SerializeField] private float randomScaleMultiplier;
    [SerializeField] private GameObject worldModel;
    private Vector3  randomRotation;
    //Setup Object at start
    public void PlaceSelf(float x ,float y ,float z,float spacing)
    {
       GameObject model = Instantiate(worldModel,transform.position,Quaternion.identity);
       model.transform.parent = transform;
       randomOffset.x = Random.Range(0, spacing / 2);
       randomOffset.y = Random.Range(0,  spacing/2);
       randomOffset.z = Random.Range(0, spacing/2);
       randomRotation.z = Random.Range(0, randomOffsetMultiplier);
       randomRotation.y = Random.Range(0, randomOffsetMultiplier);
       randomRotation.x = Random.Range(0, randomOffsetMultiplier);
        randomScale = Vector3.one * Random.Range(1, randomScaleMultiplier);
        transform.position = (new Vector3(x + randomScale.x, y + randomScale.y , z + randomScale.z)  );
       transform.localScale= randomScale;
    }

    private void Update()
    {
        var rotAngle = 20;
        transform.Rotate(randomRotation * Time.deltaTime *rotAngle);

    }

}
