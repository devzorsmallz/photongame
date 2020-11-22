﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomController : MonoBehaviourPunCallbacks {
    // Audio stuff
    public AudioClip audioClip;
    private AudioSource audioSource;

    private PhotonView myPhotonView;

    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int menuSceneIndex;
    private int playerCount;
    private int roomSize;
    [SerializeField]
    private int minPlayersToStart;

    [SerializeField]
    private Text roomCountDisplay;
    [SerializeField]
    private Text timerToStartDisplay;

    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;

    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameWaitTime;

    private void Start () {
        // Audio stuff
        audioSource = GetComponent<AudioSource>();

        myPhotonView = GetComponent<PhotonView> ();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        PlayerCountUpdate ();
    }

    void PlayerCountUpdate () {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + ":" + roomSize;

        if (playerCount == roomSize) {
            readyToStart = true;
        } else if (playerCount >= minPlayersToStart) {
            readyToCountDown = true;
        } else {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom (Player newPlayer) {
        PlayerCountUpdate ();

        if (PhotonNetwork.IsMasterClient) {
            myPhotonView.RPC ("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer (float timeIn) {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if (timeIn < fullGameTimer) {
            fullGameTimer = timeIn;
        }
    }
    public override void OnPlayerLeftRoom (Player otherPlayer) {
        PlayerCountUpdate ();

    }

    private void Update () {
        WaitingForMorePlayers ();
    }

    void WaitingForMorePlayers () {
        if (playerCount <= 1) {
            ResetTimer ();
        }

        if (readyToStart) {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
            Debug.Log ("-------------------------------");
        } else if (readyToCountDown) {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
            Debug.Log ("++++++++++++++++++++++++++++++++++");
        }

        string tempTimer = string.Format ("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        if (timerToStartGame <= 0f) {
            Debug.Log ("**************************************");
            if (startingGame) {
                return;
            }
            StartGame ();
        }
    }

    void ResetTimer () {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    public void StartGame () {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient) {
            return;
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel (multiplayerSceneIndex);
    }

    public void DelayCancel () {
        // Audio stuff
        audioSource.PlayOneShot(audioClip);
        StartCoroutine("DelayCancelCoroutine");
    }

    // Audio stuff
    private IEnumerator DelayCancelCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        // Photon stuff
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}