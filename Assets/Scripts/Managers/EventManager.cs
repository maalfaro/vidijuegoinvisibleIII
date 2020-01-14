using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManager : Singleton<EventManager>
{
	public static System.Action<List<EventData>> OnEventChanged;
	private List<EventData> activeEvents;

	private void Start()
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
		OnEventChanged?.Invoke(activeEvents);
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
		OnEventChanged?.Invoke(activeEvents);
	}

}
