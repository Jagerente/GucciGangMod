//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class OnJoinedInstantiate : MonoBehaviour
{
    public float PositionOffset = 2f;
    public GameObject[] PrefabsToInstantiate;
    public Transform SpawnPosition;

    public void OnJoinedRoom()
    {
        if (PrefabsToInstantiate != null)
        {
            foreach (var obj2 in PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + obj2.name);
                var up = Vector3.up;
                if (SpawnPosition != null)
                {
                    up = SpawnPosition.position;
                }
                var insideUnitSphere = Random.insideUnitSphere;
                insideUnitSphere.y = 0f;
                insideUnitSphere = insideUnitSphere.normalized;
                var position = up + PositionOffset * insideUnitSphere;
                PhotonNetwork.Instantiate(obj2.name, position, Quaternion.identity, 0);
            }
        }
    }
}

