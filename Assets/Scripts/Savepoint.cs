using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Savepoint : MonoBehaviour
{
    private Transform trans;

    private bool besucht = false;
    //private bool zuletztBesucht;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    void newSave()
    {
        if (!besucht)
        {
            FindObjectOfType<GameManager>().setSave(FindObjectOfType<GameManager>().getActiveScene(), trans.position.x, trans.position.y);
            besucht = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.tag == "Player")
        {
            newSave();
            Debug.Log("aftertrigger =  " + PlayerPrefs.GetString("scene") + "__" + PlayerPrefs.GetFloat("posX") + "__" + PlayerPrefs.GetFloat("posY"));
        }

    }
}
