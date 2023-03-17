using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    // 1-indexed
    static string[] directions = {
    "",
    "Please move to the region marked by the blue indicator. You can teleport to the area by pressing the grab button, pointing to the place, and releasing the button.",
    "Please read the instructions written on the board. If done, go see your friend James on the right. He's waiting for you!",
    "It seems that James is under heat exhaustion. If left uncared, it will develop into heat stroke. Help him to move to a cooler place, under the Gazebo."
    };

    public int state = 1;
    public string message = directions[1]; // message for the state

    public AudioSource successAudio;
    public GameObject person;

    // State 1
    public GameObject marker_state1;
    private float marker_state1_x;
    private float marker_state1_z;

    // State 2
    public GameObject marker_state2;
    private float marker_state2_x;
    private float marker_state2_z;

    // Start is called before the first frame update
    void Start()
    {
        SetPositions();
    }

    public int GetState()
    {
        return state;
    }

    private void SetMessage(string msg)
    {
        message = msg;
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
        float curr_x = gameObject.transform.position.x;
        float curr_z = gameObject.transform.position.z;

        if (Mathf.Pow(curr_x - marker_state1_x, 2) + Mathf.Pow(curr_z - marker_state1_z, 2) <= 3)
        {
            successAudio.Play(0);
            IncrementState();
        }
    }

    private void CheckSuccessful2()
    {
        float curr_x = gameObject.transform.position.x;
        float curr_z = gameObject.transform.position.z;

        if (Mathf.Pow(curr_x - marker_state2_x, 2) + Mathf.Pow(curr_z - marker_state2_z, 2) <= 3)
        {
            successAudio.Play(0);
            IncrementState();
        }
    }

    private void CheckSuccessful3()
    {

    }

    private void CheckSuccessful4()
    {

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
                SleepAndExecute(2); // Sleep to give some time to the players to react (or say hello) and execute whatever needed
                break;

            case 4:
                // Before moving to state 4, how about we disable the isKinematic of the model to true so that no more moving or pushing by hands is allowed?
                // person.GetComponent<Rigidbody>().isKinematic = true;
                // person.GetComponent<"XR Grab Interactable" as scriptname>().enabled = false; // How do I address a script name with spaces haha...
                break;

            case 5:
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

    private IEnumerator SleepCoroutine(int s)
    {
        yield return new WaitForSeconds(s);

        // NEED A FAINTING ANIMATION HERE
        person.GetComponent<Animator>().Play("Stunned");
        yield return new WaitForSeconds(2);

        UpdateBoxCollider(0.55f, 1.4f, 0.70f);
        SetMessage(directions[3]);
    }

    private void UpdateBoxCollider(float x, float y, float z)
    {
        Vector3 pos = person.transform.position;
        BoxCollider collider = person.GetComponent<BoxCollider>();
        collider.center = new Vector3(0, 0.27f, -1.1f);
        collider.size = new Vector3(z, x, y);
    }
}
