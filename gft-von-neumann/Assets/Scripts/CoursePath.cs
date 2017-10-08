using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CoursePath : MonoBehaviour {
	public int vertexCount = 0;
	public float lineWidth = 0.2f;

	private Color _defaultColor = Color.cyan;
	private LineRenderer lineRenderer;
	
	private void Awake()
	{
		Debug.Log("CoursePath Awake Called");
		lineRenderer = GetComponent<LineRenderer>();
	}

	// Use this for initialization
	void Start () {
		Debug.Log("CoursePath Start Called");
		lineRenderer.widthMultiplier = lineWidth;
		lineRenderer.loop = false;
		SetColor(_defaultColor, _defaultColor);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetupCoursePath(Vector3[] course)
	{
		int count = course.Length;
		lineRenderer.positionCount = count;
		for (int i = 0; i < count; i++)
		{
			lineRenderer.SetPosition(i, course[i]);
		}
	}

	public void AddPoint(Vector3 pt)
	{
		vertexCount++;
		lineRenderer.positionCount = vertexCount;
		lineRenderer.loop = false;
		lineRenderer.SetPosition(lineRenderer.positionCount - 1, pt);
	}

	public void SetColor(Color c1, Color c2)
	{
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startColor = c1;
		lineRenderer.endColor = c2;
	}
}
