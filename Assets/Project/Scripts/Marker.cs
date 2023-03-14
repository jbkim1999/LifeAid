using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public States statesInstance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (other.CompareTag("Player") && statesInstance.GetState() == 1)
        {
            statesInstance.IncrementState();
            gameObject.SetActive(false);
        }
    }
}
