using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int count;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pack"))
        {
            count++;
            Debug.Log("Colliison Detected! Count: " + count);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public int GetCount()
    {
        return count;
    }
}
