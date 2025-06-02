using UnityEngine;

public class GridCellObject : MonoBehaviour
{
    public float width, height,depth;

    GridCell gridCell;
    
    private void Start()
    {
        gridCell= new GridCell(transform.position,width,height,depth);
    }
    private void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(gridCell.spawnPoint, 0.5f);

   

    }
}
