﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Galaxy : MonoBehaviour {
	public int numberOfStars = 300;
	public int maximumRadius = 100;
	public int maximumHeight = 100;
	public int maximumPlanets = 12;
	public int seedNumber = 100;
	public float mininumProximity = 4f;
	public Dictionary<Star, GameObject> starToObjectMap { get; protected set; }
	public static Galaxy Instance;
	public string[] PlanetTypes = { "Empty", "Rocky", "Gas Giant", "Asteroids", "Planetoid" };

	void OnEnable()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		CreateGalaxy();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DestroyGalaxy()
	{
		while (transform.childCount > 0)
		{
			var gameObject = transform.GetChild(0);
			gameObject.transform.SetParent(null);
			Destroy(gameObject.gameObject);
		}
	}

	public void CreateGalaxy()
	{
		Random.InitState(seedNumber);
		starToObjectMap = new Dictionary<Star, GameObject>();
		int failCount = 0;
		for (int i = 0; i < numberOfStars; i++) 
		{
			float distance = Random.Range(0, maximumRadius);
			float angle = Random.Range(0, 2 * Mathf.PI);
			float altitude = Random.Range(0, maximumHeight) * (1 - distance / maximumRadius);

			var position = new Vector3(distance * Mathf.Cos(angle), altitude, distance * Mathf.Sin(angle));

			var positionCollider = Physics.OverlapSphere(position, mininumProximity);

			if (positionCollider.Length == 0)
			{
				var name = "Star - " + i.ToString();
				var starData = new Star(name, Random.Range(0, maximumPlanets));
				var starGameObject = SpaceObjects.CreateSphereObject(starData.StarName, position, this.transform);

				starToObjectMap.Add(starData, starGameObject);
				CreatePlanetData(starData);
				
				failCount = 0;
			}
			else
			{
				failCount++;
				i--;
			}
			if (failCount > numberOfStars)
			{
				Debug.Log("Failed to generate star due to proximity validation check");
				break;
			}
		}
	}

	GameObject CreateStarSystem(Star starData, Vector3 position)
	{
		var starGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

		starGameObject.transform.position = position;
		starGameObject.name = starData.StarName;
		starGameObject.transform.SetParent(this.transform);

		return starGameObject;
	}

	void CreatePlanetData(Star star)
	{
		for (int i = 0; i < star.NumberOfPlanets; i++)
		{
			string name = star.StarName + " - " + i.ToString();
			int random = Random.Range(1, 100);
			string planetType = string.Empty;

			if (random < 5)
			{
				planetType = PlanetTypes[0];
			}
			else if (random < 45)
			{
				planetType = PlanetTypes[1];
			}
			else if (random < 75)
			{
				planetType = PlanetTypes[2];
			}
			else if (random < 90)
			{
				planetType = PlanetTypes[3];
			}
			else
			{
				planetType = PlanetTypes[4];
			}

			var planet = new PlanetaryObject(name, planetType);

			star.PlanetList.Add(planet);
		}
	}

	public Star GetStarByGameObject(GameObject gameObject)
	{
		if (starToObjectMap.ContainsValue(gameObject))
		{
			int index = starToObjectMap.Values.ToList().IndexOf(gameObject);
			Star star = starToObjectMap.Keys.ToList()[index];

			return star;
		}
		else
		{
			return null;
		}
	}
}
