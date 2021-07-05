using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    //bool sag = true;
    public Transform plane1;      
    public Transform plane2;
    //public Transform player;
    int a = 0;
    private string midJump = "n";   
    //bool yerdeMi = true;

    public Animator animationn;
    public Rigidbody rigi;


    public TMPro.TextMeshProUGUI scoretxt;
    public TMPro.TextMeshProUGUI hptxt2;
    public int score = 0;
    int hp = 3;
    

    public GameObject gameend;



    public AudioSource voice;
    public AudioClip v_siringa;
    public AudioSource v_walk;

    //public AudioSource voice2;
   // public AudioClip v_walk;

   // public AudioSource voice;
    public AudioClip v_aaa;



    public bool miknatisvar = false;






    // Start is called before the first frame update
    void Start()
    {
        //voice.PlayOneShot(v_walk);
    }

    // Update is called once per frame
    void Update()
    {
        
        hareket();
       // score.ToString();
        
    }

    /*public void ReturnLoad()
    {
       
        //voice.PlayOneShot(v_walk);
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "again1")
        {
            plane2.position = new Vector3(plane1.position.x,plane1.position.y, plane1.position.z + 50.0f);
        }

        if (other.gameObject.name == "again2")
        {
            plane1.position = new Vector3(plane2.position.x,plane2.position.y, plane2.position.z + 50.0f);
        }

        if (other.gameObject.tag == "miknatis")
        {
            voice.PlayOneShot(v_siringa);
            GameObject[] all_miknatis = GameObject.FindGameObjectsWithTag("miknatis");
            foreach ( GameObject miknatis in all_miknatis)
            {
                Destroy(miknatis);
            }
            miknatisvar = true;
            Invoke("resetmiknatis",10.0f);   //10f saniye sonra mıknatıs resetlenir

        }


        if (other.gameObject.name== "siringa(Clone)")
        {

            voice.PlayOneShot(v_siringa); 
            Destroy(other.gameObject);  //temas ettiğimiz cd nin yok olmasını sağlar
            score += 10;

            scoretxt.text = score.ToString("000000");

        }

        if (other.gameObject.name == "bag(Clone)")
        {

            voice.PlayOneShot(v_siringa);
            Destroy(other.gameObject);  //temas ettiğimiz çantanın nin yok olmasını sağlar
            score += 50;
            scoretxt.text = score.ToString("000000");


        }
        /*if (other.gameObject.name == "barrier(Clone)")
        {
            gameend.SetActive(true);
            Time.timeScale = 0.0f;

        }*/
        if (other.gameObject.tag == "Finish")
        {

            hp -= 1;
            hptxt2.text = hp.ToString();
            voice.PlayOneShot(v_aaa);
            Destroy(other.gameObject);

            if (hp == 0 )
            {
                PlayerPrefs.SetInt("highscore", score);
                gameend.SetActive(true);
                SceneManager.LoadScene("finishscene");
            }
           
            

            //Time.timeScale = 0.0f;

        }

        

    }

    void resetmiknatis()
    {
        miknatisvar = false;
    }

    

    private void OnCollisionStay(Collision collision)
    {
        v_walk.enabled = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        v_walk.enabled = false;
    }

    





    IEnumerator stopJump()
    {
        //animationn.SetBool("playerJump", true);
        yield return new WaitForSeconds(.55f); //havaya çıkıp inme süresi
        GetComponent<Rigidbody>().velocity = new Vector3(0, -2, 0);
        yield return new WaitForSeconds(.35f); //bidaha tuşa baılma süresi
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 

        

        midJump = "n";  
    }
    


    void hareket()
    {

        if (Input.GetKey(KeyCode.UpArrow) && (midJump == "n"))
        {
            
            animationn.SetBool("playerJump", true);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 5.5f, 1.9f);
            midJump = "y";
            StartCoroutine(stopJump());
            //Invoke("Ziplama_iptal", 0.5f);


            // animationn.SetBool("playerJump",true);

            //rigi.velocity = Vector3.up * 300f * Time.deltaTime;                        
        }
        else
        {
            animationn.SetBool("playerJump", false);
        }
        /*if (b == 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 2f, transform.position.z), Time.deltaTime * 3.0f);
            //b = 0;
        }*/


        if (Input.GetKeyDown(KeyCode.RightArrow) && a<1 )
        {           
            a += 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && a>-1)
        {
            a -= 1;
        }

        if (a == 0)
        {
            //transform.Translate(transform.position.x - 2, 0, 0);
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), Time.deltaTime * 4.0f);
        }

        if (a==1 /*&& transform.position.x < 2f*/)
        {
            //transform.Translate(transform.position.x + 2,0,0);
            transform.position = Vector3.Lerp(transform.position, new Vector3( 2f, transform.position.y, transform.position.z), Time.deltaTime * 4.0f);
        }

        if (a==-1 /*&& transform.position.x > -2f*/)
        {
            //transform.Translate(transform.position.x - 2, 0, 0);
            transform.position = Vector3.Lerp(transform.position, new Vector3(-2f, transform.position.y, transform.position.z), Time.deltaTime * 4.0f);
        }


        //----------------------
        /*
        if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sag = true;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    sag = false;
                }

                if (sag)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(2f, transform.position.y, transform.position.z), Time.deltaTime * 3.0f);
                }

                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(-2f, transform.position.y, transform.position.z), Time.deltaTime * 3.0f);
                }
        
*/
        float x = 0;
        if (score<800)
        {
            x = score / 80;
        }
        else
        {
            x = 10;
        }
       transform.Translate(0, 0, (8+x) * Time.deltaTime);
        



    }

    void Ziplama_iptal()
    {
       animationn.SetBool("playerJump",false);
       // b = false;
    }










}
