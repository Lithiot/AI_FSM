using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour 
{
	int[,] stateMatrix;

	public FiniteStateMachine(int statesCount, int eventsCount)
	{
		stateMatrix = new int[statesCount, eventsCount];

		for(int i = 0; i < statesCount; i++)
		{
			for(int j = 0; j < eventsCount; j++)
			{
				stateMatrix[i,j] = -1;
			}
		}
	}

	public void RegisterTransition(int sourceState, int eventNumber, int newState)
	{	
		stateMatrix[sourceState, eventNumber] = newState;
	}

	public int CheckTransition(int state, int eventNumber)
	{
		if(stateMatrix[state, eventNumber] != -1)
			return stateMatrix[state, eventNumber];

		return -1;
	}
}
