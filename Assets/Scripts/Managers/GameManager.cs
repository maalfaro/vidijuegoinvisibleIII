using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : Singleton<GameManager>
{

	public static Action<CardData> InitializeCard;
	public static Action<GlobalData.AttributesEffect[]> ShowAttirbutesModifier;

	[SerializeField] private List<CardData> cards;

	private void Start()
	{
		cards = Resources.LoadAll<CardData>("").ToList();

		InitializeCard?.Invoke(cards[0]);

	}


	public void SelecRightChoice()
	{

	}

	public void SelectLeftChoice()
	{

	}

	public void SelectCard()
	{

	}


}
