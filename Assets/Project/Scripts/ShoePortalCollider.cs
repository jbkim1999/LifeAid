using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoePortalCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShoePortal")
        {
            other.gameObject.SetActive(false);

            // Load Playground Scene
            SceneManager.LoadScene(1);
        }
    }
}
