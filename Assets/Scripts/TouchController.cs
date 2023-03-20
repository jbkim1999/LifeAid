using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public OVRInput.Controller Controller;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Controller == OVRInput.Controller.LTouch)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(Controller);
            transform.localRotation = OVRInput.GetLocalControllerRotation(Controller);
        }
        if (Controller == OVRInput.Controller.RTouch)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(Controller);
            transform.localRotation = OVRInput.GetLocalControllerRotation(Controller) * Quaternion.Euler(0, 90, 0);
        }
    }
}
