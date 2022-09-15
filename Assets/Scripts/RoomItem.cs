using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoomItem : MonoBehaviour
{
    //������ Text
    public Text roomInfo;
    //Ŭ�� �Ǿ��� �� ȣ��Ǿ� �ϴ� �Լ��� ���� ����
    public Action<string> onClickAction;

    
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

    public void OnClick()
    {
        //���࿡ onClickAction�� null�� �ƴ϶��
        if(onClickAction != null)
        {
            //onClickAction �� ����ִ� �Լ��� ȣ��
            onClickAction(name);
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
