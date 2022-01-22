using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{

    public float CountDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDown -= Time.deltaTime;
        if (CountDown < 0)
            Destroy(gameObject);
    }
}
