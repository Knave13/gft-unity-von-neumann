using System.Collections;
using System.Collections.Generic;

public class Star {
	public string StarName { get; protected set; }
	public float StarSize { get; protected set; }
	public int NumberOfPlanets { get; protected set; }
	public int ColorIndex { get; protected set; }
	public int SpectralIndex { get; protected set; }
	public List<PlanetaryObject> PlanetList;

	public Star(string name, int planets, float size, int color, int spectralIndex)
	{
		this.StarName = name;
		this.NumberOfPlanets = planets;
		this.StarSize = size;
		this.ColorIndex = color;
		this.PlanetList = new List<PlanetaryObject>();
		this.SpectralIndex = spectralIndex;
	}

	public string Details(int indent = 1)
	{
		string indentString = string.Empty;
		for (int i = 0; i < indent; i++)
		{
			indentString += " ";
		}
		string output = "Star Details\r\n"
			+ indentString + "Name: " + this.StarName + "\r\n"
			+ indentString + "Size: " + this.StarSize + "\r\n"
			+ indentString + "Orbits: " + this.NumberOfPlanets + "\r\n"
			+ indentString + "Color: " + SpaceObjects.StarColorNames[this.ColorIndex] + " " + this.SpectralIndex + "\r\n";

		return output;
	}
}
