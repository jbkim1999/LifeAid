using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapBehaviour : MonoBehaviour
{
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bottle")
        {
            GetComponent<Renderer>().material.color = Color.green;
            if (!other.gameObject.GetComponent<XRGrabInteractable>().isSelected)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.transform.position = transform.position;
                other.gameObject.transform.rotation = transform.rotation;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bottle")
        {
            GetComponent<Renderer>().material.color = originalColor;
        }
    }
}
