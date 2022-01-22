using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    public int DemonsInPlay;
    public int TargetNumberOfDemons;
    public List<GameObject> Demons;
    public Transform SpawnSpace;
    private Vector2 spawnOffset;
    public Animator ClickAnimator;
    public int PlayersClicks;
    public int PlayersStartingClicks;
    //public bool needsToSpawn = true;
    public TMP_Text PlayerClicksUI;

    public static LevelController s;
    // Start is called before the first frame update
    void Start()
    {
        PlayersClicks = PlayersStartingClicks;
        spawnOffset.x = SpawnSpace.localScale.x * .5f;
        spawnOffset.y = SpawnSpace.localScale.y * .5f;
        s = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (DemonsInPlay < TargetNumberOfDemons)
        {
            Instantiate(Demons[Random.Range(0, Demons.Count)], SpawnSpace.position +
                new Vector3(Random.Range(spawnOffset.x * -1, spawnOffset.x),
                Random.Range(spawnOffset.y * -1, spawnOffset.y), 0), SpawnSpace.rotation);

            DemonsInPlay += 1;
        }
        if (PlayersClicks < 1)
        {
            PlayersClicks = PlayersStartingClicks;
            PlayerClicksUI.text = PlayersClicks.ToString();
            //needsToSpawn = false;
        }
        //else if (needsToSpawn == false && PlayersClicks < 1)
        //{
        //    needsToSpawn = true;
        //}

            
    }
    public void DemonDestroyed()
    {
        DemonsInPlay -= 1;
    }
}
