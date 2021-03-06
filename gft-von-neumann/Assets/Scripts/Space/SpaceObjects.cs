﻿using UnityEngine;
using System.Collections;

public class SpaceObjects {
    public static float[] OrbitDistances = { 0f, 10f, 13f, 16.5f, 20.5f, 25f, 30f, 36f, 43f, 51f, 60f, 70f, 81f, 93f, 106f, 120f, 140f };
    public static Color[] StarColors = { Color.white, Color.blue, Color.green, Color.yellow, new Color(.9f, .2f, 0f), Color.red };
    public static string[] StarColorNames = { "White", "Blue", "Green", "Yellow", "Orange", "Red" };

    // This method creates a sphere object whether that be a planet or star
	public static GameObject CreateSphereObject(string name, Vector3 position, float size, Transform parent = null)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;
        sphere.transform.position = position;
        sphere.transform.parent = parent;
        sphere.transform.localScale = new Vector3(size, size, size);

        CreateNamePlate(sphere);

        return sphere;
    }

    public static GameObject CreateOrbitPath(GameObject orbitSprite, string name, int orbitNumber, Transform parent = null)
    {
        GameObject orbit = GameObject.Instantiate(orbitSprite);
        orbit.name = name;
        orbit.transform.localScale = orbit.transform.localScale * orbitNumber;
        orbit.transform.SetParent(parent);

        return orbit;
    }

    public static GameObject CreateOrbitRing(GameObject circle, string name, int orbitNumber, Transform parent = null)
    {
        GameObject orbit = GameObject.Instantiate(circle);
        orbit.name = name;
        orbit.GetComponent<Circle>().radius = OrbitDistances[orbitNumber];
        //orbit.radius = orbitNumber * 5f;
        orbit.transform.SetParent(parent);

        return orbit;
    }

    public static GameObject CreateCoursePath(GameObject course, Transform parent = null)
    {
        GameObject coursePath = GameObject.Instantiate(course);
        coursePath.transform.SetParent(parent);

        return coursePath;
    }

    public static void AddStarToCoursePath(GameObject course, Vector3 starPosition)
    {
        course.GetComponent<CoursePath>().AddPoint(starPosition);
        if (course.GetComponent<CoursePath>().vertexCount == 2)
        {
            course.SetActive(true);
        }
    }

    public static void CreateNamePlate(GameObject gameObject)
    {
        var namePlateGameObject = new GameObject(gameObject.name + " Name Plate");
        TextMesh namePlate = namePlateGameObject.AddComponent<TextMesh>();
        namePlate.transform.SetParent(gameObject.transform);
        namePlate.text = gameObject.name;
        namePlate.transform.localPosition = new Vector3(0, -1.2f, 0);
        namePlate.anchor = TextAnchor.MiddleCenter;
        namePlate.alignment = TextAlignment.Center;
        namePlate.color = Color.white;
        namePlate.fontSize = 10;

        GUIManagement.Instance.NamePlates.Add(namePlateGameObject);
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
