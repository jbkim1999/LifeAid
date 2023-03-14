using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    public int state = 1;
    public string message = "Please move to the region marked by the blue indicator.";

    public AudioSource successAudio;

    public GameObject marker_state1;
    private float marker_state1_x;
    private float marker_state1_z;

    // Start is called before the first frame update
    void Start()
    {
        marker_state1_x = marker_state1.transform.position.x;
        marker_state1_z = marker_state1.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            CheckSuccessful1();
        } else if (state == 2)
        {
            CheckSuccessful2();
        }
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
                SetMessage("Please read the instructions written on the board. If done, go see your friend James on the right. He's waiting for you!");
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

    private void CheckSuccessful1()
    {
        float curr_x = gameObject.transform.position.x;
        float curr_z = gameObject.transform.position.z;

        if (Mathf.Pow(curr_x - marker_state1_x, 2) + Mathf.Pow(curr_z - marker_state1_z, 2) <= 3)
        {
            successAudio.Play(0);
            marker_state1.SetActive(false);
            IncrementState();
        }
    }

    private void CheckSuccessful2()
    {

    }
}
