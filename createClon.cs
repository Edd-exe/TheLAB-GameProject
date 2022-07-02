using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createClon : MonoBehaviour
{

    //oyun da kullanmak için tanımlanmış gameobeject ler 
    public GameObject obstacle1;  //zombi polis
    public GameObject obstacle2;  //zombi hasta
    public GameObject obstacle3; //robot 1
    public GameObject obstacle4;
    public GameObject obstacle5; //z3
    public GameObject obstaclebig;

    public GameObject mik;
    public GameObject cd;
    public GameObject cd2;

    public GameObject barrier;
    public GameObject barrier2;
    public GameObject barrier3;
    public GameObject barrier4;

    //test push
    //. . .
    //public TMPro.TextMeshProUGUI scoretxt;
    //Play py = new Play();



    //Play script tindeki score int değerini çağırmak için yazdık
    private Play ss;
    public GameObject obje;
    public Transform player;


    float deletetime = 10.0f;

    float topx = -2.0f;   // sol şerit
    float midx = 0f;  //orta şerit
    float botx = 2.0f;  //sağ şerit

    
    void Start()
    {
        InvokeRepeating("objectclone", 0, 0.5f) ;   //***************


        ss = obje.GetComponent<Play>();   // Play scriptin dekş public değeleri çağırabilmemizi sağlar


    }

  
    void Update()
    {
        
    }

    
    void objectclone()     //  hangi objenin makeclone fonksiyonu çağırlacağını belirler
    { 

        int i = Random.Range(0,100);

        //------------------------------------------------ //   sabit nesneler
        if (i < 40)
        {
            makeclone(cd, 1f); //şırınga 
        }
        if (i > 40 && i < 45)
        {
            makeclone(barrier3, 0.4f); //sedye
        }
        if (i > 45 && i < 50)
        {
            makeclone(barrier4, 0.45f); //sandaye
        }
        if (i > 50 && i < 55)
        {
            makeclone(cd2, 1f); //çanta
        }
        if (i > 55 && i < 60)
        {
            makeclone(mik, 1f); //miknatis
        }
        if (i > 60 && i < 65)
        {
            makeclone(barrier, 0f); //sabit zombie
        }
        if (i > 65 && i < 70)
        {
            makeclone(barrier2, 0f); //sürünen zombie
        }

        //----------------------------------------------- //   hareketli nesneler

        if (i < 10 && ss.score > 70)
        {
            makeclone(obstacle1, 0f); //polis zombi 
        }
        if (i > 10 && i < 20 && ss.score > 90)
        {
            makeclone(obstacle2, 0f); //hasta zombie 
        }
        if (i > 20 && i < 30 && ss.score > 120)
        {
            makeclone(obstacle5, 0.05f); //topal zombi
        }

        // ------------------------------------------------ // hızlı hareketli nesneler

        if (i > 30 && i < 35 && ss.score > 400)
        {
            makeclone(obstacle3, 0f); //hızlı zombie1 
        }
        if (i > 35 && i < 40 && ss.score > 600)
        {
            makeclone(obstacle4, 0f); //hızlı zombie2
        }
        if (i > 40 && i < 45 && ss.score > 1000)
        {
            makeclone2(obstaclebig, 0f); //dev zombie
        }

        //-----------------------------------------------  //
        //-----------------------------------------------  // + hareketli nesneler
        if (i > 40 && i < 45 && ss.score > 1200)
        {
            makeclone(obstacle1, 0f); //polis zombi 
        }
        if (i > 50 && i < 55 && ss.score > 1400)
        {
            makeclone(obstacle2, 0f); //hasta zombie 
        }
        if (i > 60 && i < 65 && ss.score > 1600)
        {
            makeclone(obstacle5, 0.05f); //topal zombi
        }
        //----------------------------------------------- // + sabit nesneler
        if (i > 70 && i < 85 && ss.score > 1200)
        {
            makeclone(cd, 1f); //şırınga 
        }
        if (i > 85 && i < 90 && ss.score > 600)
        {
            makeclone(barrier, 0f); //sabit zombie
        }
        if (i > 95 && i < 100 && ss.score > 800)
        {
            makeclone(barrier2, 0f); //sürünen zombie
        }


    }

    void makeclone(GameObject nesne, float y)   //objenin hangi koridora ne kadar yükseklikte ve uzaklıkla klonalanacağını belirleriz 
    {
        GameObject newclone = Instantiate(nesne);  //***************
        

        int i = Random.Range(0,99);

        if (i<33)
        {     
            newclone.transform.position = new Vector3(topx,y,player.position.z + 40.0f);      
        }
        else if(i < 66)
        {
            newclone.transform.position = new Vector3(midx, y, player.position.z + 40.0f);
        }
        else if(i < 99)
        {
            newclone.transform.position = new Vector3(botx, y, player.position.z +40.0f);
        }
        Destroy(newclone, deletetime);    // clonlanan nesnenin deletetime  süresi sonra yok olmasını sağlar

    }

    void makeclone2(GameObject nesne, float y)   //  yukardaki işlemi dev zombi 2 şeritlik yer kapladığı için farklı şeklide yapmamazı sağlar
    {
        GameObject newclone2 = Instantiate(nesne);

        int i = Random.Range(0, 100);  

        if (i <50)
        {
            newclone2.transform.position = new Vector3(1, y, player.position.z + 60.0f);
        }
        else
        {
            newclone2.transform.position = new Vector3(-1, y, player.position.z + 60.0f);
        }

        Destroy(newclone2, deletetime);


    }

    }
