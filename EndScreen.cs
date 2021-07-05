using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // sahne kütüphanesi

public class EndScreen : MonoBehaviour
{

    bool gamestop = false;

    public void bntnew() 
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Scenesone");
    }

    public void bntend()
    {
        Application.Quit();
    }
    public void bntpause()
    {
        
            gamestop = !gamestop;

            if (gamestop == true)
            {
                Time.timeScale = 0.0f;
                //voice.SetActive(false);
            }
            if (gamestop == false)
            {
                Time.timeScale = 1.0f;
                //  gameaudio.SetActive(true);
            }
        
    }
     

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
         
    }
}
