using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSouls : MonoBehaviour
{
    public int PlayerID;
    public int GroupID;
    public bool IsConnected;
    public bool HasMadeGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
