using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI;

public class Card : MonoBehaviour
{

	#region Members

	[SerializeField]
	private Image characterImage;

	[SerializeField] private Image characterBG;

	[SerializeField]
	private GameObject backCard;

	[SerializeField]
	private Image backgroundCharacterImage;

	[Header("Animaciones")]
	[SerializeField]
	private Tweener[] firstFlipCardTweener;

	[SerializeField]
	private Tweener[] secondFlipCardTweener;

	[SerializeField]
	private Tweener[] nextCardRightTweeners;

	[SerializeField]
	private Tweener[] nextCardLeftTweeners;

	[SerializeField]
	private Tweener holdCardTweener;

	[SerializeField]
	private Tweener releaseCardTweener;

	private CardData currentCardData;
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	#endregion

	#region MonoBehaviour Methods

	private void Awake()
	{
		GameManager.OnGameStart += Initialize;
	}

	private void Initialize()
	{
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		firstFlipCardTweener[0].AddOnFinishedCallback(ChangeToCharacter);
		secondFlipCardTweener[0].AddOnFinishedCallback(EnableInput);
		holdCardTweener.transform.localScale = Vector3.one;
	}

	private void OnEnable() {
		GameManager.InitializeCard += InitCurrentCard;
	}

	private void OnDisable() {
		GameManager.InitializeCard -= InitCurrentCard;
	}

	#endregion

	#region Public methods

	public void InitCurrentCard(CardData cardData)
	{
		if (cardData == null) return;
		backgroundCharacterImage.enabled = false;
		currentCardData = cardData;
		characterImage.sprite = currentCardData.Character.Sprite;
		characterBG.color = currentCardData.Character.background;
		backCard.SetActive(true);
		SoundsManager.Instance.PlaySound("flipCard");
		for (int i = 0; i < firstFlipCardTweener.Length; i++)
		{
			firstFlipCardTweener[i].PlayTweener();
		}
	}

	public void ExitCard(CardData cardData, bool isLeftChocie)
	{
		currentCardData = cardData;
		StartCoroutine(WaitForFinishCharacter(isLeftChocie));
	}

	public void AnimateCard(bool hold)
	{
		if (hold)
		{
			holdCardTweener.PlayTweener();
		}
		else
		{
			releaseCardTweener.PlayTweener();
		}
	}

	#endregion

	#region Private methods

	private void ChangeToCharacter()
	{
		for (int i = 0; i < secondFlipCardTweener.Length; i++)
		{
			secondFlipCardTweener[i].PlayTweener();
		}
		backCard.SetActive(false);
		backgroundCharacterImage.enabled = true;
	}

	private IEnumerator WaitForFinishCharacter(bool isLeftChocie)
	{
		InputManager.Instance.CanMove = false;
		SoundsManager.Instance.PlaySound("woosh");
		Tweener[] nextCardTweeners = isLeftChocie ? nextCardLeftTweeners : nextCardRightTweeners;
		for (int i = 0; i < nextCardTweeners.Length; i++)
		{
			nextCardTweeners[i].PlayTweener();
		}
		yield return new WaitForSeconds(nextCardTweeners[0].Duration);
		setInitialPosition();
		GameManager.Instance.SetNextCard(currentCardData);
	}

	private void setInitialPosition()
	{
		transform.position = initialPosition;
		transform.rotation = initialRotation;
	}

	private void EnableInput()
	{
		InputManager.Instance.CanMove = true;
	}

	#endregion
}
