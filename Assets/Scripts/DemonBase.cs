using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBase : MonoBehaviour
{
    public bool IsBeingDestroyed = false;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        ChainDestroy();
    }
    public void ChainDestroy()
    {
        Collider2D[] adjacentDemons = Physics2D.OverlapCircleAll(transform.position, 1.2f);
        //Collider[] adjacentDemons = Physics.OverlapSphere(transform.position, 10f);
        for (int i = 0; i < adjacentDemons.Length; i++)
        {
            if (adjacentDemons[i].tag == tag && 
                adjacentDemons[i].GetComponent<DemonBase>().IsBeingDestroyed == false)
            {
                adjacentDemons[i].GetComponent<DemonBase>().IsBeingDestroyed = true;
                adjacentDemons[i].GetComponent<DemonBase>().ChainDestroy();
            }
        }
        Instantiate(Explosion, transform.position, transform.rotation);
        LevelController.s.DemonsInPlay -= 1;
        LevelController.s.ClickAnimator.Play("ClickAnimation");
        Destroy(gameObject);
    }
}
