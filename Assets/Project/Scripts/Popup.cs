using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
  public OVRInput.Controller Controller;

  public string instructionButton_enter;
  public string instructionButton_exit;
  private bool showInstructions;
  public GameObject instructions;

  // Start is called before the first frame update
  void Start()
  {
        instructions.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetAxis(instructionButton_enter) == 1 && showInstructions == false)
        {
            showInstructions = true;
            instructions.SetActive(showInstructions);
            Debug.Log("canvas_is_True");
        }
     else if(Input.GetAxis(instructionButton_exit) == 1 && showInstructions == true)
       {
           showInstructions = false;
           instructions.SetActive(showInstructions);
           Debug.Log("canvas_is_False");
        }
  }

  private void ToggleInstructions() 
    {
        Debug.Log(showInstructions);
        if(showInstructions == true) 
        {
            showInstructions = false;
        }
        else{
            showInstructions = true;
        }
    }



}

//LHandPrimary