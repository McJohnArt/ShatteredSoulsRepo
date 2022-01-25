using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSoulsAndID
{
    public int iD;
    public List<GameObject> souls;

    public PlayerSoulsAndID(int ID)
    {
        iD = ID;
        souls = new List<GameObject> { };
    }
}
