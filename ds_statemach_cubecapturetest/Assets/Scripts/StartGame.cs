using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
