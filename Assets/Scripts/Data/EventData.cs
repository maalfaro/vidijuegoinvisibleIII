using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Vidijuego/New Event")]
public class EventData : ScriptableObject, ICloneable
{

	[Range(0f, 1f)]
	public float Probability;
	public string Description;
	public int Duration;

	public GlobalData.AttributesEffect[] AttributesEffect;

	public object Clone()
	{
		return this.MemberwiseClone();
	}
}
