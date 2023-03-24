using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int count;
    List<GameObject> currentCollisions = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pack"))
        {
            count++;
            currentCollisions.Add(collision.gameObject);
            Debug.Log("Colliison Detected! Count: " + count);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
