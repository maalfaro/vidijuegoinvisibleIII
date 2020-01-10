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
	[SerializeField] private AttributesManager attributesManager;

	private CardData currentCard;

	private void Start()
	{
		//cards = Resources.LoadAll<CardData>("").ToList();

		InputManager.Instance.OnLeftChoiceConfirmed += OnLeftChoiceConfirmedHandler;
		InputManager.Instance.OnRightChoiceConfirmed += OnRightChoiceConfirmedHandler;

		attributesManager.Initialize();

		SetNextCard(GetRandomCard());
		//InitializeCard?.Invoke(cards[0]);
	}

	private void nextTurn(bool isLeftChoice) {
		//if (CardDeckManager.Instance.CurrentCard.UnlockedTransition != null) {
		//	OnChangeStageHandler(CardDeckManager.Instance.CurrentCard.UnlockedTransition);
		//}
		//checkNewCharacters();

		if (CheckGameFinished())
		{

		}
		else
		{
			SetNextCard(isLeftChoice);
		}

		//LifeStageManager.Instance.CheckYear();
	}

	private void OnLeftChoiceConfirmedHandler() {
		ApplyChoices(currentCard.LeftChoice.attributes);
		nextTurn(isLeftChoice: true);

	}

	private void OnRightChoiceConfirmedHandler() {
		ApplyChoices(currentCard.RightChoice.attributes);
		nextTurn(isLeftChoice: false);
	}



	private void ApplyChoices(GlobalData.AttributesEffect[] attributesEffect)
	{
		for(int i=0;i< attributesEffect.Length; i++)
		{
			attributesManager.ChangeAttributeValue(attributesEffect[i]);
		}
		FireAttributesChanged(attributesEffect);
	}

	private void FireAttributesChanged(GlobalData.AttributesEffect[] attributes) {

		GEvent_OnAttributesChange ge = new GEvent_OnAttributesChange();
		ge.Description = "GE: Los atributos han cambiado";
		ge.publisher = attributesManager.GetAttributeAmount(GlobalData.Attributes.Publisher); 
		ge.community = attributesManager.GetAttributeAmount(GlobalData.Attributes.Community);
		ge.team = attributesManager.GetAttributeAmount(GlobalData.Attributes.Team); 
		ge.money = attributesManager.GetAttributeAmount(GlobalData.Attributes.Money); 
		ge.FireEvent();
	}

	private bool CheckGameFinished()
	{
		for(int i = 0; i < attributesManager.AttributeValues.Length; i++)
		{
			if(attributesManager.AttributeValues[i]<=0 || attributesManager.AttributeValues[i] >= 10)
			{
				//TODO GAME OVER
				Debug.LogError("GAME OVER");
				return true;
			}
		}
		return false;
	}

	#region Cards methods

	private int GetTotalWeight()
	{
		return cards.Sum(c => c.weight);
	}

	private void SetNextCard(bool isLeftChoice)
	{
		//CardDeckManager.Instance.SetNextCard(isLeftChoice);
		GameplayPanel.Instance.StartExitCardAnimation(GetNextCard(isLeftChoice), isLeftChoice);
	}

	private CardData GetNextCard(bool isLeftChoice)
	{
		CardData nextCard = isLeftChoice ? currentCard.LeftChoice.nextCard : currentCard.RightChoice.nextCard;
		return nextCard ?? GetRandomCard();
	}

	private CardData GetRandomCard()
	{
		CardData nextCard = null;

		int totalWeight = GetTotalWeight();
		int selectedCard = UnityEngine.Random.Range(0, totalWeight);

		int weight = 0;

		cards.Shuffle();

		for (int i = 0; i < cards.Count; i++)
		{
			weight += cards[i].weight;
			if (weight >= selectedCard)
			{
				nextCard = cards[i];
				cards.Remove(nextCard);
				break; 
			}
		}


		return nextCard;
	}

	public void SetNextCard(CardData cardData)
	{
		currentCard = cardData;
		InitializeCard?.Invoke(cardData);
	}

	#endregion


}


public static class Extensions
{
	public static void Shuffle<T>(this IList<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			T temp = list[i];
			int randomIndex = UnityEngine.Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}
}
