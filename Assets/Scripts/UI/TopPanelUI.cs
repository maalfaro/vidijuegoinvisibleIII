using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class TopPanelUI : MonoBehaviour
{
	[SerializeField] private AttributeUI[] attributesUI;
	//[SerializeField] private AttributeUI community;
	//[SerializeField] private AttributeUI team;
	//[SerializeField] private AttributeUI money;

	private void Start()
	{
		InitializeAmounts();
		GEvent_OnAttributesChange.RegisterListener(OnAttributesChanged);
		GameManager.ShowAttirbutesModifier += OnShowAttirbutesModifier;
	}

	private void OnDestroy()
	{
		GEvent_OnAttributesChange.UnregisterListener(OnAttributesChanged);
	}

	private void InitializeAmounts()
	{
		for (int i = 0; i < attributesUI.Length; i++)
		{
			attributesUI[i].Initialize();
		}
	}

	private void OnAttributesChanged(GEvent_OnAttributesChange data)
	{
		//publisher.SetAmount(data.publisher);
		//community.SetAmount(data.community);
		//team.SetAmount(data.team);
		//money.SetAmount(data.money);
	}

	private void OnShowAttirbutesModifier(GlobalData.AttributesEffect[] attributes)
	{
		for(int i = 0; i < attributes.Length; i++)
		{
			if (attributes[i].amount == 0) continue;

		}
	}

	private void ShowAttributeModifier(GlobalData.Attributes attribute, int amount)
	{
		if (amount == 0) return;
		//AttributeUI att = attributesUI.FirstOrDefault(x=>x.attr)
	}



}
