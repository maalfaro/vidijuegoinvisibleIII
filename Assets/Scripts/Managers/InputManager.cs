using UnityEngine;
using System;

  /// <summary>
  /// Class purpose
  /// </summary>
  public class InputManager : Singleton<InputManager> {

    #region Members

    public event Action OnLeftChoiceConfirmed = delegate { };
    public event Action OnRightChoiceConfirmed = delegate { };
	public event Action OnLeftChoiceSelected = delegate { };
	public event Action OnRightChoiceSelected = delegate { };
	public event Action DisableGameplayChoices = delegate { };

	[SerializeField]
    private float angleModifier = 0.5f;

    [SerializeField]
    private float maxBottom = 400f;

    [SerializeField]
    private Card currentCard;

    private float choiceOffset = 50;
    private bool canMove;
    private bool holdCard = false;
    private RectTransform cardRectTransform;
    private float halfScreenWidth;
    private float halfScreenHeight;
    private Vector3 mousePosition;
    private Vector3 finalCardPosition;
    private Vector3 initialCardPosition;
	private int change;

    public bool CanMove {
      get {
        return canMove;
      }

      set {
        canMove = value;
      }
    }

	#endregion

	#region MonoBehaviour Methods

	void Start() {
		halfScreenWidth = Screen.width / 2;
		halfScreenHeight = Screen.height / 2;

		initialCardPosition = currentCard.transform.position;
		cardRectTransform = currentCard.GetComponent<RectTransform>();

		GameManager.OnGameStart += OnGameStart;
		GameManager.OnGameOver += OnGameOver;
		GameManager.OnGamePaused += OnGamePaused;
	}

    void Update() {
		if (CanMove && Input.GetMouseButton(0)) {
			AnimateCard(true);
			MoveCard();
			SetChoiceInterface();
		}
		else if (CanMove && Input.GetMouseButtonUp(0)) {
			AnimateCard(false);
			GetChosenOption();
		}else if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.GamePaused){
			GameManager.Instance.GamePaused = true;
		}
	}

	#endregion

	#region Private methods

	private void OnGameStart()
	{
		canMove = true;
	}

	private void OnGamePaused(bool paused)
	{
		canMove = !paused;
	}

	private void OnGameOver(bool playerWin)
	{
		canMove = false;
	}

    private void MoveCard() {

      mousePosition = finalCardPosition = Input.mousePosition;
      mousePosition.z = currentCard.transform.position.z;
      mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      mousePosition.z = currentCard.transform.position.z;
      LimitMousePosition();

      currentCard.transform.eulerAngles = new Vector3(0,0, -cardRectTransform.anchoredPosition.x * angleModifier);
    }

    private void LimitMousePosition() {
      if (mousePosition.y > initialCardPosition.y) {
        mousePosition.y = initialCardPosition.y;
      } else if (mousePosition.y < initialCardPosition.y - maxBottom) {
        mousePosition.y = initialCardPosition.y - maxBottom;
      }

      currentCard.transform.position = Vector3.Lerp(currentCard.transform.position,
                                                    mousePosition,
                                                    Time.deltaTime * 10);

      float x = cardRectTransform.anchoredPosition.x;
      if (x < -halfScreenWidth / 3) {
        x = -halfScreenWidth / 3;
      } else if (x > halfScreenWidth / 3) {
        x = halfScreenWidth / 3;
      }

      cardRectTransform.anchoredPosition = new Vector2(x, cardRectTransform.anchoredPosition.y);

    }

    private void GetChosenOption() {
      float finalPosition = cardRectTransform.anchoredPosition.x;
      if (finalPosition < -choiceOffset) {
        OnLeftChoiceConfirmed();
      } else if (finalPosition > choiceOffset) {
        OnRightChoiceConfirmed();
      } else {
        resetCard();
      }
    }

    private void resetCard() {
      currentCard.transform.eulerAngles = Vector3.zero;
      currentCard.transform.position = initialCardPosition;
    }

    private void SetChoiceInterface() {
		float finalPosition = cardRectTransform.anchoredPosition.x;
		if (finalPosition < -choiceOffset)
		{
			if (change != 1)
			{
				change = 1;
				DisableGameplayChoices?.Invoke();
			}
			OnLeftChoiceSelected?.Invoke();
		}
		else if (finalPosition > choiceOffset)
		{
			if (change != 2)
			{
				change = 2;
				DisableGameplayChoices?.Invoke();
			}
			OnRightChoiceSelected?.Invoke();
		}
		else
		{
			change = 0;
			DisableGameplayChoices?.Invoke();
		}
	}

    private void AnimateCard(bool hold) {
      if (holdCard != hold) {
        holdCard = hold;
        currentCard.AnimateCard(hold);
      }

    }

    #endregion

  }



