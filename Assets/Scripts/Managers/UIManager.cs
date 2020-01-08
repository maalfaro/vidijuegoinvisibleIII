using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager> {

	//#region Members

	//public Action OnGameStart = delegate { };

	//[SerializeField]
	//private TitlePanel titlePanel;

	//[SerializeField]
	//private MenuPanel menuPanel;

	[SerializeField]
	private GameplayPanel gameplayPanel;

	//[SerializeField]
	//private PowersPanel powersPanel;

	//[SerializeField]
	//private ResultPanel resultPanel;

	//[SerializeField]
	//private SettingsPanel settingsPanel;

	//[SerializeField]
	//private UnlocksPanel unlocksPanel;

	//#endregion


	//#region MonoBehaviour Methods

	void Start() {
		//titlePanel.InitPanel();
		gameplayPanel.InitPanel();

		InputManager.Instance.OnLeftChoiceSelected += SetUILeft;
		InputManager.Instance.OnRightChoiceSelected += SetUIRight;
		InputManager.Instance.DisableGameplayChoices += DisableGameplayChoices;
	}

	//void OnEnable() {
	//	titlePanel.OnContinue += OnStartMenuHandler;

	//	menuPanel.OnStartGame += OnStartGameHandler;
	//	menuPanel.OnSettings += OnSettingsHandler;
	//	menuPanel.OnCharactersUnlock += OnCharactersUnlockHandler;
	//	menuPanel.OnResultsUnlock += OnResultsUnlockHandler;

	//	settingsPanel.OnCloseSettings += OnCloseSettingsHandler;

	//	unlocksPanel.OnCloseUnlocks += OnCloseUnlocksHandler;

	//	resultPanel.OnBackToMenu += OnStartMenuHandler;

	//}


	//#endregion

	//#region Public methods

	//public void EnableTitlePanel() {
	//	titlePanel.InitPanel();
	//}

	private void SetUILeft() {
		gameplayPanel.SetUILeft();
	}

	private void SetUIRight() {
		gameplayPanel.SetUIRight();
	}

	private void DisableGameplayChoices() {
		gameplayPanel.DisableChoices();
	}

	//public void ShowResult(ResultData resultData) {
	//	gameplayPanel.ClosePanel();
	//	resultPanel.InitPanel();
	//	resultPanel.EnableResult(true, resultData.Description, resultData.ResultSprite);
	//}


	//#endregion

	//#region Private methods

	//private void OnStartGameHandler() {
	//	OnGameStart();
	//	menuPanel.ClosePanel();
	//	gameplayPanel.InitPanel();
	//	gameplayPanel.SetNextCard(CardDeckManager.Instance.CurrentCard);
	//	powersPanel.InitPanel();
	//	powersPanel.ChangeBalance(FourPowersManager.Instance.PowerValues);
	//}

	//private void OnSettingsHandler() {
	//	menuPanel.EnableGameButton(false);
	//	settingsPanel.InitPanel();
	//}

	//private void OnCharactersUnlockHandler() {
	//	menuPanel.EnableGameButton(false);
	//	unlocksPanel.InitCharactersPanel();
	//}

	//private void OnResultsUnlockHandler() {
	//	menuPanel.EnableGameButton(false);
	//	unlocksPanel.InitResultsPanel();
	//}

	//private void OnCloseSettingsHandler() {
	//	settingsPanel.ClosePanel();
	//	menuPanel.EnableGameButton(true);
	//}

	//private void OnCloseUnlocksHandler() {
	//	unlocksPanel.ClosePanel();
	//	menuPanel.EnableGameButton(true);
	//}

	//private void OnStartMenuHandler() {
	//	titlePanel.ClosePanel();
	//	menuPanel.InitPanel();
	//	powersPanel.ClosePanel();
	//}

	//#endregion

}
