using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthdownspawner : MonoBehaviour
{
    public GameObject[] obj;
    public float spawnMin;
    public float spawnMax;
    public float spawnMin2;
    public float spawnMax2;
    public float morespawntime;
    public Vector3 higherspawnpos;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
       
    }


    void Spawn()
    {
        GameObject newobj = obj[Random.Range(0, obj.GetLength(0))];
        if(newobj.tag == "beercan")
        {
            Instantiate(newobj, transform.position, Quaternion.identity);
        }
        else
        {
            higherspawnpos = transform.position;
            higherspawnpos.y += 2;
            Instantiate(newobj, higherspawnpos, Quaternion.identity);
        }
        
        if (Time.timeSinceLevelLoad > morespawntime)
        {
            Invoke("Spawn", Random.Range(spawnMin2, spawnMax2));
        }
        else
        {
            Invoke("Spawn", Random.Range(spawnMin, spawnMax));
        }

    }
}
