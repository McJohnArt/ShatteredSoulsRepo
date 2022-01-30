using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCardInfo : MonoBehaviour
{

    public int PlayerID;
    public TMP_Text PlayerName;
    public Slider PlayerScore;
    public Animator CardAnimator;
    public TMP_Text ClicksLeft;
    public TMP_Text PlayerWinCounter;
    public Image PlayerWinImage;


    // Start is called before the first frame update
    void Start()
    {
        PlayerWinImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementPlayerWinCounter()
    {
        int value = int.Parse(PlayerWinCounter.text);
        if (value == 0)
            PlayerWinImage.gameObject.SetActive(true);
        value++;
        PlayerWinCounter.text = value.ToString();
    }
}
