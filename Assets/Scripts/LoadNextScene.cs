using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundT;

    public bool returnMenu = false;
    void Awake()
    {
        AudioSource.PlayClipAtPoint(soundT,transform.position);
    }


    void LoadNextLevel()
    {
        if (returnMenu == false)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        else
        SceneManager.LoadScene("MenuPrincipal");

    }





}
