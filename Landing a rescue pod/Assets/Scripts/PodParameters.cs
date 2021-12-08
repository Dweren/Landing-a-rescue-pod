using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PodParameters : MonoBehaviour
{
    public static PodParameters Instance;

    public GameObject rescuePod;
    private Rigidbody2D rb;

    public GameObject noticeLanding;
    public Text textNoticeQuality;

    public GameObject sliderEnginePower;

    public Text textVelocityRescuePod;
    public Text textVelocityRescuePodAlternate;

    [HideInInspector]
    public Vector2 velocityRescuePod;

    public float enginePower = 0f;
    private float goodLandingSpeed = -0.5f;
    private float perfectLandingSpeed = -0.1f;

    public Slider sliderFuelLevel;
    public Text fuelLevelInNumber;
    public float fuelLevelMax;
    public float fuelLevel;

    public GameObject groundLanding;
    public Text textDistanceToLanding;

    public bool isAlternateParameters = false;

    public float planetAtmosphere = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (rescuePod)
        {
            velocityRescuePod = rb.velocity;

            textVelocityRescuePod.text = velocityRescuePod.y.ToString("00.00");

            fuelLevel -= enginePower / 100;

            if (fuelLevel < 0)
            {
                fuelLevel = 0;
                sliderEnginePower.GetComponent<Slider>().value = 0;
                sliderEnginePower.GetComponent<Slider>().interactable = false;
            }

            fuelLevelInNumber.text = fuelLevel.ToString("0.00");
            sliderFuelLevel.value = fuelLevel;

            if (rescuePod.GetComponent<RescuePod>().isLanded == false)
            {
                Vector2 temp = new Vector2(0f, enginePower);
                rb.AddForce(temp, ForceMode2D.Force);
            }

            if (rescuePod.GetComponent<RescuePod>().isLanded == true)
            {
                sliderEnginePower.GetComponent<Slider>().value = 0;
                sliderEnginePower.GetComponent<Slider>().interactable = false;

                if (rescuePod.GetComponent<RescuePod>().velocityY < goodLandingSpeed)
                {
                    textNoticeQuality.text = "You Crashed!";
                    noticeLanding.SetActive(true);
                }

                if (rescuePod.GetComponent<RescuePod>().velocityY >= goodLandingSpeed && rescuePod.GetComponent<RescuePod>().velocityY < perfectLandingSpeed)
                {
                    textNoticeQuality.text = "Good Landing!";
                    noticeLanding.SetActive(true);
                }

                if (rescuePod.GetComponent<RescuePod>().velocityY >= perfectLandingSpeed)
                {
                    textNoticeQuality.text = "Perfect Landing!";
                    noticeLanding.SetActive(true);
                }
            }
        }
    }

    public void ChangeEnginePower(Slider slider)
    {
        enginePower = (float)slider.value;
    }

    public void SelectShipByID(int shipID)
    {
        if (rescuePod)
        {
            Destroy(rescuePod);
        }

        RescuePod scriptRescuePod = ListShips.Instance.listPrefabShips[shipID].GetComponent<RescuePod>();

        sliderEnginePower.GetComponent<Slider>().maxValue = scriptRescuePod.shipEnginePower;
        sliderFuelLevel.maxValue = scriptRescuePod.fuelLevelMax;
        sliderFuelLevel.value = scriptRescuePod.fuelLevelMax;

        fuelLevel = scriptRescuePod.fuelLevelMax;
        fuelLevelMax = scriptRescuePod.fuelLevelMax;

        scriptRescuePod.groundLanding = groundLanding;
        scriptRescuePod.textDistanceToLanding = textDistanceToLanding;

        rescuePod = Instantiate(ListShips.Instance.listPrefabShips[shipID], new Vector2(0f, 4f), Quaternion.identity);
        rb = rescuePod.GetComponent<Rigidbody2D>();

        if (isAlternateParameters)
        {
            rescuePod.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            rescuePod.transform.GetChild(0).gameObject.SetActive(false);
        }

        RestartLanding();
    }

    public void SelectPlanetByID(int planetID)
    {
        planetAtmosphere = ListShips.Instance.listPrefabPlanet[planetID].GetComponent<PlanetParameters>().atmosphere;
        if (rescuePod)
        {
            rescuePod.GetComponent<Rigidbody2D>().drag = planetAtmosphere;
        }
    }

    public void RestartLanding()
    {
        rescuePod.GetComponent<RescuePod>().LandedRestart();
        noticeLanding.SetActive(false);
        sliderEnginePower.GetComponent<Slider>().value = 0;
        sliderEnginePower.GetComponent<Slider>().interactable = true;
        rescuePod.transform.position = new Vector2(0f, 4f);
        fuelLevel = fuelLevelMax;;
    }

    public void OnAlternateParameters()
    {
        isAlternateParameters = true;

        if (rescuePod)
        {
            rescuePod.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    public void OffAlternateParameters()
    {
        isAlternateParameters = false;

        if (rescuePod)
        {
            rescuePod.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
