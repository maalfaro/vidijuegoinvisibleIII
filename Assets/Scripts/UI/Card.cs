using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI;

public class Card : MonoBehaviour
{
	[SerializeField] private Text questionText;

	[Header("Character")]
	[SerializeField] private Image characterImg;
	[SerializeField] private Text characterName;
	[SerializeField] private Text characterDescription;

	private void Awake()
	{
		GameManager.InitializeCard += InitCurrentCard;
	}

	private void OnDisable()
	{
		GameManager.InitializeCard -= InitCurrentCard;
	}

	//public void Initialize(CardData cardData)
	//{
	//	questionText.text = cardData.Description;
	//	characterImg.sprite = cardData.Character.Sprite;
	//	characterName.text = cardData.Character.Name;
	//	characterDescription.text = cardData.Character.Description;
	//}


	#region Members

	[SerializeField]
	private Image characterImage;

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
	private Tweener[] nextCardTweeners;

	[SerializeField]
	private Tweener holdCardTweener;

	[SerializeField]
	private Tweener releaseCardTweener;

	private CardData currentCardData;
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	#endregion
	
	#region MonoBehaviour Methods

	private void Start()
	{
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		firstFlipCardTweener[0].AddOnFinishedCallback(ChangeToCharacter);
		secondFlipCardTweener[0].AddOnFinishedCallback(EnableInput);
	}

	#endregion
	
	#region Public methods

	public void SetShowCardCallback()
	{

	}

	public void InitCurrentCard(CardData cardData)
	{
		backgroundCharacterImage.enabled = false;
		currentCardData = cardData;
		characterImage.sprite = currentCardData.Character.Sprite;
		backCard.SetActive(true);

		for (int i = 0; i < firstFlipCardTweener.Length; i++)
		{
			firstFlipCardTweener[i].PlayTweener();
		}
	}

	public void ExitCard(CardData cardData)
	{
		currentCardData = cardData;
		StartCoroutine(WaitForFinishCharacter());
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

	private IEnumerator WaitForFinishCharacter()
	{
		InputManager.Instance.CanMove = false;
		for (int i = 0; i < nextCardTweeners.Length; i++)
		{
			nextCardTweeners[i].PlayTweener();
		}
		yield return new WaitForSeconds(nextCardTweeners[0].Duration);
		setInitialPosition();
		GameManager.Instance.SetNextCard(/*currentCardData*/);
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
