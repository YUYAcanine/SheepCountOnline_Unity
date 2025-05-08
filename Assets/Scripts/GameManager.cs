using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
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
                TryRemoveSheep();
            }
        }
    }

    void TryGenerateSheep()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        PhotonNetwork.Instantiate(sheepPrefab.name, spawnPosition, Quaternion.identity);
    }

    void TryRemoveSheep()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Sheep"))
            {
                PhotonView sheepPhotonView = hit.collider.GetComponent<PhotonView>();
                if (sheepPhotonView != null && sheepPhotonView.IsMine)
                {
                    PhotonNetwork.Destroy(sheepPhotonView.gameObject);
                }
            }
        }
    }
}

