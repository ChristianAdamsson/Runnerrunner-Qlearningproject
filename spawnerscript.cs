using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerscript : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMin;
    public float spawnMax;
    public float spawnMin2;
    public float spawnMax2;
    public float morespawntime;
	// Use this for initialization
	void Start () {
        Spawn();

	}
	
    void Spawn()
    {
        Instantiate(obj[Random.Range (0, obj.GetLength(0))], transform.position, Quaternion.identity);
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
