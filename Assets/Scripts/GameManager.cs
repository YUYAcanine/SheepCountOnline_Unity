using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public GameObject sheepPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["role"] as string == "generate sheep")
                TryGenerateSheep();
            else if (PhotonNetwork.LocalPlayer.CustomProperties["role"] as string == "count sheep")
                TryRemoveSheep();
        }
    }

    void TryGenerateSheep()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        PhotonNetwork.Instantiate(sheepPrefab.name, spawnPos, Quaternion.identity);
    }

    void TryRemoveSheep()
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
                    photonView.RPC("DestroySheepRPC", RpcTarget.AllBuffered, sheepView.ViewID);
                }
            }
        }
    }

    [PunRPC]
    public void DestroySheepRPC(int viewID)
    {
        PhotonView target = PhotonView.Find(viewID);
        if (target != null)
        {
            PhotonNetwork.Destroy(target);
        }
    }
}


