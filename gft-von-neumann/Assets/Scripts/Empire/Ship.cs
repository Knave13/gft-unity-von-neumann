using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship 
{
	public string ShipName { get; set; }
	public int HullPoints { get; set; }
	public int ShieldPoints { get; set; }

	public int MaxHullPoints { get; set; }
	public int MaxShieldPoints { get; set; }

	public Ship(string name, int maxHull, int maxShields)
	{
		this.ShipName = name;
		this.MaxHullPoints = maxHull;
		this.MaxShieldPoints = maxShields;
		this.HullPoints = maxHull;
		this.ShieldPoints = maxShields;
	}
}
