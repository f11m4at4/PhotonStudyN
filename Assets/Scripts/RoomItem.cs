using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    //������ Text
    public Text roomInfo;
    //�漳�� Text
    public Text roomDescription;

    //Ŭ�� �Ǿ��� �� ȣ��Ǿ� �ϴ� �Լ��� ���� ����
    public Action<string, int> onClickAction;

    //map Id
    int mapId;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, byte maxPlayer)
    {
        //�ڽ��� ���ӿ�����Ʈ �̸��� roomName����
        name = roomName;
        //������ ���� => �� �̸� ( 1 / 10) 
        roomInfo.text = roomName + " (" + currPlayer + " / " + maxPlayer + ")";
    }

    public void SetInfo(RoomInfo info)
    {
        SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);

        //�� ���� ����
        roomDescription.text = (string)info.CustomProperties["description"];

        //�� id ����
        mapId = (int)info.CustomProperties["mapId"];
    }



    public void OnClick()
    {
        //���࿡ onClickAction�� null�� �ƴ϶��
        if(onClickAction != null)
        {
            //onClickAction �� ����ִ� �Լ��� ȣ��
            onClickAction(name, mapId);
        }

        ////InputRoomName �� roomName�� �����ؼ� ����
        ////1. InputRoomName ���ӿ�����Ʈ ã��
        //GameObject ��ǲ����� = GameObject.Find("InputRoomName"); 
        ////2. ã�� ���� ������Ʈ���� InputField ������Ʈ ��������
        //InputField input = ��ǲ�����.GetComponent<InputField>();
        ////3. ������ ������Ʈ���� text�� roomName���� ����
        //input.text = name;
    }
}
