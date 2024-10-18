using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject Jinpu;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Jinpu, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(Jinpu, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(Jinpu, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(Jinpu, new Vector3(0, 0, 0), Quaternion.identity);
        //then instatiate the object
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
