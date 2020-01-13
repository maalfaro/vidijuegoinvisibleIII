using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : Singleton<GameManager>
{
	public static Action OnGameStart;
	public static Action<CardData> InitializeCard;
	public static Action<GlobalData.AttributesEffect[]> ShowAttirbutesModifier;
	public static Action<int> OnProjectsChange;
	public static Action OnGameOver;

	[SerializeField] private List<CardData> cards;
	[SerializeField] private List<FinalCardData> finalCards;
	[SerializeField] private AttributesManager attributesManager;
	[SerializeField] private CardData initialCard;

	private CardData currentCard;
	private int projectsCount;

	public int ProjectsCount => projectsCount;

	private void Start()
	{
		cards = Resources.LoadAll<CardData>("Game").ToList();
		finalCards = Resources.LoadAll<FinalCardData>("Finals").ToList();

		InputManager.Instance.OnLeftChoiceConfirmed += OnLeftChoiceConfirmedHandler;
		InputManager.Instance.OnRightChoiceConfirmed += OnRightChoiceConfirmedHandler;
	}

	public void PlayGame()
	{
		OnGameStart?.Invoke();

		attributesManager.Initialize();
		SetNextCard(initialCard);
		OnProjectsChange?.Invoke(projectsCount);
	}

	public void ResetGame()
	{
		cards = Resources.LoadAll<CardData>("Game").ToList();
		projectsCount = 0;
		PlayGame();
	}

	private void nextTurn(bool isLeftChoice) {

		if (finalCards.Contains(currentCard))
		{
			OnGameOver();
			return;
		}

		CheckProjectsCount(isLeftChoice);
		if (CheckGameFinished())
		{
			SetNextCard(GetFinalCard());
		}
		else
		{
			SetNextCard(isLeftChoice);
		}
	}

	private void CheckProjectsCount(bool isLeftChoice)
	{
		if (currentCard.Name.StartsWith("Init")) return;

		if(isLeftChoice && currentCard.LeftChoice.nextCard == null)
		{
			projectsCount++;
			OnProjectsChange?.Invoke(projectsCount);
		}else if(!isLeftChoice && currentCard.RightChoice.nextCard == null)
		{
			projectsCount++;
			OnProjectsChange?.Invoke(projectsCount);
		}
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
				return true;
			}
		}

		if (cards.Count == 0) return true;
		return false;
	}

	private CardData GetFinalCard() {
		GlobalData.Attributes attr = attributesManager.GetAttributeFinal();
		return finalCards.FirstOrDefault(x => x.attribute.Equals(attr));
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
