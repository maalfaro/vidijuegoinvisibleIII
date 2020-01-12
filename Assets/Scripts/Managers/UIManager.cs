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

	#endregion

	#region MonoBehaviour Methods

	void Start() {
		GameManager.OnGameStart += OnGameStart;
		GameManager.OnGameOver += OnGameOver;

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
		gameplayPanel.Show();
	}

	public void ShowMenu()
	{
		gameplayPanel.Hide();
		resultPanel.Hide();
		menuPanel.Show();
	}

	#endregion

	#region Private methods

	private void OnGameStart()
	{
		gameplayPanel.InitPanel();
		topPanelUI.InitPanel();
		resultPanel.InitPanel();
	}

	private void OnGameOver()
	{
		resultPanel.Show();
		menuPanel.Hide();
		gameplayPanel.Hide();
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
