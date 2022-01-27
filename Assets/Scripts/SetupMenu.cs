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
    public int NumberOfPlayersShowing;
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
        PlayerPrefs.SetString("Player2Name", Player2Name.text);
        PlayerPrefs.SetString("Player3Name", Player3Name.text);
        PlayerPrefs.SetString("Player4Name", Player4Name.text);

        SceneManager.LoadScene("DemoScene_1");
    }
}