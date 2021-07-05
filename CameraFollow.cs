using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  
    void Start()
    {
        
    }
    public Transform cameraCube;

    
    void LateUpdate()  //
    {
        transform.position = Vector3.Lerp(transform.position, cameraCube.position, Time.deltaTime* 10f);  
        
        // kamera hareket kodu. Oyun kamerası ajan karekterine bağlı olan cameracube nesnesinin kordinatlarını takip eder
        // time.delta kameranını cbe doğru giderkenki sırada gecikmesini ayarlamamıza yarar
    }
}
