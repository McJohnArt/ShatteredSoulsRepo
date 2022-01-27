using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBase : MonoBehaviour
{
    public bool IsBeingDestroyed = false;
    public GameObject Explosion;
    public GameObject SelectionGlow;
    private bool triggeringExplosion;
    private Coroutine selectionGlowCoroutine;
    private bool triggeringChainExplosion;
    // Start is called before the first frame update
    void Start()
    {
        SelectionGlow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(LevelController.s.PlayerCanClick == true)
        {
            triggeringExplosion = true;
            triggeringChainExplosion = false;
            SetSelectionGlow(true);
            StartSelectionGlowCoroutine();
            //LevelController.s.PlayersClicks -= 1;
            //LevelController.s.PlayerCards[LevelController.s.CurrentPlayersTurn].ClicksLeft.text
            //    = LevelController.s.PlayersClicks.ToString();
            //ChainDestroy();
        }
    }

    private void StartSelectionGlowCoroutine()
    {
        if (selectionGlowCoroutine != null)
            StopCoroutine(selectionGlowCoroutine);
        selectionGlowCoroutine = StartCoroutine(SetSelectionGlowForChain());
    }

    private void OnMouseUp()
    {
        if (triggeringExplosion == true)
        {
            LevelController.s.PlayersClicks -= 1;
            LevelController.s.PlayerCards[LevelController.s.CurrentPlayersTurn].ClicksLeft.text
                = LevelController.s.PlayersClicks.ToString();

            if (triggeringChainExplosion)
            {
                ChainDestroy();
                triggeringChainExplosion = false;
            }
            else
            {
                SelfDestroy();
            }
        }

        triggeringExplosion = false;
    }

    public void SetSelectionGlow(bool value)
    {
        SelectionGlow.SetActive(value);
    }

    private void OnMouseExit()
    {
        triggeringExplosion = false;
        SelectionGlow.SetActive(false);
        if (selectionGlowCoroutine != null)
        {
            StopCoroutine(selectionGlowCoroutine);
        }
        if (triggeringChainExplosion)
        {
            triggeringChainExplosion = false;
            SetChainGlow(false);
        }
    }

    public void ChainDestroy()
    {
        Collider2D[] adjacentDemons = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        //Collider[] adjacentDemons = Physics.OverlapSphere(transform.position, 10f);
        for (int i = 0; i < adjacentDemons.Length; i++)
        {
            if (adjacentDemons[i].tag == tag && 
                adjacentDemons[i].GetComponent<DemonBase>().IsBeingDestroyed == false)
            {
                adjacentDemons[i].GetComponent<DemonBase>().IsBeingDestroyed = true;
                adjacentDemons[i].GetComponent<DemonBase>().ChainDestroy();
                LevelController.s.DemonsInPlay -= 1;
                //LevelController.s.ClickAnimator.Play("ClickAnimation");
            }
        }
        SelfDestroy();
    }

    private void SelfDestroy()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void SetChainGlow(bool value)
    {
        Collider2D[] adjacentDemons = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        for (int i = 0; i < adjacentDemons.Length; i++)
        {
            if (adjacentDemons[i].tag == tag &&
                adjacentDemons[i].GetComponent<DemonBase>().SelectionGlow.activeSelf != value)
            {
                adjacentDemons[i].GetComponent<DemonBase>().SetSelectionGlow(value);
                adjacentDemons[i].GetComponent<DemonBase>().SetChainGlow(value);
            }
        }
    }

    public IEnumerator SetSelectionGlowForChain()
    {
        yield return new WaitForSeconds(1);
        if (triggeringExplosion == true)
        {
            triggeringChainExplosion = true;
            SetChainGlow(true);
        }
    }
}
