using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yurud : MonoBehaviour
{
    float v = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    
    // Update is called once per frame
    void Update()
    {

       if (transform.position.x < -2f)
        {
            transform.position =  new Vector3(5.1f, 0.104f, 38.5f);

        }
        transform.Translate(0, 0, v * Time.deltaTime);
        


    }
}
