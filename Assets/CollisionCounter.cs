/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int count;
    public GameObject bar1;
    public GameObject bar2;
    

    void OnCollisionEnter(Collision collision)
    {
        Material barActive = Resources.Load("BarActive", typeof(Material)) as Material;
        if (collision.gameObject.CompareTag("Pack"))
        {
            count++;
            Debug.Log("Colliison Detected! Count: " + count);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            if(count == 1)
            {
                bar1.renderer.material = barActive;
            }
            if (count == 2)
            {
                bar2.renderer.material = barActive;
            }
        }
    }

    public int GetCount()
    {
        return count;
    }
}
*/