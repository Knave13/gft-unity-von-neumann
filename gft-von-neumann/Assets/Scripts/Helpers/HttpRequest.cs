﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LitJson;
using System.Net;
using System.IO;

public class HttpRequest //: MonoBehaviour 
{
    public string url = "http://localhost:3000/users";
	private JsonData _itemData;
	private string _username = "test@test.com";
	private string _password = "test1313";
	private string firestoreUrl = "https://firestore.googleapis.com/v1beta1/projects/my-firebase-app-2a731/databases/(default)/documents";
	private string firestoreKey = "AIzaSyB01keRrMpnj6CpdhFPotNhjN8mei8JYvU";

	public IEnumerator GetData(string parameter)
	{
		WWW www = new WWW(url + "/" + parameter);
		
		yield return www;

		_itemData = JsonMapper.ToObject(www.text);
		Debug.Log("Got to json processing");
		Debug.Log("www.text: " + www.text);
		Debug.Log("json to string: " + _itemData["galaxies"].ToString());

	}

	public string GetUser(int index)
	{
		var result = GetData(string.Empty);
		//var data = string.Empty;

		List<object> myList = result as List<object>;
		Debug.Log(myList[0]);

		return _itemData.ToString();
	}

	public string HttpGet(string url)
    {
        System.Net.HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
		req.Credentials = new NetworkCredential(_username, _password);

        string result = null;
        using (HttpWebResponse resp = req.GetResponse()
                                      as HttpWebResponse)
        {
            StreamReader reader =
                new StreamReader(resp.GetResponseStream());
            result = reader.ReadToEnd();
        }
        return result;
    }

	public string GetJson(string url)
	{
		_itemData = JsonMapper.ToObject(HttpGet(url));

		return _itemData["galaxies"][0]["name"].ToString();
	}

	public string GetGalaxy(string id)
	{
		// https://firestore.googleapis.com/v1beta1/projects/my-firebase-app-2a731/databases/(default)/documents/galaxies/Fqcxy7aKiISZmUMICPFE?key=AIzaSyB01keRrMpnj6CpdhFPotNhjN8mei8JYvU
		// https://firestore.googleapis.com/v1beta1/projects/my-firebase-app-2a731/databases/(default)/documents/galaxies/Fqcxy7aKiISZmUMICPFE?key=AIzaSyB01keRrMpnj6CpdhFPotNhjN8mei8JYvU
		string url = string.Concat(firestoreUrl + "/galaxies/" + id + "?key=" + firestoreKey);
		Debug.Log (url);
		var data = JsonMapper.ToObject (HttpGet (url));
		Debug.Log (data.ToJson ());

		return data ["fields"] ["name"].ToString();
	}
}
