using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInputField;
    public TMP_Dropdown roleDropdown;
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

        string selectedRole = roleDropdown.options[roleDropdown.value].text.ToLower(); // "generate sheep" → "generate sheep"

        // NickName & Roleを設定
        PhotonNetwork.NickName = playerName;

        // ロールはCustomPropertiesで他のプレイヤーにも同期される
        Hashtable props = new Hashtable { { "role", selectedRole } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ReceivePlayerName(string name)
    {
        // HTMLから名前を受け取る関数（デフォルトロール: generate sheep）
        PhotonNetwork.NickName = name;
        Hashtable props = new Hashtable { { "role", "generate sheep" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

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



