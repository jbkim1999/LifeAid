using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;
using UnityEngine.VFX;

public class TargetScript : MonoBehaviour
{
    public AudioSource targetSource;
    public GameObject effect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            // DialogUI.Instance
            // .SetTitle("Target Hit")
            // .SetMessage("Well Done!")
            // .Show();
            Instantiate(effect, transform.position + new Vector3(0f, 0.3f, 0f), transform.rotation);
            targetSource.Play(0);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            effect.SetActive(true);
        }
    }
}
