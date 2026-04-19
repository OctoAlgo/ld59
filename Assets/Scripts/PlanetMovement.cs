using System;
using TMPro;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{

public Transform orbitPivot;
public Material lineMaterial;
public float orbitSpeed = 10f;

public bool isHUDMovement;

public LineRenderer lineRenderer;

public bool isSelected = false;

public Planet planetData;

public GameObject selectionIndicator;
public TMPro.TextMeshProUGUI planetText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(selectionIndicator != null)
        selectionIndicator.SetActive(false);

        if(GetComponent<Canvas>() != null)
        {
            GetComponent<Canvas>().worldCamera = PlanetsManager.Instance.planetCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitPivot != null)
        {
            transform.RotateAround(orbitPivot.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }

    }

    public void UpdatePlanetInfo()
    {
        switch (planetData.discovered)
        {
            case true:
            planetText.text = $"Planet: {planetData.name}{Environment.NewLine}Love Interest: {planetData.loveInterest.GetFullName()}";
            break;

            case false:
            planetText.text = $"Unknown planet!{Environment.NewLine}What cuties might await?";
            break;
        }
    }

    public void Init()
    {
        this.orbitPivot = PlanetsManager.Instance.sun.transform;
        this.GetComponent<MeshRenderer>().material.color = planetData.color;
        this.orbitSpeed = planetData.orbitSpeed;
        this.transform.localScale = new Vector3(planetData.size, planetData.size, planetData.size);

    }

    public void SignalPlanet()
    {
        Debug.Log($"Signaling planet with alien: {planetData.loveInterest.firstName} {planetData.loveInterest.lastName}");
        planetData.discovered = true;
        PlanetsManager.Instance.SignalPlanet(planetData.loveInterest);
    }

}
