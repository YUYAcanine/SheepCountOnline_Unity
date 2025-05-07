using UnityEngine;
using Photon.Pun;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // クリックまたはタップ
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0; // 2DなのでZは固定

            PhotonNetwork.Instantiate(sheepPrefab.name, clickPosition, Quaternion.identity);
        }
    }
}

