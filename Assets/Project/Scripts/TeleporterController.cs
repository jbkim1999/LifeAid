using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour {

    public Teleporter teleporter;
    public string buttonName;

	void Update () {

        if (Input.GetAxis(buttonName) == 1) // Change to VR Controller
        {
            teleporter.ToggleDisplay(true);
        }

        if(Input.GetAxis(buttonName) < 1) // Change to VR Controller
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
        }
	}
}
