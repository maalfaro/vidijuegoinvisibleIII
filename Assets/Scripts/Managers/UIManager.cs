using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager> {

	#region Variables

	[SerializeField] private GameplayPanel gameplayPanel;
	[SerializeField] private TopPanelUI topPanelUI;
	[SerializeField] private ResultPanel resultPanel;
	[SerializeField] private MenuPanel menuPanel;
	[SerializeField] private ExitPanel exitPanel;

	private bool pausable;

	#endregion

	#region MonoBehaviour Methods

	void Start() {
		GameManager.OnGameStart += OnGameStart;
		GameManager.OnGameOver += OnGameOver;
		GameManager.OnGamePaused += OnGamePaused;

		InputManager.Instance.OnLeftChoiceSelected += SetUILeft;
		InputManager.Instance.OnRightChoiceSelected += SetUIRight;
		InputManager.Instance.DisableGameplayChoices += DisableGameplayChoices;
		
	}

	#endregion

	#region Public methods

	public void ShowGameplayPanel()
	{
		resultPanel.Hide();
		menuPanel.Hide();
		exitPanel.Hide();
		gameplayPanel.Show();
		pausable = true;
	}

	public void ShowMenu()
	{
		pausable = false;
		gameplayPanel.Hide();
		resultPanel.Hide();
		exitPanel.Hide();
		menuPanel.Show();
	}

	public void ShowExitPanel()
	{
		pausable = false;
		gameplayPanel.Hide();
		resultPanel.Hide();
		menuPanel.Hide();
		exitPanel.Show();
	}

	#endregion

	#region Private methods

	private void OnGameStart()
	{
		gameplayPanel.InitPanel();
		topPanelUI.InitPanel();
		resultPanel.InitPanel();
	}

	private void OnGameOver(bool playerWin)
	{
		pausable = false;
		resultPanel.Show(playerWin);
		menuPanel.Hide();
		gameplayPanel.Hide();
		exitPanel.Hide();
	}

	private void OnGamePaused(bool paused)
	{
		if (paused)	{
			ShowExitPanel();
		}
		else {
			ShowGameplayPanel();
		}
	}

	private void SetUILeft() {
		topPanelUI.SetLeftAttributes();
		gameplayPanel.SetUILeft();
	}

	private void SetUIRight() {
		gameplayPanel.SetUIRight();
		topPanelUI.SetRightAttributes();
	}

	private void DisableGameplayChoices() {
		gameplayPanel.DisableChoices();
		topPanelUI.DisableModifiers();
	}

#endregion

}
