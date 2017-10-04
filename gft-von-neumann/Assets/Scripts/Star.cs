﻿using System.Collections;
using System.Collections.Generic;

public class Star {
	public string StarName { get; protected set; }
	public float StarSize { get; protected set; }
	public int NumberOfPlanets { get; protected set; }
	public List<PlanetaryObject> PlanetList;

	public Star(string name, int planets, float size)
	{
		this.StarName = name;
		this.NumberOfPlanets = planets;
		this.StarSize = size;
		this.PlanetList = new List<PlanetaryObject>();
	}
}
