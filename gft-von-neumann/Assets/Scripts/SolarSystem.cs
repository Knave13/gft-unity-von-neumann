using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {
	public static SolarSystem Instance;
	public Button galaxyViewButton;
	public Vector3 StarPosition { get; set; }
	public GameObject OrbitSpritePrefab;
	public GameObject OrbitCirclePrefab;

	void OnEnable()
	{
		Instance = this;
		galaxyViewButton.interactable = false;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit = new RaycastHit();

		if (Galaxy.Instance.GalaxyView && Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
		{
			var star = Galaxy.Instance.GetStarByGameObject(hit.transform.gameObject);
			StarPosition = hit.transform.position;
			Debug.Log("Clicked on star: " + star.StarName);

			Galaxy.Instance.DestroyGalaxy();
			CreateSolarSystem(star);
		}	
	}

	public void CreateSolarSystem(Star star)
	{
		CameraController.Instance.ResetCamera();
		Random.InitState(Galaxy.Instance.seedNumber);
		Galaxy.Instance.GalaxyView = false;
		var mainStar = SpaceObjects.CreateSphereObject(star.StarName, Vector3.zero, this.transform);

		for (int i = 0; i < star.NumberOfPlanets; i++)
		{
			PlanetaryObject planet = star.PlanetList[i];
			if (planet.PlanetType != "Empty")
			{
				var position = PositionMath.PlanetPosition(i);

				SpaceObjects.CreateSphereObject(planet.PlanetName, position, this.transform);
				//var orbit = SpaceObjects.CreateOrbitPath(OrbitSpritePrefab, planet.PlanetName + "Orbit", i + 1, this.transform);
				var orbit = SpaceObjects.CreateOrbitRing(OrbitCirclePrefab, "Orbit " + i, i + 1, this.transform);
				var circle = orbit.GetComponent<Circle>();
				circle.SetupCircle();
				Debug.Log("Circle:" + circle.vertexCount + " radius: " + circle.radius);
			}
		}

		galaxyViewButton.interactable = true;
	}

	public void DestroySolarSystem()
	{
		while (transform.childCount > 0)
		{
			var gameObject = transform.GetChild(0);
			gameObject.SetParent(null);
			Destroy(gameObject.gameObject);
		}

		CameraController.Instance.MoveTo(StarPosition);
		galaxyViewButton.interactable = false;
	}
}
