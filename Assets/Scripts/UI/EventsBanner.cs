using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EventsBanner : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI bannerText;

	private void Awake()
	{
		EventManager.OnEventChanged += OnEventChanged;
		Hide();
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private void OnEventChanged(List<EventData> events,bool isActiveEvent)
	{
		if (!isActiveEvent) return;
		if (events == null || events.Count == 0) return;
		EventData data = events.Last();
		bannerText.text = data.Description;
		gameObject.SetActive(true);
		StartCoroutine(HideBanner());
	}

	IEnumerator HideBanner()
	{
		yield return new WaitForSeconds(3);
		Hide();
		
	}

}
