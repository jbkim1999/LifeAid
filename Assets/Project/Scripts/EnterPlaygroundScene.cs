using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPlaygroundScene : MonoBehaviour
{
    public GameObject FaderScreen;
    public int fadeDuration;
    public Color fadeColor;
    private Renderer rend;
    
    void Start()
    {
        rend = FaderScreen.GetComponent<Renderer>();
        StartCoroutine(FadeRoutine(1, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShoePortal")
        {
            other.gameObject.SetActive(false);

            // Launch Playground Scene
            GoToScene();
        }
    }

    public void GoToScene()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    public IEnumerator LoadSceneRoutine()
    {
        yield return StartCoroutine(FadeRoutine(0, 1));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {

            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }

}
