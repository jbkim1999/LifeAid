using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public States statesInstance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
