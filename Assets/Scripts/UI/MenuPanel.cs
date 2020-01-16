using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuPanel : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private Text clickText;

	private bool cickable;

	private void OnEnable()
	{
		clickText.gameObject.SetActive(false);
		StartCoroutine(ShowClickText());
	}

	public void Show()
	{
		cickable = false;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		cickable = false;
		gameObject.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!cickable) return;

		SoundsManager.Instance.PlaySound("click");

		clickText.gameObject.SetActive(false);
		UIManager.Instance.ShowGameplayPanel();
		GameManager.Instance.PlayGame();
	}

	IEnumerator ShowClickText()
	{
		yield return new WaitForSeconds(9);
		cickable = true;
		clickText.gameObject.SetActive(true);
	}


}
