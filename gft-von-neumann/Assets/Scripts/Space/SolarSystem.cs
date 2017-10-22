using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {
	public static SolarSystem Instance;
	public Button galaxyViewButton;
	public Button pathViewButton;
	public Vector3 StarPosition { get; set; }
	public Text SystemDetailsText;
	public GameObject OrbitSpritePrefab;
	public GameObject OrbitCirclePrefab;

	void OnEnable()
	{
		Instance = this;
		galaxyViewButton.interactable = false;
		pathViewButton.interactable = false;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit = new RaycastHit();

		if (Galaxy.Instance.PathView && Galaxy.Instance.GalaxyView && 
			Physics.Raycast(mouseRay, out hit))
		{
			Galaxy.Instance.MoveSelectionIcon(hit);
			if (Input.GetMouseButtonDown(0))
			{
				var star = Galaxy.Instance.GetStarByGameObject(hit.transform.gameObject);
				StarPosition = hit.transform.position;
				SpaceObjects.AddStarToCoursePath(Galaxy.Instance.CurrentCourse, hit.transform.gameObject.transform.position);
			}
		}
		else if (Galaxy.Instance.GalaxyView && 
				 Physics.Raycast(mouseRay, out hit))
		{
			Galaxy.Instance.MoveSelectionIcon(hit);
			if (Input.GetMouseButtonDown(0))
			{
				var star = Galaxy.Instance.GetStarByGameObject(hit.transform.gameObject);
				StarPosition = hit.transform.position;

				Galaxy.Instance.DestroyGalaxy();
				CreateSolarSystem(star);
			}
		}
		else
		{
			Galaxy.Instance.SelectionIcon.SetActive(false);
		}
	}

	public void CreateSolarSystem(Star star)
	{
		CameraController.Instance.ResetCamera();
		Random.InitState(Galaxy.Instance.seedNumber);
		Galaxy.Instance.GalaxyView = false;
		Galaxy.Instance.PathView = false;
		var mainStar = SpaceObjects.CreateSphereObject(star.StarName, Vector3.zero, star.StarSize, this.transform);
		
		mainStar.GetComponent<Renderer>().material.color = SpaceObjects.StarColors[star.ColorIndex];
		for (int i = 0; i < star.NumberOfPlanets; i++)
		{
			PlanetaryObject planet = star.PlanetList[i];
			if (planet.PlanetType != "Empty")
			{
				var position = PositionMath.PlanetPosition(i);

				SpaceObjects.CreateSphereObject(planet.PlanetName, position, planet.PlanetSize, this.transform);
				//var orbit = SpaceObjects.CreateOrbitPath(OrbitSpritePrefab, planet.PlanetName + "Orbit", i + 1, this.transform);
				var orbit = SpaceObjects.CreateOrbitRing(OrbitCirclePrefab, "Orbit " + i, i + 1, this.transform);
				var circle = orbit.GetComponent<Circle>();
				circle.SetupCircle();
				if (i < 3) 
				{
					circle.SetColor(Color.yellow, Color.yellow);
				}
				else if (i == 3)
				{
					circle.SetColor(Color.green, Color.green);
				}
				else
				{
					circle.SetColor(Color.blue, Color.blue);
				}
			}
		}

		galaxyViewButton.interactable = true;
		pathViewButton.interactable = false;
		SystemDetailsText.text = star.Details(2);
	}

	public void DestroySolarSystem()
	{
		while (transform.childCount > 0)
		{
			var gameObject = transform.GetChild(0);
			gameObject.SetParent(null);
			Destroy(gameObject.gameObject);
		}

		//CameraController.Instance.MoveTo(StarPosition);
		galaxyViewButton.interactable = false;
		pathViewButton.interactable = true;
		SystemDetailsText.text = string.Empty;
		GUIManagement.Instance.NamePlates.Clear();
	}
}
