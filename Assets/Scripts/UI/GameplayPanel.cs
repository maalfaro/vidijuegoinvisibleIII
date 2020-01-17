using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class GameplayPanel : Singleton<GameplayPanel> {

	#region Members
	public event Action OnContinueGame = delegate { };

	[SerializeField]
	private GameObject parent;

	[SerializeField]
	private Card currentCard;

	//[SerializeField]
	//private QuestionUI questionUI;

	[SerializeField] private TextMeshProUGUI questionText;
	[SerializeField] private Text characterNameText;
	[SerializeField] private Text characterDescriptionText;

	[SerializeField] private ChoicesUI choicesUI;

	//[SerializeField]
	//private TopPanelUI powersUI;

	//[SerializeField]
	//private TransitionUI transitionUI;

	//[SerializeField]
	//private ResultPanel resultUI;

	//[SerializeField]
	//private NameUI nameUI;

	#endregion

	#region MonoBehaviour Methods

	protected override void Awake()
	{
		base.Awake();
	}

	private void OnEnable()
	{
		GameManager.InitializeCard += SetNextCard;
	}

	private void OnDisable()
	{
		GameManager.InitializeCard -= SetNextCard;
	}

	#endregion

	#region Public methods

	public void InitPanel() {
		DisableChoices();
	}

	public void Show()
	{
		parent.SetActive(true);
	}

	public void Hide()
	{
		parent.SetActive(false);
	}

	public void StartExitCardAnimation(CardData card, bool isLeftChocie) {
		currentCard.ExitCard(card, isLeftChocie);
	}

	public void SetNextCard(CardData card) {
		currentCard.InitCurrentCard(card);
		//nameUI.ChangeName(card.Character.Name);
		SetQuestion(card.Description);
		SetCharactedText(card.Character.Name, card.Character.Description);
		SetChoices(card.LeftChoice.Text, card.RightChoice.Text);
		DisableChoices();
	}

	public void SetUILeft() {
		choicesUI.EnableLeftChoice();
	}

	public void SetUIRight() {
		choicesUI.EnableRightChoice();
	}

	public void DisableChoices() {
		choicesUI.DisableChoices();
	}

	public void ClosePanel() {
		parent.SetActive(false);
	}

	#endregion

	#region Private methods

	private void ContinueGame() {
		OnContinueGame();
	}

	private void SetQuestion(string question) {
		questionText.text = question;
	}

	private void SetCharactedText(string name, string description) {
		characterNameText.text = string.IsNullOrEmpty(name)?string.Empty : name;
		characterDescriptionText.text = string.IsNullOrEmpty(description) ? string.Empty : description;
		characterNameText.gameObject.SetActive(!string.IsNullOrEmpty(name));
		characterDescriptionText.gameObject.SetActive(!string.IsNullOrEmpty(description));
	}

	private void SetChoices(string left, string right) {
		choicesUI.SetChoices(left, right);
	}

	#endregion

}
