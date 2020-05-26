using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void newGame()
    {
        //SPÄTER WARNUNG EINBAUEN
        PlayerPrefs.DeleteAll();
        FindObjectOfType<GameManager>().switchScene("scene03");
    }

    public void loadGame()
    {
        FindObjectOfType<GameManager>().loadGame();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
