using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.UI
{
  /// <summary>
  /// Rotation Tweener to animate UI transforms
  /// </summary>
  public class FlipTweener : TransformTweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/
    [SerializeField]
    private float initialRotation;

    [SerializeField]
    private float finalRotation;

    private static readonly Vector3 YComponentModifier = new Vector3(0, 1, 0);


    /**********************************************************************************************/
    /*  Protected Methods                                                                         */
    /**********************************************************************************************/
    protected override void UpdateTransform(float curveValue)
    {
      cachedTransform.localEulerAngles = YComponentModifier * Mathf.Lerp(initialRotation, finalRotation, curveValue);
    }

    protected override void SetCurrentInitialValue() {
      initialRotation = cachedTransform.localEulerAngles.y;
    }


  }
}