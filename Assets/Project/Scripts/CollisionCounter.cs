using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int count;
    List<GameObject> currentCollisions = new List<GameObject>();

    public GameObject bar1;
    public GameObject bar2;
    public Material barActive;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pack"))
        {
            count++;
            currentCollisions.Add(collision.gameObject);
            Debug.Log("Colliison Detected! Count: " + count);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (count == 1)
        {
            bar1.GetComponent<Renderer>().material = barActive;
        }
        if (count == 2)
        {
            bar2.GetComponent<Renderer>().material = barActive;
        }
    }

    // void OnCollisionExit(Collision collision)
    // {
        // if (collision.gameObject.CompareTag("Pack"))
        // {
            // count--;
            // currentCollisions.Remove(collision.gameObject);
            // Debug.Log("Colliison Removed! Count: " + count);
            // collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        // }
    // }

    public int GetCount()
    {
        return count;
    }

    public List<GameObject> GetColliders()
    {
        return currentCollisions;
    }
}
