using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class purpose
/// </summary>
[System.Serializable]
public class LocalizationData {

  /**********************************************************************************************/
  /*  Members                                                                                   */
  /**********************************************************************************************/
  #region Members

  public LocalizationItem[] General;
  public LocalizationItem[] CardName;
  public LocalizationItem[] CardOptionA;
  public LocalizationItem[] CardOptionB;
  public LocalizationItem[] Characters;
  public LocalizationItem[] Transitions;
  public LocalizationItem[] Events;
  public LocalizationItem[] Results;

  [System.Serializable]
  public class LocalizationItem {
    public string code;
    public string text;
  }

  #endregion
}


