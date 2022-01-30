using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetUp : MonoBehaviour
{
    public int NumberOfPlayers;
    public List<Transform> SoulSpawnLocations;
    public List<GameObject> PlayerSouls;
    public List<GameObject> PlayerCards;
    public GameObject PlayerCardHolder;
    public int WhichSoulToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        NumberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");

        for (int i = 0; i < SoulSpawnLocations.Count; i++)
        {
            GameObject thisSoul = Instantiate(PlayerSouls[WhichSoulToSpawn], 
                SoulSpawnLocations[i].transform.position, 
                SoulSpawnLocations[i].transform.rotation);

            if (LevelController.s.PlayersSouls.Count < WhichSoulToSpawn + 1)
            {
                LevelController.s.PlayersSouls.Add(new PlayerSoulsAndID(WhichSoulToSpawn));
                
            }
            LevelController.s.PlayersSouls[WhichSoulToSpawn].souls.Add(thisSoul);

            WhichSoulToSpawn += 1;
            if (WhichSoulToSpawn >= NumberOfPlayers)
                WhichSoulToSpawn = 0;
        }
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            
            GameObject playersCard = Instantiate(PlayerCards[i], PlayerCardHolder.transform);
            PlayerCardInfo playerCardInfo = playersCard.GetComponent<PlayerCardInfo>();
            playerCardInfo.PlayerName.text = PlayerPrefs.GetString($"Player{i + 1}Name");
            playerCardInfo.SetPlayerWinCounter(PlayerPrefs.GetInt($"Player{i + 1}WinnerCount"));
            LevelController.s.PlayerCards.Add(playerCardInfo);
            LevelController.s.PlayerCards[i].PlayerScore.maxValue = LevelController.s.PlayersSouls[i].souls.Count -1;
            LevelController.s.PlayersScores.Add(0);
            LevelController.s.PlayersSoulGroups.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
