using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{

    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine){
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar")),
            GameSetupController.GS
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
