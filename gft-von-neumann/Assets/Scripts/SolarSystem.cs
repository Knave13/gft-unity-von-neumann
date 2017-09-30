using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {
	public static SolarSystem Instance;
	public Button galaxyViewButton;

	void OnEnable()
	{
		Instance = this;
		galaxyViewButton.interactable = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hit = new RaycastHit();

		if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
		{
			var star = Galaxy.Instance.GetStarByGameObject(hit.transform.gameObject);
			Debug.Log("Clicked on star: " + star.StarName);

			Galaxy.Instance.DestroyGalaxy();
			CreateSolarSystem(star);
		}	
	}

	public void CreateSolarSystem(Star star)
	{
		Random.InitState(Galaxy.Instance.seedNumber);
		var gameObject = SpaceObjects.CreateSphereObject(star.StarName, Vector3.zero, this.transform);

		for (int i = 0; i < star.NumberOfPlanets; i++)
		{
			PlanetaryObject planet = star.PlanetList[i];
			if (planet.PlanetType != "Empty")
			{
				var position = PositionMath.PlanetPosition(i);

				SpaceObjects.CreateSphereObject(planet.PlanetName, position, this.transform);
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

		galaxyViewButton.interactable = false;
	}
}
