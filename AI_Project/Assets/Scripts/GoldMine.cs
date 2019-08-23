using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour 
{
	[SerializeField] private float gold;

	public bool CanExtractGold(float efficiency)
	{
		if(gold > 0)
		{
			gold -= efficiency;
			return true;
		}
		else
			return false;
	}

	void Update()
	{
		if(gold <= 0)
			gameObject.SetActive(false);
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("Something Collided");

		if(other.CompareTag("Miner") && this.gold > 0)
			other.GetComponent<MinerAI>().TriggerEvent((int)Events.mineHasGold);
	}
}
