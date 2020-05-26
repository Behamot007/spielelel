using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public NewBehaviourScript player;

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.tag == "Player" && player.getVisible())
        {
            Debug.Log("coli");
            FindObjectOfType<GameManager>().EndGame();
        }

    }
}
