using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex;

    public override void OnEnable(){
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable(){
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom(){
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
