using UnityEngine;

public class WorldGridSpawner : MonoBehaviour
{
    //Grid SizeX
    [SerializeField] private float gridX;
    //Grid SizeY
    [SerializeField] private float gridY;
    //Grid SizeZ
    [SerializeField] private float gridZ;

    //Grid Spacing to offset position of world object
    [SerializeField] private float spacing;

    [SerializeField] private GameObject worldObject;


    private void Start()
    {
        Vector3 offset = new Vector3(
        (gridX - 1) * spacing / -2,
        (gridY - 1) * spacing / -2,
        (gridZ - 1) * spacing / -2
    );

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    // Calculate the position for each object
                    Vector3 position = new Vector3(
                        x * spacing + offset.x,
                        y * spacing + offset.y,
                        z * spacing + offset.z
                    );

                    // Instantiate and place the object
                    GameObject instance = Instantiate(worldObject, position, Quaternion.identity);
                    WorldObject gameWorldObject = instance.GetComponent<WorldObject>();
                    gameWorldObject.PlaceSelf( position.x, position.y, position.z, spacing);
                }
            }
        }
    }
}
