using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks {

    // Audio stuff
    public AudioClip audioClip;
    private AudioSource audioSource;

    public GameObject loadingButton;
    public GameObject quitButton;

    [SerializeField]
    private GameObject delayStartButton;
    [SerializeField]
    private GameObject delayCancelButton;
    [SerializeField]
    private int roomSize;
    private void Start() {

        // Audio stuff
        audioSource = GetComponent<AudioSource>();

        Debug.Log("Step 1");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Fuckin Made it");
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartButton.SetActive(true);
        loadingButton.SetActive(false);
        quitButton.SetActive(true);
    }

    public void DelayStart(){
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        audioSource.PlayOneShot(audioClip);

        quitButton.SetActive(false);

        // Audio stuff
        StartCoroutine("DelayStartCoroutine");
    }

    // Audio stuff
    private IEnumerator DelayStartCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        // Photon stuff
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Delay Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        CreateRoom();
    }

    void CreateRoom(){
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }
    public void DelayCancel(){
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}