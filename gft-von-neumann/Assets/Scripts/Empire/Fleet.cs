using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet 
{
	public List<Ship> Ships;
	public string FleetName { get; set; }

	public Fleet(string name, Ship ship)
	{
		this.FleetName = name;
		this.Ships = new List<Ship>();
		this.Ships.Add(ship);
	}

	public void AddShip(Ship ship)
	{
		Ships.Add(ship);
	}

	public void RemoveShip(Ship ship)
	{
		Ships.Remove(ship);
	}
}
