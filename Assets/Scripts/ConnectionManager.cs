using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    //�г��� InputField
    public InputField inputNickName;
    //���� Button
    public Button btnConnect;

    void Start()
    {
        //inputNickName ���� ���Ҷ����� ȣ��Ǵ� �Լ� ���
        inputNickName.onValueChanged.AddListener(OnValueChanged);
        //inputNickName���� EnterŰ ������ ȣ��Ǵ� �Լ� ���
        inputNickName.onSubmit.AddListener(OnSubmit);
        //inputNickName���� Focusing�� ������� �� ȣ��Ǵ� �Լ� ���
        inputNickName.onEndEdit.AddListener(OnEndEdit);
    }

    void OnValueChanged(string s)
    {        
        //���࿡ s�� ���̰� 0���� ũ��
        //��ư�� �����ϰ� ����
        //�׷��� ������
        //��ư�� �������� �ʰ� ����
        btnConnect.interactable = s.Length > 0;
        print("OnValueChanged : " + s);
    }

    void OnSubmit(string s)
    {
        if(s.Length > 0)
        {
            OnClickConnect();
        }
        print("OnSubmit : " + s);
    }

    void OnEndEdit(string s)
    {
        print("OnEndEdit : " + s);
    }

    public void OnClickConnect()
    {
        //���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� ���� ����)
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� �ִ� ����)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //�� �г��� ����
        PhotonNetwork.NickName = inputNickName.text; //"������_" + Random.Range(1, 1000);
        //�κ� ���� ��û
        PhotonNetwork.JoinLobby();
    }

    //�κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("LobbyScene");
    }


    void Update()
    {
        
    }
}
