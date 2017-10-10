using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Galaxy : MonoBehaviour {
	public int numberOfStars = 300;
	public int maximumRadius = 100;
	public int maximumHeight = 20;
	public int maximumPlanets = 12;
	public int seedNumber = 100;
	public float mininumProximity = 4f;
	public Dictionary<Star, GameObject> starToObjectMap { get; protected set; }
	public static Galaxy Instance;
	public bool GalaxyView { get; set; }
	public bool PathView { get; set; }
	public Button pathViewButton;
	public GameObject CoursePathPrefab;
	public GameObject CurrentCourse;
	public GameObject SelectionIcon;
	public TextAsset StarNames;
	public string[] PlanetTypes = { "Empty", "Rocky", "Gas Giant", "Asteroids", "Planetoid" };

	private float _defaultStarSize = 1.5f;
	private List<string> availableStarNames;
	void OnEnable()
	{
		Instance = this;
		pathViewButton.interactable = true;
	}

	// Use this for initialization
	void Start () 
	{
		PathView = false;
		CreateSelectionIcon();
		CreateGalaxy();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void DestroyGalaxy()
	{
		while (transform.childCount > 0)
		{
			var gameObject = transform.GetChild(0);
			gameObject.transform.SetParent(null);
			Destroy(gameObject.gameObject);
		}
		pathViewButton.interactable = false;
		GUIManagement.Instance.NamePlates.Clear();
	}

	public void CreateGalaxy()
	{
		Random.InitState(seedNumber);
		CurrentCourse = SpaceObjects.CreateCoursePath(CoursePathPrefab, this.transform);
		CurrentCourse.SetActive(true);
		if (PathView)
		{
			TogglePathView();
		}
		GalaxyView = true;
		starToObjectMap = new Dictionary<Star, GameObject>();
		availableStarNames = TextAssetManager.TextToList(StarNames);
		int failCount = 0;
		for (int i = 0; i < numberOfStars; i++) 
		{
			float distance = Random.Range(0, maximumRadius);
			float angle = Random.Range(0, 2 * Mathf.PI);
			float altitude = Random.Range(-maximumHeight, maximumHeight) * (1 - distance / maximumRadius);

			var position = new Vector3(distance * Mathf.Cos(angle), altitude, distance * Mathf.Sin(angle));

			var positionCollider = Physics.OverlapSphere(position, mininumProximity);

			if (positionCollider.Length == 0)
			{
				var name = availableStarNames[i];
				var starData = new Star(name, Random.Range(0, maximumPlanets), Random.Range(3f, 6f), i % SpaceObjects.StarColors.Length, i % 10);
				var starGameObject = SpaceObjects.CreateSphereObject(starData.StarName, position, _defaultStarSize, this.transform);

				starGameObject.GetComponent<Renderer>().material.color = SpaceObjects.StarColors[starData.ColorIndex];

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
		pathViewButton.interactable = true;
		CurrentCourse.SetActive(false);
	}

	public void TogglePathView()
	{
		var buttonText = pathViewButton.GetComponentInChildren<Text>();
		if (PathView)
		{
			buttonText.text = "View Path";
			PathView = false;
		}
		else
		{
			buttonText.text = "View Path (On)";
			PathView = true;
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
			float size = 0;

			if (random < 5)
			{
				planetType = PlanetTypes[0];
				size = 0.5f;
			}
			else if (random < 45)
			{
				planetType = PlanetTypes[1];
				size = 1.0f;
			}
			else if (random < 75)
			{
				planetType = PlanetTypes[2];
				size = 2.0f;
			}
			else if (random < 90)
			{
				planetType = PlanetTypes[3];
				size = 0.25f;
			}
			else
			{
				planetType = PlanetTypes[4];
				size = 0.5f;
			}

			var planet = new PlanetaryObject(name, planetType, size);

			star.PlanetList.Add(planet);
		}
	}

	void CreateSelectionIcon()
	{
		SelectionIcon = GameObject.Instantiate(SelectionIcon);
		SelectionIcon.transform.localScale = SelectionIcon.transform.localScale * 3.0f;
		SelectionIcon.SetActive(false);
	}

	public void MoveSelectionIcon(RaycastHit hit)
	{
		SelectionIcon.SetActive(true);
		SelectionIcon.transform.position = hit.transform.position;
		//SelectionIcon.transform.rotation = CameraController.CurrentAngle;
		
		var eulerAngles = SelectionIcon.transform.localEulerAngles;
		SelectionIcon.transform.LookAt(Camera.main.transform);
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
