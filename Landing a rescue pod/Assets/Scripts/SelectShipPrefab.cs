using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShipPrefab : MonoBehaviour
{
    public int selectShipID;

    public GameObject selectMenu;
    public Image imageShip;
    public Text textShipName;
    public Text textShipEnginePower;
    public Text textShipFuel;
    public Text textShipWeight;

    private void Start()
    {
        imageShip.preserveAspect = true;
        imageShip.sprite = ListShips.Instance.listPrefabShips[selectShipID].GetComponent<SpriteRenderer>().sprite;

        RescuePod rescuePod = ListShips.Instance.listPrefabShips[selectShipID].GetComponent<RescuePod>();

        textShipName.text = rescuePod.shipName;
        textShipEnginePower.text = rescuePod.shipEnginePower.ToString();
        textShipFuel.text = rescuePod.fuelLevelMax.ToString();
        textShipWeight.text = rescuePod.shipWeight.ToString();
    }

    public void SelectShip()
    {
        selectMenu.SetActive(false);
        PodParameters.Instance.SelectShipByID(selectShipID);
    }
}
