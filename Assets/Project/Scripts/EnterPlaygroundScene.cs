using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPlaygroundScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShoePortal")
        {
            other.gameObject.SetActive(false);

            // Launch Playground Scene
            SceneManager.LoadScene(1);
        }
    }
}
