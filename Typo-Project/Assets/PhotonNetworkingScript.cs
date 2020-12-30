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
	[SerializeField] private MultiplayerGameController gameController;
	[Header("Menu")]
	[SerializeField] private TextMeshProUGUI errorText;
	[SerializeField] private GameObject multiplayerMenu;
	[SerializeField] private TMP_InputField roomNameInput;
	[SerializeField] private TMP_InputField playerNameInput;
	[Header("Room")]
	[SerializeField] private GameObject startGameButton;
	[SerializeField] private TMP_InputField chatInput;
	[SerializeField] private TextMeshProUGUI playersText;
	public GameObject roomMenu;
	public TextMeshProUGUI roomText;
	public TextMeshProUGUI roomLog;

	void Start()
	{
		gameController = GetComponent<MultiplayerGameController>();
		pv = GetComponent<PhotonView>();
		connectingMenu.SetActive(true);
		ConnectToMaster();
	}
	public void ConnectToMaster()
    {
		//Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
	{
		//print("Connected to Master");
		PhotonNetwork.JoinLobby();
	}
	public override void OnJoinedLobby()
	{
		//print("Joined Lobby");
		connectingMenu.SetActive(false);
		multiplayerMenu.SetActive(true);

        if (PlayerPrefs.HasKey("PlayerName"))
        {
			PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
		}
        else
        {
			PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
		}
	
		playerNameInput.text = PhotonNetwork.NickName;
		//print($"Connected as {PhotonNetwork.NickName}");		
	}

	public void ChangePlayerName()
    {
		if(playerNameInput.text != string.Empty)
        {
			PhotonNetwork.NickName = playerNameInput.text;
			PlayerPrefs.SetString("PlayerName", PhotonNetwork.NickName);
			//print($"Name changed to {PhotonNetwork.NickName}");
		}	
	}

    public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(roomNameInput.text);
	}
	public void LeaveRoom()
	{
		//print("You left");
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
		PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
	}
	public override void OnJoinedRoom()
    {
		//print($"Joined room {PhotonNetwork.CurrentRoom.Name}");
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
		playersText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
	}
	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = message;
	}
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		errorText.text = message;
	}
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
        if (gameController.gameStarted)
        {
			gameController.Win();
		}
		roomLog.text += $"\n{otherPlayer.NickName} left.";
		playersText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
	}
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(newMasterClient == PhotonNetwork.LocalPlayer)
        {
			startGameButton.SetActive(true);
		}
		roomLog.text += $"\n{newMasterClient.NickName} is the host.";
	}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
		roomLog.text += $"\n{newPlayer.NickName} joined.";
		playersText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";

		if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
			roomLog.text += "\n\n Room is full. Room creator can start the game.\n";
		}	
	}
	



	public void Chat()
    {
		if(chatInput.text != "")
        {
			string message = $"\n<{PhotonNetwork.NickName}> {chatInput.text}";

			roomLog.text += message;
			SendDataChat(message);

			chatInput.text = "";
		}
    }

	public void SendDataChat(string msg)
	{
		pv.RPC("ReceiveChat", RpcTarget.Others, msg);
	}
	public void SendDataDamage(int value)
    {
		pv.RPC("ReceiveDamage", RpcTarget.Others, value);
		gameController.DamageEnemy(value);
		//print($"Sent damage: {value}");
	}
	public void SendStartGameSignal()
	{
		if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
		{
			pv.RPC("ReceiveGameStart", RpcTarget.All);
			//print($"Sent signal: start game");
		}
        else
        {
			roomLog.text += "\n Not enough players.";
        }
	}

	[PunRPC]
	public void ReceiveDamage(int data)
	{
		//print($"Recived Damage: {data}");
		gameController.Damage(data);
	}
	[PunRPC]
	public void ReceiveGameStart()
	{
		//print($"Recived signal: start game");
		gameController.StartTheGame();
	}
	[PunRPC]
	public void ReceiveChat(string msg)
	{
		roomLog.text += msg;
	}
}
