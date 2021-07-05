using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastzombie : MonoBehaviour
{


    float v = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, v * Time.deltaTime);

    }
}
