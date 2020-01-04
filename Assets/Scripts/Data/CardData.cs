using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Vidijuego/New Card")]
public class CardData : ScriptableObject
{

	[Header("Card Description")]
	public string Name;
	[TextArea(5, 15)]
	public string Description;
	[Space(5)]
	public CharacterData Character;

	[Header("Card Choices")]
	public Choice LeftChoice;
	[Space(15)]
	public Choice RightChoice;

	[Header("Card post choice")]
	public string postChoiceText;

	[Space(20)]
	public int weight = 1;

}
