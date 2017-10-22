using System.Collections;
using System.Collections.Generic;

public class PlanetaryObject
{
	public string PlanetName;
	public string PlanetType;
	public float PlanetSize;

	public PlanetaryObject(string name, string planetType, float size)
	{
		this.PlanetName = name;
		this.PlanetType = planetType;
		this.PlanetSize = size;;
	}
}
