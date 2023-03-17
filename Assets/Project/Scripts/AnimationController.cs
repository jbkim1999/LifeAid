using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("1Key")) 
        {
            player.GetComponent<Animator>().Play("Dying");
        }
        if(Input.GetButtonDown("2Key")) 
        {
            // player.GetComponent<Animator>().Play("Dying");
            player.GetComponent<Animator>().Play("Excited");
        }
    }
}
