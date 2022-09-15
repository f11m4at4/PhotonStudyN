using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //�� �̸� InputField
    public InputField inputRoomName;
    //�� �ο� InputField
    public InputField inputMaxPlayer;
    //�� ���� Button
    public Button btnJoin;
    //�� ���� Button
    public Button btnCreate;

    //�� ��� Content
    public Transform roomListContent;

    //����� ����
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //������ ������ Prefab
    public GameObject roomItemFactory;

    void Start()
    {
        //inputRoomName ���� ���Ҷ����� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        //inputMaxPlayer ���� ���Ҷ����� ȣ��Ǵ� �Լ� ���
        inputMaxPlayer.onValueChanged.AddListener(OnMaxPlayerValueChanged);

    }

    void OnRoomNameValueChanged(string room)
    {
        //���� ��ư Ȱ��ȭ
        btnJoin.interactable = room.Length > 0;
        //���� ��ư Ȱ��ȭ
        btnCreate.interactable = room.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    void OnMaxPlayerValueChanged(string max)
    {
        //���� ��ư Ȱ��ȭ
        btnCreate.interactable = max.Length > 0 && inputRoomName.text.Length > 0;
    }



    //�� ����
    public void CreateRoom()
    {
        // �� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        // �ִ� �ο� (0�̸� �ִ��ο�)
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);
        // �� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;

        // �� ���� ��û (�ش� �ɼ��� �̿��ؼ�)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    //���� �����Ǹ� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //�� ������ ���� �ɶ� ȣ�� �Ǵ� �Լ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    //�� ���� ��û
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    //�� ������ �Ϸ� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("GameScene");
    }

    //�� ������ ���� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //�� ����� ����Ǿ��� �� (����, ����, ��������) ȣ�� ���ִ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        //�븮��Ʈ UI ����
        RemoveRoomListUI();
        //�븮��Ʈ ���� ����
        UpdateRoomCache(roomList);
        //�븮��Ʈ UI ����
        CreateRoomListUI();
    }

    void RemoveRoomListUI()
    {
        foreach (Transform tr in roomListContent)
        {
            Destroy(tr.gameObject);
        }

        //Button[] tr = roomListContent.GetComponentsInChildren<Button>();
        //for (int i = 0; i < tr.Length; i++)
        //    Destroy(tr[i].gameObject);
    }

    void UpdateRoomCache(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            //���࿡ roomCache�� info.Name�� �ش�Ǵ� Ű���� �ִٸ�
            if(roomCache.ContainsKey(info.Name))
            {
                //���࿡ �ش� ���� �����Ǵ� ���̶��
                if(info.RemovedFromList)
                {
                    //roomCache ���� ����
                    roomCache.Remove(info.Name);
                }
                //�׷��� �ʴٸ�
                else
                {
                    //��������
                    roomCache[info.Name] = info;
                }
            }
            //�׷��� �ʴٸ�
            else
            {
                //roomCache �� �߰�
                roomCache[info.Name] = info;
            }
        }
    }

    void CreateRoomListUI()
    {
        foreach(RoomInfo info in roomCache.Values)
        {
            //������ ������ �����
            GameObject go = Instantiate(roomItemFactory, roomListContent);

            //������ ���� ����
            RoomItem roomItem = go.GetComponent<RoomItem>();
            roomItem.SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
            roomItem.onClickAction = SelectRoom;

            //���ٽ�
            //roomItem.onClickAction = (string room) => {
            //    inputRoomName.text = room;
            //};
        }
    }

    void SelectRoom(string room)
    {
        inputRoomName.text = room;
    }
}
