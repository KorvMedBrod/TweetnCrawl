﻿using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour 
{
	public Texture2D Image;
	public float x;
	public float y;
	WWW www; 
	
	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x, y, Image.width, Image.height), Image)) {

			print("Play game pressed");
			www = new WWW("www.google.com");
			StartCoroutine(checkConnection());

				}
		
		
	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	IEnumerator checkConnection()
	{
		yield return www;
		
		if(www.error != null)
		{
			print("faild to connect to internet, trying after 2 seconds.");
			yield return new WaitForSeconds(2);// trying again after 2 sec
			StartCoroutine(checkConnection());
		}else
		{
			print("connected to internet");
			Application.LoadLevel("debugscene");
			
		}
		
	}
}

