using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class TopPanelUI : MonoBehaviour
{
	[SerializeField] private AttributeUI publisher;
	[SerializeField] private AttributeUI community;
	[SerializeField] private AttributeUI team;
	[SerializeField] private AttributeUI money;

	private CardData cardData;

	public void InitPanel()
	{
		InitializeAmounts();
		DisableModifiers();
	}

	private void Awake()
	{
		GEvent_OnAttributesChange.RegisterListener(OnAttributesChanged);
		GameManager.InitializeCard += OnInitializeCard;
	}

	private void OnDestroy()
	{
		GEvent_OnAttributesChange.UnregisterListener(OnAttributesChanged);
		GameManager.InitializeCard -= OnInitializeCard;
	}

	private void InitializeAmounts()
	{
		publisher.Initialize();
		community.Initialize();
		team.Initialize();
		money.Initialize();
	}

	private void OnInitializeCard(CardData cardData)
	{
		this.cardData = cardData;
	}

	private void OnAttributesChanged(GEvent_OnAttributesChange data)
	{
		publisher.SetAmount(data.publisher);
		community.SetAmount(data.community);
		team.SetAmount(data.team);
		money.SetAmount(data.money);
		DisableModifiers();
	}

	private void ShowAttributeModifier(GlobalData.Attributes attribute, float amount)
	{
		if (amount == 0) return;
		switch (attribute)
		{
			case GlobalData.Attributes.Community: community.ShowModifier(amount); break;
			case GlobalData.Attributes.Publisher: publisher.ShowModifier(amount); break;
			case GlobalData.Attributes.Team: team.ShowModifier(amount); break;
			case GlobalData.Attributes.Money: money.ShowModifier(amount); break;

		}
	}

	public void SetLeftAttributes()
	{
		for(int i = 0; i < cardData.LeftChoice.attributes.Length; i++)
		{
			GlobalData.AttributesEffect at = cardData.LeftChoice.attributes[i];
			ShowAttributeModifier(at.attribute, at.amount);
		}
	}

	public void SetRightAttributes()
	{
		for (int i = 0; i < cardData.RightChoice.attributes.Length; i++)
		{
			GlobalData.AttributesEffect at = cardData.RightChoice.attributes[i];
			ShowAttributeModifier(at.attribute, at.amount);
		}
	}

	public void DisableModifiers()
	{
		publisher.ShowModifier(0);
		community.ShowModifier(0);
		team.ShowModifier(0);
		money.ShowModifier(0);
	}

}
