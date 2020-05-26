using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public float restartDelay = 1f;

    public void EndGame()
    {
        Debug.Log("Game Over");
        //KÖNNTE PROBLEM GEBEN
        Invoke("loadGame", restartDelay);
    }

    public void switchScene(string scene)
    {
        Debug.Log("load "+scene);
        SceneManager.LoadScene(scene);
    }
    void Restart()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool testActiveScene()
    {
        if ((SceneManager.GetActiveScene().name).Equals(PlayerPrefs.GetString("scene")))
        {
            Debug.Log("true");
            return true;
        }
        Debug.Log("false");
        return false;
    }

    public string getActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void setSave(string id, float posX, float posY)
    {
        Debug.Log("Saved");
        PlayerPrefs.SetString("scene", id);
        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        Debug.Log("afterSave =  " + PlayerPrefs.GetString("scene") + "__" + PlayerPrefs.GetFloat("posX") + "__" + PlayerPrefs.GetFloat("posY"));
    }

    public void loadGame()
    {
        if (!(PlayerPrefs.GetString("scene").Equals("")))
            FindObjectOfType<GameManager>().switchScene(PlayerPrefs.GetString("scene"));
        else
        {
            Debug.Log("Savefile dont exist");
        }
    }

}
