using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;

public class TargetScript : MonoBehaviour
{
    public AudioSource targetSource;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            // DialogUI.Instance
            // .SetTitle("Target Hit")
            // .SetMessage("Well Done!")
            // .Show();
            targetSource.Play(0);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
