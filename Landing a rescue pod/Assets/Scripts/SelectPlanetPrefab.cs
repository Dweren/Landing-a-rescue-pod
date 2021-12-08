using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlanetPrefab : MonoBehaviour
{
    public int selectPlanetID;

    public GameObject selectMenu;
    public Image imageShip;
    public Text textPlanetName;
    public Text textAtmosphere;

    private void Start()
    {
        imageShip.preserveAspect = true;
        imageShip.sprite = ListShips.Instance.listPrefabPlanet[selectPlanetID].GetComponent<SpriteRenderer>().sprite;

        PlanetParameters planetParameters = ListShips.Instance.listPrefabPlanet[selectPlanetID].GetComponent<PlanetParameters>();

        textPlanetName.text = planetParameters.planetName;
        textAtmosphere.text = planetParameters.atmosphere.ToString();
    }

    public void SelectPlanet()
    {
        selectMenu.SetActive(false);
        PodParameters.Instance.SelectPlanetByID(selectPlanetID);
    }
}
