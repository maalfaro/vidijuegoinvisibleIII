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
		//cards = Resources.LoadAll<CardData>("").ToList();

		InitializeCard?.Invoke(cards[0]);
		InputManager.Instance.OnLeftChoiceConfirmed += OnLeftChoiceConfirmedHandler;
		InputManager.Instance.OnRightChoiceConfirmed += OnRightChoiceConfirmedHandler;
	}

	private void nextTurn(bool isLeftChoice) {
		//if (CardDeckManager.Instance.CurrentCard.UnlockedTransition != null) {
		//	OnChangeStageHandler(CardDeckManager.Instance.CurrentCard.UnlockedTransition);
		//}
		//checkNewCharacters();
		SetNextCard(isLeftChoice);
		//LifeStageManager.Instance.CheckYear();
	}

	private void OnLeftChoiceConfirmedHandler() {
		//applyChoices(CardDeckManager.Instance.CurrentCard.LeftChoices);
		nextTurn(isLeftChoice: true);
	}

	private void OnRightChoiceConfirmedHandler() {
		//applyChoices(CardDeckManager.Instance.CurrentCard.RightChoices);
		nextTurn(isLeftChoice: false);
	}

	private void SetNextCard(bool isLeftChoice) {
		//CardDeckManager.Instance.SetNextCard(isLeftChoice);
		GameplayPanel.Instance.StartExitCardAnimation(cards[1]);
	}

	public void SetNextCard(CardData cardData)
	{
		InitializeCard?.Invoke(cards[1]);
	}

}
