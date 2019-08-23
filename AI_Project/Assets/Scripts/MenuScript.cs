using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript 
{
	[MenuItem("Tools/Load mines in GameManager")]
	public static void LoadMines()
	{
		GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		if(gm != null)
			gm.minesInWorld = GameObject.FindGameObjectsWithTag("GoldMine");
		else
			Debug.LogError("GameManager instance is NULL");
	}
}
