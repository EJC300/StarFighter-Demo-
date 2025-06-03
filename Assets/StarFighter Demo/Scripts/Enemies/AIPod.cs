using System.Collections.Generic;
using UnityEngine;

public class AIPod : MonoBehaviour
{

    //3 ai types to randomize
    [SerializeField] private GameObject Scout, Fighter, Frigate;
    //Spawn only 3 random ai 
   [SerializeField] List<Bot> Bots = new();

    public void AddBot(GameObject botPrefab)
    {
        Bot bot = botPrefab.GetComponent<Bot>();
        if(!Bots.Contains(bot))
        {
            Bots.Add(bot);
        }
    }
    public void RemoveBot(GameObject botPrefab)
    {
        Bot bot = botPrefab.GetComponent<Bot>();
        if (Bots.Contains(bot))
        {
            Bots.Remove(bot);
        }
    }

    void Randomize()
    {
       AddBot(Scout);
       AddBot(Fighter);
       AddBot(Frigate);
        for (int i = 0; i < Bots.Count; i++)
        {
            for (int j = 1; j< Bots.Count; j++)
            {
                int index = Random.Range(i, Bots.Count);
                if (Bots[index] != Bots[j])
                {
                    Bots[index] = Bots[j];
                }
               
            }
        }

    }
    public void SpawnBots()
    {
        for (int i = 0; i < Bots.Count; i++)
        {
            Bots[i].hostPod = this;
            GameObject botSpawn = Instantiate(Bots[i].gameObject, transform.position, Quaternion.identity);
            
        }


    }

    
    private void Start()
    {
        Randomize();
    }

    private void Update()
    {
        
        if(Bots.Count == 0)
        {
            Destroy(this.gameObject);
            //remove self from pod manager
        }
    }
    //Track how many are still alive

    //If none are alive destroy the pod

    //On Pod creation track via the pod manager
    //IF Pod is destroyed remove from pod mananager








}
