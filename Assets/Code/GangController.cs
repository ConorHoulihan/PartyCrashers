using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangController : MonoBehaviour
{
    [SerializeField]
    List<Transform> NPCs;

    void Start()
    {
        foreach (Transform child in transform)
        {
            NPCs.Add(child);
        }
        NPCs[Random.Range(0, NPCs.Count)].GetComponent<NPCPatrol>().BecomeHider();
    }
}
