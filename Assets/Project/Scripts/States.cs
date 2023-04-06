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
        "Move to the region marked by the blue indicator. Grab James by pressing the side trigger on your controllers, and move him under the Gazebo. You can close this instruction by pressing the X button, or the lower button on your left controller.",
        "Find two ice packs somewhere in the gazebo and place them on James' body to cool him down.",
        "James is now awake, hand him some water to drink by placing the water bottle into the snap zone.",
        "Go to the signboard and learn more about heat exhaustion.",
        "Congratulations, you have completed the tutorial on heat exhaustion!"
    };
    public GameObject handBoard;
    public int state = 1;
    public string message = directions[1]; // message for the state

    public AudioSource successAudio;
    public GameObject person;
    public GameObject leftHand;

    public GameObject torso;
    private float model_x;
    private float model_y;
    private float model_z;

    // State 1
    public GameObject marker_state1;
    private float marker_state1_x;
    private float marker_state1_z;

    // State 2
    private int packCounter = 0;

    // State 3
    public GameObject bottleZone;

    // State 4
    public GameObject marker_state4;
    private float marker_state4_x;
    private float marker_state4_z;

    // for fading in and out
    public GameObject FaderScreen;
    public int fadeDuration;
    public Color fadeColor;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = FaderScreen.GetComponent<Renderer>();
        SetMessage(directions[1]);
        SetPositions();
        SleepAndExecute(3);
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

        // State 4
        marker_state4_x = marker_state4.transform.position.x;
        marker_state4_z = marker_state4.transform.position.z;
        marker_state4.SetActive(false);
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
            marker_state1.SetActive(false);
        }
        if (person.GetComponent<XRGrabInteractable>().isSelected)
        {
            successAudio.Play(0);
            StartCoroutine(FadeOut());
            Debug.Log("fading out");

            // make person immovable and ungrabbable
            person.GetComponent<XRGrabInteractable>().enabled = false;
            IncrementState();
        }
    }

    private void CheckSuccessful2()
    {
        if (person.GetComponent<CollisionCounter>().GetCount() == 2)
        {
            foreach (GameObject obj in person.GetComponent<CollisionCounter>().GetColliders())
            {
                obj.SetActive(false);
            }
            successAudio.Play(0);
            SleepAndExecuteState2(3);
            IncrementState();
        }
    }

    private void CheckSuccessful3()
    {
        if (bottleZone.GetComponent<SnapBehaviour>().IsFilled())
        {
            successAudio.Play(0);
            IncrementState();
        }

    }

    private void CheckSuccessful4()
    {
        float curr_x = leftHand.transform.position.x;
        float curr_z = leftHand.transform.position.z;

        if (Mathf.Pow(curr_x - marker_state4_x, 2) + Mathf.Pow(curr_z - marker_state4_z, 2) <= 3)
        {
            marker_state4.SetActive(false);
            successAudio.Play(0);
            IncrementState();
        }
    }

    public void IncrementState()
    {
        state++;
        switch (state)
        {
            case 2:
                
                SetMessage(directions[2]);
                break;

            case 3:
                SetMessage(directions[3]);
                break;

            case 4:
                marker_state4.SetActive(true);
                SetMessage(directions[4]);
                break;

            case 5:
                SetMessage(directions[5]);
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

        // DisableColliders();
        person.GetComponent<Animator>().Play("Stunned");
        yield return new WaitForSeconds(2);

        UpdateBoxCollider(0.375f, 1.4f, 0.70f);
        // EnableColliders();

    }

    private void UpdateBoxCollider(float x, float y, float z)
    {
        Vector3 pos = person.transform.position;

        BoxCollider collider = person.GetComponent<BoxCollider>();
        collider.center = new Vector3(0, 0.21f, -1.1f);
        collider.size = new Vector3(z, x, y);
    }

    private void SleepAndExecuteState2(int s)
    {
        StartCoroutine(SleepCoroutineState2(s));
    }

    private IEnumerator SleepCoroutineState2(int s)
    {
        yield return new WaitForSeconds(s);
        person.transform.position = new Vector3(model_x, model_y, model_z + 0.85f);
        person.GetComponent<Animator>().Play("Situp To Idle");
        bottleZone.SetActive(true);
    }

    /* helpers and routines for fading the screen to black (and back) */

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(FadeRoutine(1, 0));
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeRoutine(0, 1));

        yield return new WaitForSeconds(1);
        marker_state1.SetActive(false); // double check

        person.transform.position = new Vector3(9f, 0.39f, 7.34f);
        person.transform.Rotate(0f, -20f, 0f);

        model_x = person.transform.position.x;
        model_y = person.transform.position.y;
        model_z = person.transform.position.z;


        yield return StartCoroutine(FadeIn());
        Debug.Log("fading in");

    }


    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {

            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }

    /* Unused methods */
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
