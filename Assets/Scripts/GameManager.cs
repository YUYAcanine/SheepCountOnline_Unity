using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public GameObject sheepPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string role = PhotonNetwork.LocalPlayer.CustomProperties["role"] as string;

            if (role == "generate sheep")
            {
                TryGenerateSheep();
            }
            else if (role == "count sheep")
            {
                TryRequestRemoveSheep();
            }
        }
    }

    void TryGenerateSheep()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        PhotonNetwork.Instantiate(sheepPrefab.name, spawnPos, Quaternion.identity);
    }

    void TryRequestRemoveSheep()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Sheep"))
            {
                PhotonView sheepView = hit.collider.GetComponent<PhotonView>();
                if (sheepView != null)
                {
                    // MasterClientに削除を依頼（誰でも呼べる）
                    photonView.RPC("RequestMasterDestroy", RpcTarget.MasterClient, sheepView.ViewID);
                }
            }
        }
    }

    [PunRPC]
    public void RequestMasterDestroy(int viewID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView target = PhotonView.Find(viewID);
            if (target != null)
            {
                PhotonNetwork.Destroy(target);
            }
        }
    }
}



