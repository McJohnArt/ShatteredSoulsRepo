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

    void Awake()
    {
        PlayerWinImage.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerWinCounter(int value)
    {
        if (value > 0)
            PlayerWinImage.gameObject.SetActive(true);
        PlayerWinCounter.text = value.ToString();
    }
}
