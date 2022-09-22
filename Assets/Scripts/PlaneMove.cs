using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlaneMove : MonoBehaviourPun
{
    public bool isCreator;
    void Start()
    {
        isCreator = photonView.IsMine;
    }

    void Update()
    {
        if(isCreator)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            photonView.RPC("RpcSetDir", RpcTarget.MasterClient, h, v);            
        }

        if(PhotonNetwork.IsMasterClient)
        {
            if(dir.sqrMagnitude > 0)
            {
                transform.position += dir * 5 * Time.deltaTime;
            }
        }


        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            if(!PhotonNetwork.IsMasterClient && photonView.IsMine)
            {
                photonView.TransferOwnership(PhotonNetwork.MasterClient);
            }
        }
    }

    Vector3 dir;
    [PunRPC]
    void RpcSetDir(float h, float v)
    {
        dir = new Vector3(h, v, 0);
        dir.Normalize();
    }
}
