//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections;
using UnityEngine;

public class PanelMultiJoin : MonoBehaviour
{
    private int currentPage = 1;
    private float elapsedTime = 10f;
    private string filter = string.Empty;
    private ArrayList filterRoom;
    public GameObject[] items;
    private int totalPage = 1;

    public void connectToIndex(int index, string roomName)
    {
        var num = 0;
        for (num = 0; num < 10; num++)
        {
            items[num].SetActive(false);
        }
        num = (10 * (currentPage - 1)) + index;
        var separator = new char[] { "`"[0] };
        var strArray = roomName.Split(separator);
        if (strArray[5] != string.Empty)
        {
            PanelMultiJoinPWD.Password = strArray[5];
            PanelMultiJoinPWD.roomName = roomName;
            NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().PanelMultiPWD, true);
            NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiROOM, false);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    private string getServerDataString(RoomInfo room)
    {
        var separator = new char[] { "`"[0] };
        var strArray = room.name.Split(separator);
        var objArray1 = new object[] { !(strArray[5] == string.Empty) ? "[PWD]" : string.Empty, strArray[0], "/", strArray[1], "/", strArray[2], "/", strArray[4], " ", room.playerCount, "/", room.maxPlayers };
        return string.Concat(objArray1);
    }

    private void OnDisable()
    {
    }

    private void OnEnable()
    {
        currentPage = 1;
        totalPage = 0;
        refresh();
    }

    private void OnFilterSubmit(string content)
    {
        filter = content;
        updateFilterRooms();
        showlist();
    }

    public void pageDown()
    {
        currentPage++;
        if (currentPage > totalPage)
        {
            currentPage = 1;
        }
        showServerList();
    }

    public void pageUp()
    {
        currentPage--;
        if (currentPage < 1)
        {
            currentPage = totalPage;
        }
        showServerList();
    }

    public void refresh()
    {
        showlist();
    }

    private void showlist()
    {
        if (FengGameManagerMKII.shallRejoin[0] is bool && (bool)FengGameManagerMKII.shallRejoin[0])
        {
            if ((((RoomInfo)FengGameManagerMKII.shallRejoin[2]).maxPlayers == 0 || ((RoomInfo)FengGameManagerMKII.shallRejoin[2]).maxPlayers > ((RoomInfo)FengGameManagerMKII.shallRejoin[2]).playerCount) && ((RoomInfo)FengGameManagerMKII.shallRejoin[2]).open)
            {
                PhotonNetwork.JoinRoom(((RoomInfo)FengGameManagerMKII.shallRejoin[2]).name);
            }
            else
            {
                FengGameManagerMKII.shallRejoin[0] = false;
            }
        }
        if (filter == string.Empty)
        {
            if (PhotonNetwork.GetRoomList().Length > 0)
            {
                totalPage = ((PhotonNetwork.GetRoomList().Length - 1) / 10) + 1;
            }
            else
            {
                totalPage = 1;
            }
        }
        else
        {
            updateFilterRooms();
            if (filterRoom.Count > 0)
            {
                totalPage = ((filterRoom.Count - 1) / 10) + 1;
            }
            else
            {
                totalPage = 1;
            }
        }
        if (currentPage < 1)
        {
            currentPage = totalPage;
        }
        if (currentPage > totalPage)
        {
            currentPage = 1;
        }
        showServerList();
    }

    private void showServerList()
    {
        if (PhotonNetwork.GetRoomList().Length != 0)
        {
            var index = 0;
            if (filter == string.Empty)
            {
                for (index = 0; index < 10; index++)
                {
                    var num2 = (10 * (currentPage - 1)) + index;
                    if (num2 < PhotonNetwork.GetRoomList().Length)
                    {
                        items[index].SetActive(true);
                        items[index].GetComponentInChildren<UILabel>().text = getServerDataString(PhotonNetwork.GetRoomList()[num2]);
                        items[index].GetComponentInChildren<BTN_Connect_To_Server_On_List>().roomName = PhotonNetwork.GetRoomList()[num2].name;
                    }
                    else
                    {
                        items[index].SetActive(false);
                    }
                }
            }
            else
            {
                for (index = 0; index < 10; index++)
                {
                    var num3 = (10 * (currentPage - 1)) + index;
                    if (num3 < filterRoom.Count)
                    {
                        var room = (RoomInfo) filterRoom[num3];
                        items[index].SetActive(true);
                        items[index].GetComponentInChildren<UILabel>().text = getServerDataString(room);
                        items[index].GetComponentInChildren<BTN_Connect_To_Server_On_List>().roomName = room.name;
                    }
                    else
                    {
                        items[index].SetActive(false);
                    }
                }
            }
            GameObject.Find("LabelServerListPage").GetComponent<UILabel>().text = currentPage + "/" + totalPage;
        }
        else
        {
            for (var i = 0; i < items.Length; i++)
            {
                items[i].SetActive(false);
            }
            GameObject.Find("LabelServerListPage").GetComponent<UILabel>().text = currentPage + "/" + totalPage;
        }
    }

    private void Start()
    {
        var index = 0;
        for (index = 0; index < 10; index++)
        {
            items[index].SetActive(true);
            items[index].GetComponentInChildren<UILabel>().text = string.Empty;
            items[index].SetActive(false);
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1f)
        {
            elapsedTime = 0f;
            showlist();
        }
    }

    private void updateFilterRooms()
    {
        filterRoom = new ArrayList();
        if (filter != string.Empty)
        {
            foreach (var info in PhotonNetwork.GetRoomList())
            {
                if (info.name.ToUpper().Contains(filter.ToUpper()))
                {
                    filterRoom.Add(info);
                }
            }
        }
    }
}

