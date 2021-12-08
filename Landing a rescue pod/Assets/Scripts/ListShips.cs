using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListShips : MonoBehaviour
{
    public static ListShips Instance;

    public GameObject[] listPrefabShips;

    public GameObject[] listPrefabPlanet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
