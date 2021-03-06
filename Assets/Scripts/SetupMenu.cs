using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SetupMenu : MonoBehaviour
{
    public Slider NumberOfPlayersBar;
    public List<GameObject> PlayerNameCards;
    public TMP_InputField Player1Name;
    public TMP_InputField Player2Name;
    public TMP_InputField Player3Name;
    public TMP_InputField Player4Name;
    public TMP_InputField Player5Name;
    public TMP_InputField Player6Name;
    public TMP_InputField Player7Name;
    public int NumberOfPlayersShowing;
    public Slider NumberOfRoundsSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfPlayersShowing < NumberOfPlayersBar.value)
        {
            PlayerNameCards[NumberOfPlayersShowing].SetActive(true);
            NumberOfPlayersShowing += 1;
        }
        
        if (NumberOfPlayersBar.value < NumberOfPlayersShowing)
        {
            NumberOfPlayersShowing -= 1;
            PlayerNameCards[NumberOfPlayersShowing].SetActive(false);
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("NumberOfPlayers", (int)NumberOfPlayersBar.value);
        PlayerPrefs.SetString("Player1Name", Player1Name.text);
        PlayerPrefs.SetInt("Player1WinnerCount", 0);
        PlayerPrefs.SetString("Player2Name", Player2Name.text);
        PlayerPrefs.SetInt("Player2WinnerCount", 0);
        PlayerPrefs.SetString("Player3Name", Player3Name.text);
        PlayerPrefs.SetInt("Player3WinnerCount", 0);
        PlayerPrefs.SetString("Player4Name", Player4Name.text);
        PlayerPrefs.SetInt("Player4WinnerCount", 0);
        PlayerPrefs.SetString("Player5Name", Player5Name.text);
        PlayerPrefs.SetInt("Player5WinnerCount", 0);
        PlayerPrefs.SetString("Player6Name", Player6Name.text);
        PlayerPrefs.SetInt("Player6WinnerCount", 0);
        PlayerPrefs.SetString("Player7Name", Player7Name.text);
        PlayerPrefs.SetInt("Player7WinnerCount", 0);
        PlayerPrefs.SetInt("CurrentRound", 1);
        PlayerPrefs.SetInt("NumberOfRoundsTotal", (int)NumberOfRoundsSlider.value);

        SceneManager.LoadScene("DemoScene_1");
    }
}
