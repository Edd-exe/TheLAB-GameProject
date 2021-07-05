using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishscript : MonoBehaviour
{
    private Play ss;
    public GameObject obje;
    public TMPro.TextMeshProUGUI scoretxt2;

    public void Play()
    {
        SceneManager.LoadScene("Scenesone");  //********
    }
    public void Menu()
    {
        SceneManager.LoadScene("Scenemenu");  //********
    }

   



    
    void Start()
    {
        // obje = GameObject.FindWithTag("Player");
        //scoretxt2.text = obje.GetComponent<Play>().score.ToString();
        //Debug.Log("++"+obje.GetComponent<Play>().score.ToString());

        //ss = obje.GetComponent<Play>();
        //ss = obje.GetComponent<Play>();
        //scoretxt2.text = ss.score.ToString();

        scoretxt2.text = "SCORE: " + PlayerPrefs.GetInt("highscore");
    }

   
    void Update()
    {
        // scoretxt.text = ss.score.ToString();
       // ss = obje.GetComponent<Play>();
        //scoretxt2.text = ss.scoretxt.ToString();
    }
}
