using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice 
{

	public string Text;
	public GlobalData.AttributesEffect[] attributes;

	[Header("Next card")]
	public CardData nextCard;

	[Header("Event")]
	public EventData eventData;
}
