using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PhotonNetworkingScript : MonoBehaviourPunCallbacks
{
	private PhotonView pv;

	[SerializeField] private GameObject fadeOut;
	[SerializeField] private GameObject connectingMenu;
	[SerializeField] private SceneController sceneController;
	[Header("Menu")]
	[SerializeField] private GameObject multiplayerMenu;
	[SerializeField] private TMP_InputField roomNameInput;
	[SerializeField] private TMP_InputField playerNameInput;
	[Header("Room")]
	[SerializeField] private GameObject startGameButton;
	public GameObject roomMenu;
	public TextMeshProUGUI roomText;
	public TextMeshProUGUI roomLog;

	void Start()
	{
		pv = GetComponent<PhotonView>();
		connectingMenu.SetActive(true);
		ConnectToMaster();
	}
	public void ConnectToMaster()
    {
		print("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
	{
		print("Connected to Master");
		PhotonNetwork.JoinLobby();
	}
	public override void OnJoinedLobby()
	{
		print("Joined Lobby");
		connectingMenu.SetActive(false);
		multiplayerMenu.SetActive(true);

		PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
		playerNameInput.text = PhotonNetwork.NickName;
		print($"Connected as {PhotonNetwork.NickName}");		
	}

	public void ChangePlayerName()
    {
		if(playerNameInput.text != string.Empty)
        {
			PhotonNetwork.NickName = playerNameInput.text;
			print($"Name changed to {PhotonNetwork.NickName}");
		}	
	}

    public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(roomNameInput.text);
	}
	public void LeaveRoom()
	{
		print("You left");
		multiplayerMenu.SetActive(true);
		roomMenu.SetActive(false);
		PhotonNetwork.LeaveRoom();
	}
	public void Disconecct()
    {
		PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
		fadeOut.SetActive(true);
		sceneController.LoadScene("Menu");
    }
    public void CreateRoom()
	{
		if (string.IsNullOrEmpty(roomNameInput.text))
		{
			return;
		}
		PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
	}
	public override void OnJoinedRoom()
    {
		print($"Joined room {PhotonNetwork.CurrentRoom.Name}");
		multiplayerMenu.SetActive(false);
		roomMenu.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
			startGameButton.SetActive(true);
        }
        else
        {
			startGameButton.SetActive(false);
		}
		
		roomText.text = PhotonNetwork.CurrentRoom.Name;
		roomLog.text = "";
		roomLog.text += $"\n{PhotonNetwork.NickName} joined.";
	}
	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.LogError("Room Creation Failed: " + message);
	}
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		print($"{otherPlayer.NickName} Left The Game");
		roomLog.text += $"\n{otherPlayer.NickName} left.";
	}
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
		print($"{newPlayer.NickName} Joined The Game");
		roomLog.text += $"\n{newPlayer.NickName} joined.";

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
			roomLog.text += "\n Room is full. Room creator can start the game.";
		}	
	}

    public void SendData(int data)
    {
		pv.RPC("ReceiveData", RpcTarget.Others, data);
		print($"Sent data: {data}");
	}

	[PunRPC]
	public void ReceiveData(int data)
	{
		//gameScript.DisplayTaps(data);
		print($"Recived data: {data}");
	}
}
