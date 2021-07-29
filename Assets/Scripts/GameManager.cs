using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIManager uI = FindObjectOfType<UIManager>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                //Debug.Log("Unpausing Game");
                Time.timeScale = 1f;
                isPaused = false;
                uI.SetPauseUI(isPaused);
            }
            else
            {
                //Debug.Log("Pausing Game");
                Time.timeScale = 0f;
                isPaused = true;
                uI.SetPauseUI(isPaused);
            }
        }
    }
}
