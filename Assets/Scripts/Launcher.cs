using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInputField;
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        string playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player" + Random.Range(1000, 9999);
        }

        PhotonNetwork.NickName = playerName;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room. Loading GameScene.");
        PhotonNetwork.LoadLevel("GameScene");
    }
}


