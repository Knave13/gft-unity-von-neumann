using UnityEngine;
using System.Collections;

public class SpaceObjects {

    // This method creates a sphere object whether that be a planet or star
	public static GameObject CreateSphereObject(string name, Vector3 position, float size, Transform parent = null)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;
        sphere.transform.position = position;
        sphere.transform.parent = parent;
        sphere.transform.localScale = new Vector3(size, size, size);

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
        orbit.GetComponent<Circle>().radius = orbitNumber * 5f;
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
    }

    /*
    Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
    Removing this comment forfits any rights given to the user under licensing.
    */

}
