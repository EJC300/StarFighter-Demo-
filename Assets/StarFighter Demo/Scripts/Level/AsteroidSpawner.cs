using UnityEngine;
namespace Level
{
    public class AsteroidSpawner : MonoBehaviour
    {
        //Grid SizeX
        public float gridX;
        //Grid SizeY
        public float gridY;
        //Grid SizeZ
        public float gridZ;

        //Grid Spacing to offset position of world object
        public float spacing;

        [SerializeField] private GameObject asteroidPrefab;


        public void Generate()
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
                        GameObject instance = Instantiate(asteroidPrefab, position, Quaternion.identity);
                        AsteroidObject asteroid = instance.GetComponent<AsteroidObject>();
                        asteroid.PlaceSelf(position.x, position.y, position.z, spacing);
                    }
                }
            }
        }
    }
}
