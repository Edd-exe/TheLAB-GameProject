using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miknatisscript : MonoBehaviour
{


    Play agentTpos;
    Transform box;

    float mesafe;

    // Start is called before the first frame update
    void Start()
    {
        agentTpos = GameObject.Find("agentTpos").GetComponent<Play>();
        box = GameObject.Find("agentTpos/box").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (agentTpos.miknatisvar == true)
        {
            mesafe = Vector3.Distance(transform.position,box.position);

            if (mesafe <= 10)
            {
                transform.position = Vector3.MoveTowards(transform.position, box.position, Time.deltaTime *20.0f);  
            }
        }
        
    }
}
