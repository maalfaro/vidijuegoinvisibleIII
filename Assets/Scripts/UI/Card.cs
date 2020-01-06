using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	[SerializeField] private Text questionText;

	[Header("Character")]
	[SerializeField] private Image characterImg;
	[SerializeField] private Text characterName;
	[SerializeField] private Text characterDescription;

	private void Awake()
	{
		GameManager.InitializeCard += Initialize;
	}

	private void OnDisable()
	{
		GameManager.InitializeCard -= Initialize;
	}

	public void Initialize(CardData cardData)
	{
		questionText.text = cardData.Description;
		characterImg.sprite = cardData.Character.Sprite;
		characterName.text = cardData.Character.Name;
		characterDescription.text = cardData.Character.Description;
	}


}
