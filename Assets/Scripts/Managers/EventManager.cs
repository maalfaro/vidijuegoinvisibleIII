﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManager : Singleton<EventManager>
{
	public static System.Action<List<EventData>, bool> OnEventChanged;
	private List<EventData> activeEvents;

	protected override void Awake()
	{
		base.Awake();
		GameManager.OnGameStart += OnGameStart;
	}

	private void OnGameStart()
	{
		activeEvents = new List<EventData>();
	}

	public void ActiveEvent(EventData eventData)
	{
		if (eventData == null) return;
		if (activeEvents.Contains(eventData)) return;
		if (activeEvents.Count >= 3) return;

		float prob = Random.Range(0f, 1f);
		if (prob < eventData.Probability)
		{
			activeEvents.Add((EventData)eventData.Clone());
		}
		OnEventChanged?.Invoke(activeEvents,true);
	}

	public void ApplyActiveEvents()
	{
		for(int i=0;i<activeEvents.Count;i++) 
		{
			GameManager.Instance.ApplyChoices(activeEvents[i].AttributesEffect);
			activeEvents[i].Duration--;
		}
		RemoveEvents();
	}

	private void RemoveEvents()
	{
		activeEvents.RemoveAll(x => x.Duration <= 0);
		OnEventChanged?.Invoke(activeEvents,false);
	}

}
