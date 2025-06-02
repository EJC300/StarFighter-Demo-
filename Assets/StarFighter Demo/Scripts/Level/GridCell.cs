using UnityEngine;

public class GridCell
{
    

    public Vector3 pos;

    public Vector3 spawnPoint;

 

    public float width, height,depth;

    public bool occupied;

    public void Spawn(GameObject go)
    {
        GameObject.Instantiate(go,spawnPoint,Quaternion.identity);
        occupied = true;
    }



    public GridCell(Vector3 pos, float width, float height,float depth)
    {
        this.pos = pos;
        this.width = width;
        this.height = height;
        this.depth = depth;
        spawnPoint = pos - new Vector3((1- width) * 0.5f, (1-height) * 0.5f, (depth-1) * 0.5f);
    }
}
