using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    public int state = 1;
    public string message = "Please move to the region marked by the blue indicator.";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetState()
    {
        return state;
    }

    public void IncrementState()
    {
        state++;
        switch (state)
        {
            case 2:
                SetMessage("Please read the instructions written on the board. If done, please move to the region marked by the blue indicator.");
                break;
            default:
                break;
        }
        Debug.Log(state);
    }

    private void SetMessage(string msg)
    {
        message = msg;
        Debug.Log(message);
    }
}
