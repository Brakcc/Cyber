﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    #region fields
    [SerializeField] Unit unit;
    [SerializeField] TMP_Text nameP;
    [SerializeField] TMP_Text mpText;
    [SerializeField] TMP_Text AtkText;
    [SerializeField] TMP_Text DefText;
    [SerializeField] TMP_Text CritRate;
    [SerializeField] TMP_Text hP;
    [SerializeField] TMP_Text uPText;
    [SerializeField] Image imageRef;
    #endregion

    #region methodes
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        SetName(unit.UnitData.Name);
        SetMP(unit);
        SetAtk(unit);
        SetDef(unit);
        SetCritRate(unit);
        SetHP(unit);
        SetUP(unit);
        SetSprite(unit);
    }

    #region recurent methodes
    public void SetMP(Unit unit, UIColorType uCT) { mpText.text = unit.CurrentMP.ToString(); SetColor(uCT); }
    public void SetMP(Unit unit) { mpText.text = unit.CurrentMP.ToString(); SetColor(UIColorType.Default); }

    public void SetAtk(Unit unit, UIColorType uCT) { AtkText.text = unit.CurrentAtk.ToString(); SetColor(uCT); }
    public void SetAtk(Unit unit) { AtkText.text = unit.CurrentAtk.ToString(); SetColor(UIColorType.Default); }

    public void SetDef(Unit unit, UIColorType uCT) { DefText.text = unit.CurrentDef.ToString(); SetColor(uCT); }
    public void SetDef(Unit unit) { DefText.text = unit.CurrentDef.ToString(); SetColor(UIColorType.Default); }

    public void SetCritRate(Unit unit, UIColorType uCT) { CritRate.text = unit.CurrentCritRate.ToString(); SetColor(uCT); }
    public void SetCritRate(Unit unit) { CritRate.text = unit.CurrentCritRate.ToString(); SetColor(UIColorType.Default); }

    public void SetHP(Unit unit)
    {
        hP.text = unit.CurrentHealth.ToString();
        hP.color = Color.Lerp(Color.red, Color.green, unit.CurrentHealth / unit.UnitData.HealthPoint);
    }

    public void SetUP(Unit unit)
    {
        uPText.text = unit.UltPoints.ToString();
        if (unit.UltPoints > 0) { uPText.color = Color.green; }
        else uPText.color = Color.red;
    }
    #endregion

    void SetSprite(Unit unit) { imageRef.sprite = unit.UnitData.Sprite; imageRef.color = unit.GetComponentInChildren<SpriteRenderer>().color; }

    void SetName(string n) { nameP.text = n; }

    Color SetColor(UIColorType uCT) => (int)uCT switch
    {
        0 => Color.green,
        1 => Color.red,
        2 => Color.white,
        _ => Color.white,
    };
    #endregion
}

public enum UIColorType
{
    Buff,
    Debuff,
    Default
}