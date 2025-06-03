using Level;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int width = 100;
    [SerializeField] private int height = 100;
    [SerializeField] private int depth = 100;

    AsteroidSpawner asteroidSpawner;

    private void Start()
    {
        asteroidSpawner= GetComponent<AsteroidSpawner>();
        asteroidSpawner.gridX = width;
        asteroidSpawner.gridY = height;
        asteroidSpawner.gridZ = depth;
        asteroidSpawner.Generate();
    }

}
