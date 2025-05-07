using UnityEngine;
using Photon.Pun;

public class SheepController : MonoBehaviourPun
{
    void OnMouseDown()
    {
        // 誰がタップしてもRPCで削除を全員に指示
        photonView.RPC("DestroySheep", RpcTarget.All);
    }

    [PunRPC]
    void DestroySheep()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}


