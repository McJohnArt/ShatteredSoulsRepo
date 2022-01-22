using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetUp : MonoBehaviour
{
    public int NumberOfPlayers;
    public List<Transform> SoulSpawnLocations;
    public List<GameObject> PlayerSouls;
    public int WhichSoulToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SoulSpawnLocations.Count; i++)
        {
            Instantiate(PlayerSouls[WhichSoulToSpawn], SoulSpawnLocations[i].transform.position, 
                SoulSpawnLocations[i].transform.rotation);

            WhichSoulToSpawn += 1;

            if (WhichSoulToSpawn >= NumberOfPlayers)
                WhichSoulToSpawn = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
