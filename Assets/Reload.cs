using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public GameObject Panel;

    public AudioSource mainAudioSource;
    public AudioSource audioSource;


    public void ClickButton()
    {
        StartCoroutine(ReturnToLevel());
    }


    IEnumerator ReturnToLevel()
    {
        yield return new WaitForSeconds(0.5f);
        Panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        mainAudioSource.gameObject.SetActive(true);
        audioSource.gameObject.SetActive(false);

    }

}
