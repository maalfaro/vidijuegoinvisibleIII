using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventsUI : MonoBehaviour
{

	[SerializeField] private List<Text> activeEventsText;


	private void Awake()
	{
		GameManager.OnGameStart += OnGameStart;
		EventManager.OnEventChanged += OnEventChanged;
	}

	private void OnGameStart()
	{
		activeEventsText.ForEach(t => t.text=string.Empty);
	}

	private void OnEventChanged(List<EventData> activeEvents)
	{
		for(int i=0;i< activeEventsText.Count; i++)
		{
			if(i< activeEvents.Count)
			{
				activeEventsText[i].text = activeEvents[i].Description;
			}
			else
			{
				activeEventsText[i].text = string.Empty;
			}
		}
	}

}
