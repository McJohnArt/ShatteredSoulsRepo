using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayersSouls : MonoBehaviour
{
    public int PlayerID;
    public int GroupID;
    public bool IsConnected;
    public bool HasMadeGroup;
    public Light2D SoulsLight;
    private bool isMyPlayersTurn;
    private Coroutine toggleSoulsLightCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        isMyPlayersTurn = false;
        if (PlayerID != 1) // turn all lights except player 1's off
            SoulsLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle soul light on
        if (LevelController.s.CurrentPlayersTurn + 1 == PlayerID &&
            isMyPlayersTurn == false)
        {
            if (toggleSoulsLightCoroutine != null)
                StopCoroutine(toggleSoulsLightCoroutine);
            toggleSoulsLightCoroutine = StartCoroutine(ToggleSoulsLight(true, 0));
            isMyPlayersTurn = true;
        } // toggle soul light off
        else if (LevelController.s.CurrentPlayersTurn + 1 != PlayerID &&
            isMyPlayersTurn == true)
        {
            if (toggleSoulsLightCoroutine != null)
                StopCoroutine(toggleSoulsLightCoroutine);
            toggleSoulsLightCoroutine = StartCoroutine(ToggleSoulsLight(false, 10));
            isMyPlayersTurn = false;
        }
    }

    public IEnumerator ToggleSoulsLight(bool value, int secToWait)
    {
        yield return new WaitForSeconds(10);
        SoulsLight.gameObject.SetActive(value);
    }

    public void MakeGroupID()
    {
        //if I am not a part of a group
        if(GroupID < 0)
        {
            LevelController.s.PlayersSoulGroups[PlayerID - 1] += 1;
            GroupID = LevelController.s.PlayersSoulGroups[PlayerID - 1];

            Collider2D[] thisSoulCheck = Physics2D.OverlapCircleAll(transform.position, .6f);
            for (int i = 0; i < thisSoulCheck.Length; i++)
            {
                PlayersSouls soulToCheck = thisSoulCheck[i].GetComponent<PlayersSouls>();

                if (soulToCheck == null)
                    continue;

                if(soulToCheck.PlayerID == PlayerID)
                {
                    soulToCheck.GroupID = GroupID;
                    soulToCheck.SetGroupID(GroupID);
                }
            }
        }
    }
    public void SetGroupID(int GroupID)
    {
        Collider2D[] thisSoulCheck = Physics2D.OverlapCircleAll(transform.position, .6f);
        for (int i = 0; i < thisSoulCheck.Length; i++)
        {
            PlayersSouls soulToCheck = thisSoulCheck[i].GetComponent<PlayersSouls>();

            if (soulToCheck == null)
                continue;

            if (soulToCheck.PlayerID == PlayerID && soulToCheck.GroupID < 0)
            {
                soulToCheck.GroupID = GroupID;
                soulToCheck.SetGroupID(GroupID);
            }
        }
    }

}
