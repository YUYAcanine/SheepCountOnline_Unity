using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListDisplay : MonoBehaviourPunCallbacks
{
    public TMP_Text playerListText;

    void Start()
    {
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        string list = "Players in Room:\n";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            list += "- " + p.NickName + "\n";
        }

        playerListText.text = list;
    }
}

