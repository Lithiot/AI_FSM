using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States
{
	Idle = 0, GoMinning, Minning, GoBack, DropGold, Count
}
enum Events
{
	hasNoGold = 0, mineHasGold, mineIsEmpty, timeToReturn, arrivedAtHome, droppingFinished, Count
}

public class MinerAI : MonoBehaviour 
{
	private FiniteStateMachine FSM;
	private States currentState;
	private GameObject home;
	[SerializeField]private float maxCapacity;
	[SerializeField] private float speed;
	[SerializeField] private float efficiency;
	[SerializeField] private float gold;
	private GameObject currentMine;

	private void Start()
	{
		FSM = new FiniteStateMachine((int)States.Count, (int)Events.Count);

		FSM.RegisterTransition((int)States.Idle, (int)Events.hasNoGold, (int)States.GoMinning);
		FSM.RegisterTransition((int)States.GoMinning, (int)Events.mineHasGold, (int)States.Minning);
		FSM.RegisterTransition((int)States.Minning, (int)Events.mineIsEmpty, (int)States.Idle);
		FSM.RegisterTransition((int)States.Minning, (int)Events.timeToReturn, (int)States.GoBack);
		FSM.RegisterTransition((int)States.GoBack, (int)Events.arrivedAtHome, (int)States.DropGold);
		FSM.RegisterTransition((int)States.DropGold, (int)Events.droppingFinished, (int)States.Idle);

		home = GameManager.instance.home;
		currentState = States.Idle;
		gold = 0;
		currentMine = null;
	}

	private void Update()
	{
		switch (currentState)
		{
			case States.Idle:
				Debug.Log("State is: Idle");
				if(gold == 0) TriggerEvent((int)Events.hasNoGold);
				break;

			case States.GoMinning:
				Debug.Log("State is: GoMinning");
				
				if(!currentMine || !currentMine.activeInHierarchy)
				{
					currentMine = GameManager.instance.GetActiveGoldMine();
					if(!currentMine)
						TriggerEvent((int)Events.mineIsEmpty);
				}
				else
					transform.position = Vector3.Lerp(transform.position, currentMine.transform.position, speed * Time.deltaTime);
				break;

			case States.Minning:
				Debug.Log("State is: Minning");
				if(gold < maxCapacity && currentMine.GetComponent<GoldMine>().CanExtractGold(efficiency))
					gold += efficiency;
				else TriggerEvent((int)Events.timeToReturn);
				break;

			case States.GoBack:
				Debug.Log("State is: GoBack");
				if(home)
					transform.position = Vector3.Lerp(transform.position, home.transform.position, speed * Time.deltaTime);
				else
					home = GameManager.instance.home;
				break;

			case States.DropGold:
				Debug.Log("State is: DropGold");
				home.GetComponent<Home>().Deposit(gold);
				gold = 0;
				TriggerEvent((int)Events.droppingFinished);
				break;
		}
	}

	public void TriggerEvent(int eventNumber)
	{
		if(FSM.CheckTransition((int)currentState, eventNumber) != -1)
			currentState = (States)FSM.CheckTransition((int)currentState, eventNumber);
	}
}
