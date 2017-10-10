using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HttpRequest //: MonoBehaviour 
{
    public string url = "http://localhost:3000/users";
    // IEnumerator Start()
    // {
    //     WWW www = new WWW(url);
    //     yield return www;
	// 	Debug.Log("ReSTful Result: " + www.text);
    // }

	public IEnumerator GetData(string parameter)
	{
		WWW www = new WWW(url + "/" + parameter);
		yield return www;
	}

	public string GetUser(int index)
	{
		var result = GetData(string.Empty);
		var data = string.Empty;
		Debug.Log(result.Current);
		while (result.MoveNext())
		{
			Debug.Log(result.Current);
			//data += result.Current.ToString();
		}
		return data;
	}
}
