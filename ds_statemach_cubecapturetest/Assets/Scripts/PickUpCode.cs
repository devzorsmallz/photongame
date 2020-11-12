using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCode : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Pplayer"){
            Destroy(this.gameObject);
        }
    }
}
