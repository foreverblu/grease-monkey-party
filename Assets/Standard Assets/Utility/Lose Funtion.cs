using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneMangement;

public class LoseFuntion : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public void EndGame ()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
