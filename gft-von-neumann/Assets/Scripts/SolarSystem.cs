using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem : MonoBehaviour {
	public static SolarSystem Instance;

	void OnEnable()
	{
		Instance = this;
		galaxyViewButton.intractable = false;
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
		// var gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		// gameObject.transform.position = Vector3.zero;
		// gameObject.name = star.StarName;
		// gameObject.transform.SetParent(this.transform);

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

		galaxyViewButton.intractable = true;
	}
}
