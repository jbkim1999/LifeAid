using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
  public OVRInput.Controller Controller;
  public string buttonName;
  public float grabRadius;
  public LayerMask grabMask;
  public GameObject objectToSpawn;
  public float shootMultiplier = 1000f;
  public AudioSource gunAudio;
  public AudioSource pickUpAudio;

  private GameObject grabbedObject;
  private bool grabbing;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (!grabbing && Input.GetAxis(buttonName) == 1)
    {
      GrabObject();
    }
    if (grabbing && Input.GetAxis(buttonName) < 1)
    {
      DropObject();
    }
  }

  void GrabObject()
  {
    grabbing = true;
    RaycastHit[] hits;
    hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);
    if (hits.Length > 0)
    {
      int closestHit = 0;
      for (int i = 0; i < hits.Length; i++)
      {
        if ((hits[i]).distance < hits[closestHit].distance)
        {
          closestHit = i;
        }
      }

      grabbedObject = hits[closestHit].transform.gameObject;
      grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
      if (Controller == OVRInput.Controller.RTouch)
      {
        grabbedObject.transform.position = transform.position + OVRInput.GetLocalControllerRotation(Controller) * Vector3.forward * 0.17f;
        pickUpAudio.Play(0);
      } else
      {
        grabbedObject.transform.position = transform.position;
      }
      grabbedObject.transform.parent = transform;
    }
  }

  void DropObject()
  {
    grabbing = false;
    if (grabbedObject != null)
    {
      grabbedObject.transform.parent = null;
      grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
      if (Controller == OVRInput.Controller.RTouch)
      {
          gunAudio.Play(0);
          grabbedObject.GetComponent<Rigidbody>().AddForce(OVRInput.GetLocalControllerRotation(Controller) * Vector3.forward * shootMultiplier);
      } else
      {
        grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller);
        grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller);
      }
      Instantiate(objectToSpawn, transform.position + OVRInput.GetLocalControllerRotation(Controller) * new Vector3(0.6f, 0f, 0.4f), Quaternion.identity);
      Instantiate(objectToSpawn, transform.position + OVRInput.GetLocalControllerRotation(Controller) * new Vector3(0.5f, 0f, 0.5f), Quaternion.identity);
      // Instantiate(objectToSpawn, new Vector3(-3.4f, 1f, 6.4f), Quaternion.identity);
      grabbedObject = null;
    }
  }
}
