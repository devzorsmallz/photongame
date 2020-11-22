using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupController : MonoBehaviour {

    public static GameSetupController GS;

    public Text healthDisplay;

    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;

    public int nextPlayersTeam;

    private void OnEnable () {
        if (GameSetupController.GS == null) {
            GameSetupController.GS = this;
        }
    }

    public void DisconnectPLayer(){
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad(){
        PhotonNetwork.Disconnect();
        while(PhotonNetwork.IsConnected){
            yield return null;
        }
        //SceneManager.LoadScene(MultiplayerSetting.multiplayerSetting.menuScene);
    }

    private void CreatePlayer () {
        Debug.Log ("Creating Player");
        PhotonNetwork.Instantiate (Path.Combine ("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    public void UpdateTeam(){
        if(nextPlayersTeam == 1){
            nextPlayersTeam = 2;
        }
        else{
            nextPlayersTeam = 1;
        }

    }
}