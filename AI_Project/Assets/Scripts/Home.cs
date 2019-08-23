using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour 
{
	[SerializeField] private float depositedGold;

	public void Deposit(float gold)
	{
		depositedGold += gold;
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Miner"))
			other.GetComponent<MinerAI>().TriggerEvent((int)Events.arrivedAtHome);
	}
}
