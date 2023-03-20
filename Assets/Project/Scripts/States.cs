using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class States : MonoBehaviour
{
    // 1-indexed
    static string[] directions = {
        "",
        "Please move to the region marked by the blue indicator. You can teleport to the area by pressing the grab button, pointing to the place, and releasing the button.",
        "Please read the instructions written on the board. If done, go see your friend James on the right. He's waiting for you!",
        "It seems that James is under heat exhaustion. If left uncared, it will develop into heat stroke. Help him to move to a cooler place, under the Gazebo.",
        "Find 3 ice packs somewhere in the gazebo and place them on James' body to cool him down! Quick!",
        "Good job! James is awake, hand him some water to drink."
    };
    public GameObject handBoard;
    public int state = 1;
    public string message = directions[1]; // message for the state

    public AudioSource successAudio;
    public GameObject person;
    public GameObject leftHand;

    // State 1
    public GameObject marker_state1;
    private float marker_state1_x;
    private float marker_state1_z;

    // State 2
    public GameObject marker_state2;
    private float marker_state2_x;
    private float marker_state2_z;

    // State 3
    public GameObject marker_state3;
    private float marker_state3_x;
    private float marker_state3_z;
    public GameObject torso;

    // State 4
    public GameObject bottleZone;
    private int packCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetMessage(directions[1]);
        SetPositions();
    }

    public int GetState()
    {
        return state;
    }

    private void SetMessage(string msg)
    {
        message = msg;
        handBoard.GetComponent<TMP_Text>().text = msg.ToString();
        Debug.Log(message);
    }

    void SetPositions()
    {
        // State 1
        marker_state1_x = marker_state1.transform.position.x;
        marker_state1_z = marker_state1.transform.position.z;

        // State 2
        marker_state2_x = marker_state2.transform.position.x;
        marker_state2_z = marker_state2.transform.position.z;
        marker_state2.SetActive(false);

        // State 3
        marker_state3_x = marker_state3.transform.position.x;
        marker_state3_z = marker_state3.transform.position.z;
        marker_state3.SetActive(false);
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
        } else if (state == 3)
        {
            CheckSuccessful3();
        } else if (state == 4)
        {
            CheckSuccessful4();
        }
    }

    private void CheckSuccessful1()
    {
        float curr_x = leftHand.transform.position.x;
        float curr_z = leftHand.transform.position.z;

        if (Mathf.Pow(curr_x - marker_state1_x, 2) + Mathf.Pow(curr_z - marker_state1_z, 2) <= 3)
        {
            successAudio.Play(0);
            IncrementState();
        }
    }

    private void CheckSuccessful2()
    {
        float curr_x = leftHand.transform.position.x;
        float curr_z = leftHand.transform.position.z;

        if (Mathf.Pow(curr_x - marker_state2_x, 2) + Mathf.Pow(curr_z - marker_state2_z, 2) <= 3)
        {
            successAudio.Play(0);
            IncrementState();
        }
    }

    private void CheckSuccessful3()
    {
        float curr_x = leftHand.transform.position.x;
        float curr_z = leftHand.transform.position.z;

        float model_x = person.transform.position.x;
        float model_z = person.transform.position.z;

        //if (Mathf.Pow(curr_x - marker_state3_x, 2) + Mathf.Pow(curr_z - marker_state3_z, 2) <= 3)
        if (Mathf.Pow(model_x - marker_state3_x, 2) + Mathf.Pow(model_z - marker_state3_z, 2) <= 2 && !person.GetComponent<XRGrabInteractable>().isSelected)
        {
            successAudio.Play(0);
            person.transform.position = new Vector3(model_x, 0.31f, model_z-0.2f);
            // person.GetComponent<Animator>().Play("Stroke Shaking Head");
            IncrementState();
        }
    }

    private void CheckSuccessful4()
    {
        if (person.GetComponent<CollisionCounter>().GetCount() == 2)
        {
            successAudio.Play(0);
            SleepAndExecuteState4(2);
            IncrementState();
        }
    }

    public void IncrementState()
    {
        state++;
        switch (state)
        {
            case 2:
                marker_state1.SetActive(false);
                marker_state2.SetActive(true);
                SetMessage(directions[2]);
                break;

            case 3:
                marker_state2.SetActive(false);
                marker_state3.SetActive(true);
                SleepAndExecute(2); // Sleep to give some time to the players to react (or say hello) and execute whatever needed
                break;

            case 4:
                marker_state3.SetActive(false);
                // make person immovable and ungrabbable
                person.GetComponent<Rigidbody>().isKinematic = true;
                person.GetComponent<XRGrabInteractable>().enabled = false;
                SetMessage(directions[4]);
                break;

            case 5:
                SetMessage(directions[5]);
                bottleZone.SetActive(true);
                break;

            default:
                break;
        }
        Debug.Log(state);
    }

    private void SleepAndExecute(int s)
    {
        StartCoroutine(SleepCoroutine(s));
    }

    private void SleepAndExecuteState4(int s)
    {
        StartCoroutine(SleepCoroutineState4(s));
    }

    private IEnumerator SleepCoroutineState4(int s)
    {
        yield return new WaitForSeconds(s);
        person.GetComponent<Animator>().Play("Situp To Idle");
    }

    private IEnumerator SleepCoroutine(int s)
    {
        yield return new WaitForSeconds(s);

        // DisableColliders();
        person.GetComponent<Animator>().Play("Stunned");
        yield return new WaitForSeconds(2);

        UpdateBoxCollider(0.55f, 1.4f, 0.70f);
        // EnableColliders();

        SetMessage(directions[3]);
    }

    private void UpdateBoxCollider(float x, float y, float z)
    {
        Vector3 pos = person.transform.position;

        BoxCollider collider = person.GetComponent<BoxCollider>();
        collider.center = new Vector3(0, 0.27f, -1.1f);
        collider.size = new Vector3(z, x, y);

        BoxCollider colliderTorso = torso.GetComponent<BoxCollider>();
        colliderTorso.center = new Vector3(0, 0.27f, -1.1f);
    }

    private void DisableColliders()
    {
        // person.GetComponent<BoxCollider>().enabled = false;
        torso.GetComponent<MeshCollider>().enabled = false;
    }

    private void EnableColliders()
    {
        // person.GetComponent<BoxCollider>().enabled = true;
        torso.GetComponent<MeshCollider>().enabled = true;
    }
}
