using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescuePod : MonoBehaviour
{
    [Header("Parameters")]
    public string shipName;
    public float shipEnginePower;
    public float fuelLevelMax;
    public float shipWeight;

    public static RescuePod Instance;

    public bool isLanded = false;

    public Rigidbody2D rb;

    public float velocityY;

    public GameObject groundLanding;
    public float distanceToLanding;
    public Text textDistanceToLanding;

    public Text textDistanceToLandingAlternate;
    public Text textVelocityRescuePodAlternate;

    public Slider sliderFuelLevel;
    public Text fuelLevelInNumber;

    public GameObject jetOfFlame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = shipWeight;
        rb.drag = PodParameters.Instance.planetAtmosphere;

        sliderFuelLevel.maxValue = fuelLevelMax;
        sliderFuelLevel.value = fuelLevelMax;
    }

    private void Update()
    {
        distanceToLanding = Mathf.Abs(transform.position.y - groundLanding.transform.position.y);
        float distanceForText = distanceToLanding - 0.7f;
        textDistanceToLanding.text = distanceForText.ToString("0.00");

        //Parameters Alternate
        textDistanceToLandingAlternate.text = distanceToLanding.ToString("0.00");
        textVelocityRescuePodAlternate.text = rb.velocity.y.ToString("00.00");
        sliderFuelLevel.value = PodParameters.Instance.fuelLevel;
        fuelLevelInNumber.text = PodParameters.Instance.fuelLevel.ToString("0.00");

        if(PodParameters.Instance.enginePower > 0)
        {
            jetOfFlame.SetActive(true);
        }
        else
        {
            jetOfFlame.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isLanded = true;
        velocityY = rb.velocity.y;
    }

    public void LandedRestart()
    {
        isLanded = false;
    }
}
