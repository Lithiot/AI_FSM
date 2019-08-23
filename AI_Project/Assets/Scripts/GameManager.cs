using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;
	public GameObject[] minesInWorld;

	public GameObject home;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public GameObject GetActiveGoldMine()
	{
		for (int i = 0; i < minesInWorld.Length; i++)
		{
			if(minesInWorld[i].activeInHierarchy)
				return minesInWorld[i];
		}
		return null;
	}

}
