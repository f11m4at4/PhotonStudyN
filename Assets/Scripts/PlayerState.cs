using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerState : MonoBehaviourPun
{
    //�÷��̾� ���� Enmu
    public enum State
    {
        IDLE,
        MOVE,
        DIE
    }

    //���� ����
    public State currState;
    //Animator
    public Animator anim;

    //�׾��� �� off����� �ϴ� GameObject��
    public GameObject[] disableGo;
    //�׾��� �� off����� �ϴ� Component��
    public MonoBehaviour[] disableCom;

    //���� ���� �Լ�
    public void ChangeState(State s)
    {        
        //���� ���°� s�� ���ٸ� �Լ��� ������
        if (currState == s) return;

        //���� ���¸� s���·�!
        currState = s;
        //���¿� ���� animation ó��
        switch(s)
        {
            case State.IDLE:
                photonView.RPC("RpcSetTrigger", RpcTarget.All, "Idle");
                break;
            case State.MOVE:
                photonView.RPC("RpcSetTrigger", RpcTarget.All, "Move");
                break;
            case State.DIE:
                //��, ui off, PlayerFire ������Ʈ off
                //1. GameObject Off
                for (int i = 0; i < disableGo.Length; i++)
                    disableGo[i].SetActive(false);
                //2. Component Off
                for (int i = 0; i < disableCom.Length; i++)
                    disableCom[i].enabled = false;
                break;
        }
    }
    
    [PunRPC]
    void RpcSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
