using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class StartGame : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartGameFunc()
    {
        audioSource.PlayOneShot(audioClip);
        StartCoroutine("StartGameCoroutine");
    }

    public IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Launcher.unity");
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(audioClip);
        StartCoroutine("QuitGameCoroutine");
    }

    public IEnumerator QuitGameCoroutine()
    {
        Debug.Log("Quitting Application");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void BackToMenu()
    {
        audioSource.PlayOneShot(audioClip);
        StartCoroutine("BackToMenuCoroutine");
    }
    
    public IEnumerator BackToMenuCoroutine()
    {
        Debug.Log("Disconnecting...");
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}
