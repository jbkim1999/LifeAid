using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoePortalCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShoePortal")
        {
            other.gameObject.SetActive(false);

            // TODO: Launch Playground Scene
        }
    }
}
