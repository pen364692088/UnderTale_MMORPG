using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerCtrl : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
    }
}
