﻿using UnityEngine;
using System;

  /// <summary>
  /// Class purpose
  /// </summary>
  public class InputManager : Singleton<InputManager> {

    #region Members

    public event Action OnLeftChoiceConfirmed = delegate { };
    public event Action OnRightChoiceConfirmed = delegate { };

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
    }

    void Update() {
      if (CanMove && Input.GetMouseButton(0)) {
        animateCard(true);
        moveCard();
        setChoiceInterface();
      }
      else if (CanMove && Input.GetMouseButtonUp(0)) {
        animateCard(false);
        getChosenOption();
      }
    }

    #endregion

    #region Private methods

    private void moveCard() {

      mousePosition = finalCardPosition = Input.mousePosition;
      mousePosition.z = currentCard.transform.position.z;
      mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      mousePosition.z = currentCard.transform.position.z;
      limitMousePosition();

      currentCard.transform.eulerAngles = new Vector3(0,0, -cardRectTransform.anchoredPosition.x * angleModifier);
    }

    private void limitMousePosition() {
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

    private void getChosenOption() {
      float finalPosition = cardRectTransform.anchoredPosition.x;
      if (finalPosition < -choiceOffset) {
        OnLeftChoiceConfirmed();
      }
      else if (finalPosition > choiceOffset) {
        OnRightChoiceConfirmed();
      } else {
        resetCard();
      }
    }

    private void resetCard() {
      currentCard.transform.eulerAngles = Vector3.zero;
      currentCard.transform.position = initialCardPosition;
    }

    private void setChoiceInterface() {
      float finalPosition = cardRectTransform.anchoredPosition.x;
		if (finalPosition < -choiceOffset)
		{
			//UIManager.Instance.SetUILeft();
		}
		else if (finalPosition > choiceOffset)
		{
			//UIManager.Instance.SetUIRight();
		}
		else
		{
			//UIManager.Instance.DisableGameplayChoices();
		}
	}

    private void animateCard(bool hold) {
      if (holdCard != hold) {
        holdCard = hold;
        currentCard.AnimateCard(hold);
      }

    }

    #endregion

  }



