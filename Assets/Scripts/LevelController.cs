using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public GameObject WinningScreen;
    public TMP_Text WinningText;
    public GameObject RestartButton;
    public GameObject NextRoundButton;
    public List<PlayerCardInfo> PlayerCards;

    public GameObject NextTurnScreen;
    public TMP_Text NextTurnText;
    public TMP_Text RoundInfoText;
    public int CurrentPlayersTurn;

    public GameObject PauseScreen;

    private float scoreUpdateDelay;
    private bool roundOver;
    public bool PlayerCanClick;
    public List<PlayerSoulsAndID> PlayersSouls;
    public List<int> PlayersScores;
    public List<int> PlayersSoulGroups;
    public int CurrentRound;
    public int NumberOfRoundsTotal;

    public static LevelController s;
    // Start is called before the first frame update

    void Awake()
    {
        s = this;
        PlayersSouls = new List<PlayerSoulsAndID> { };
        CurrentRound = PlayerPrefs.GetInt("CurrentRound");
        NumberOfRoundsTotal = PlayerPrefs.GetInt("NumberOfRoundsTotal");
    }
    void Start()
    {
        PlayerCanClick = true;
        PlayersClicks = PlayersStartingClicks;
        spawnOffset.x = SpawnSpace.localScale.x * .5f;
        spawnOffset.y = SpawnSpace.localScale.y * .5f;
        PlayerCards[CurrentPlayersTurn].CardAnimator.Play("PlayersTurnStart");
        PlayerCards[CurrentPlayersTurn].ClicksLeft.text = PlayersClicks.ToString();

        for (int i = 0; i < PlayersSouls[CurrentPlayersTurn].souls.Count; i++)
        {
            PlayersSouls[CurrentPlayersTurn].souls[i].GetComponent<PlayersSouls>().TurnOnPS();
        }

        //for (int i = 0; i < PlayerCards.Count - 1; i++)
        //{

        //}

    }

    // Update is called once per frame
    void Update()
    {

        scoreUpdateDelay -= Time.deltaTime;
        if (scoreUpdateDelay < 0)
        {
            scoreUpdateDelay = .4f;
            StartCoroutine(ScoreUpdate());
        }
        
        if (DemonsInPlay < TargetNumberOfDemons)
        {
            Instantiate(Demons[Random.Range(0, Demons.Count)], SpawnSpace.position +
                new Vector3(Random.Range(spawnOffset.x * -1, spawnOffset.x),
                Random.Range(spawnOffset.y * -1, spawnOffset.y), 0), SpawnSpace.rotation);

            DemonsInPlay += 1;
        }
        //  Time for the next Players Turn
        if (PlayersClicks < 1)
        {
            PlayerCards[CurrentPlayersTurn].CardAnimator.Play("PlayersTurnEnd");

            for (int i = 0; i < PlayersSouls[CurrentPlayersTurn].souls.Count; i++)
            {
                PlayersSouls[CurrentPlayersTurn].souls[i].GetComponent<PlayersSouls>().TurnOffPS();
            }
            if (CurrentPlayersTurn + 2 > PlayerCards.Count)
            {
                CurrentPlayersTurn = 0;
            }
            else
            {
                CurrentPlayersTurn += 1;
            }
            if (roundOver == false)
            {
                StartCoroutine(TurnOver(CurrentPlayersTurn));

                for (int i = 0; i < PlayersSouls[CurrentPlayersTurn].souls.Count; i++)
                {
                    PlayersSouls[CurrentPlayersTurn].souls[i].GetComponent<PlayersSouls>().TurnOnPS();
                }

                PlayersClicks = PlayersStartingClicks;
                PlayerCards[CurrentPlayersTurn].ClicksLeft.text = PlayersClicks.ToString();
            }
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
    public void RemakeSoulGroups(int PlayerID)
    {
        for (int i = 0; i < PlayersSouls[PlayerID].souls.Count; i++)
        {
            PlayersSouls[PlayerID].souls[i].GetComponent<PlayersSouls>().MakeGroupID();
        }

    }
    public IEnumerator ScoreUpdate()
    {

        for (int i = 0; i < PlayersScores.Count; i++)
        {
            PlayersScores[i] = 0;
        }
        for (int i = 0; i < PlayersSouls.Count; i++)
        {
            for (int j = 0; j < PlayersSouls[i].souls.Count; j++)
            {
                PlayersSoulGroups[i] = 0;
                PlayersSouls[i].souls[j].GetComponent<PlayersSouls>().GroupID = -1;
                PlayersSouls[i].souls[j].GetComponent<PlayersSouls>().IsConnected = false;
                PlayersSouls[i].souls[j].GetComponent<PlayersSouls>().HasMadeGroup = false;
                //if (PlayersSouls[i].souls[j].TryGetComponent<PlayersSouls>(out PlayersSouls playersSouls))
                //{
                //    playersSouls.GroupID = -1;
                //}
            }
        }
        //for each player
        for (int i = 0; i < PlayersSouls.Count; i++)
        {
            //for each of that players souls
            for (int j = 0; j < PlayersSouls[i].souls.Count; j++)
            {
                Collider2D[] thisSoulCheck = Physics2D.OverlapCircleAll(
                    PlayersSouls[i].souls[j].transform.position, .6f);

                PlayersSouls soulToCheck = PlayersSouls[i].souls[j].GetComponent<PlayersSouls>();

                for (int k = 0; k < thisSoulCheck.Length; k++)
                {
                    if (thisSoulCheck[k].gameObject == PlayersSouls[i].souls[j].gameObject)
                    {
                        continue;
                    }
                    else
                    {
                        PlayersSouls thisSoul = thisSoulCheck[k].GetComponent<PlayersSouls>();

                        //is this a soul?
                        if (thisSoul != null)
                        {
                            //do we belong to the same player?
                            if (thisSoul.PlayerID == soulToCheck.PlayerID)
                            {
                                if(soulToCheck.IsConnected == false)
                                {
                                    PlayersScores[i] += 1;
                                    soulToCheck.IsConnected = true;
                                }
                                //Do I belong to a group?
                                if (soulToCheck.GroupID < 0)
                                {
                                    PlayersSoulGroups[i] = 0;
                                    RemakeSoulGroups(i);
                                    ////does the soul I am checking belong to a group?
                                    //if (thisSoul.GroupID >= 0)
                                    //{
                                    //    soulToCheck.GroupID = thisSoul.GroupID;
                                    //}
                                    ////New soul group is needed
                                    //else if(soulToCheck.HasMadeGroup == false)
                                    //{
                                    //    PlayersSoulGroups[i] += 1;
                                    //    soulToCheck.GroupID = PlayersSoulGroups[i];
                                    //    soulToCheck.HasMadeGroup = true;
                                }

                            }
                                ////Do we belong to the same group?
                                //else if(soulToCheck.GroupID == thisSoul.GroupID)
                                //{
                                //    continue;
                                //}

                                ////Do we both belong to a group?
                                //else if (thisSoul.GroupID >= 0)
                                //{
                                //    PlayersSoulGroups[i] = 0;
                                //    RemakeSoulGroups(i);
                                //}
                                ////you can be a part of my group :D
                                //else
                                //{
                                //    thisSoul.GroupID = soulToCheck.GroupID;
                                //}

                            //}
                        }

                    }

                }

            }
        }

        for (int i = 0; i < PlayerCards.Count; i++)
        {
            PlayerCards[i].PlayerScore.value = PlayersScores[i] - PlayersSoulGroups[i];
            if (PlayerCards[i].PlayerScore.value == PlayerCards[i].PlayerScore.maxValue)
            {
                roundOver = true;
                SetRoundWinner(i);
                if (CurrentRound < NumberOfRoundsTotal)
                    RoundWinnerReveal(i);
                else
                    GrandWinnerReveal();
                CurrentRound++;
                PlayerPrefs.SetInt("CurrentRound", CurrentRound);
                WinningScreen.SetActive(true);
                PlayerCanClick = false;
                Time.timeScale = 0;
            }
        }
        yield return null;
    }

    private void SetRoundWinner(int playerIndex)
    {
        int playerWinCounter = int.Parse(PlayerCards[playerIndex].PlayerWinCounter.text);
        playerWinCounter++;
        PlayerPrefs.SetInt($"Player{playerIndex + 1}WinnerCount", playerWinCounter);
        PlayerCards[playerIndex].SetPlayerWinCounter(playerWinCounter);
    }

    private void RoundWinnerReveal(int palyerIndex)
    {
        WinningText.text = $"{PlayerCards[palyerIndex].PlayerName.text} is the WINNER of Round {CurrentRound}!";
        RestartButton.SetActive(false);
        NextRoundButton.SetActive(true);
    }

    private void GrandWinnerReveal()
    {
        int WinningPlayerIndex = 0;
        int TopWinningNumber = int.Parse(PlayerCards[WinningPlayerIndex].PlayerWinCounter.text);
        for (int j = 0; j < PlayerCards.Count; j++)
        {
            int NextWinningNumber = int.Parse(PlayerCards[j].PlayerWinCounter.text);
            if (NextWinningNumber >= TopWinningNumber)
            {
                WinningPlayerIndex = j;
                TopWinningNumber = NextWinningNumber;
            }
        }
        WinningText.text = $"{PlayerCards[WinningPlayerIndex].PlayerName.text} is the GRAND WINNER!";
        RestartButton.SetActive(true);
        NextRoundButton.SetActive(false);
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("CurrentRound", 1);
        for (int i = 0; i < PlayerCards.Count; i++)
        {
            PlayerPrefs.SetInt($"Player{i + 1}WinnerCount", 0);
            PlayerCards[i].SetPlayerWinCounter(0);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("DemoScene_1");
    }
    public void LoadNextRound()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("DemoScene_1");
    }

    public void PauseGame()
    {
        PlayerCanClick = false;
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    public void ContinueGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
        PlayerCanClick = true;
    }

    public void LoadSetupMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SetupMenu");
    }
    public IEnumerator TurnOver(int PlayerID)
    {
        PlayerCanClick = false;
        Time.timeScale = 5;
        yield return new WaitForSeconds(10);
        RoundInfoText.text = $"Round {CurrentRound} of {NumberOfRoundsTotal}";
        NextTurnText.text = $"{PlayerCards[PlayerID].PlayerName.text}'s turn.";
        NextTurnScreen.SetActive(true);

        if (roundOver == false)
        {
            Time.timeScale = 0;
        }

    }
    public void NextTurnStart()
    {
        NextTurnScreen.SetActive(false);
        Time.timeScale = 1;
        PlayerCanClick = true;
        PlayerCards[CurrentPlayersTurn].CardAnimator.Play("PlayersTurnStart");
    }
}
