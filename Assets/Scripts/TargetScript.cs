using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;
using UnityEngine.VFX;

public class TargetScript : MonoBehaviour
{
    public AudioSource targetSource;
    private Scoreboard[] scoreboards;
    public GameObject textboard;
    public GameObject effect;
    private void Start()
    {
        scoreboards = FindObjectsOfType<Scoreboard>();
    }
    //public GameObject hitCOUNTER;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            Instantiate(effect, collision.transform.position, transform.rotation);
            targetSource.Play(0);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            foreach (Scoreboard scoreboard in scoreboards)
            {
                scoreboard.hitup();
            }
        }
    }
}
