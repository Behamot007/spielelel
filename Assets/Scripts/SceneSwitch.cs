using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{

    public string scene;

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.tag == "Player")
        {
            Debug.Log("new Scene");
            FindObjectOfType<GameManager>().switchScene(scene);
        }

    }
}
