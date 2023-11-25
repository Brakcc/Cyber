using UnityEngine;
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
        SetMP(unit.CurrentMP);
        SetAtk(unit.CurrentAtk);
        SetDef(unit.CurrentDef);
        SetCritRate(unit.CurrentCritRate);
        SetHP(unit.CurrentHealth);
        SetSprite(unit);
    }

    public void SetMP(int mp) => mpText.text = "MP : " + mp;
    
    public void SetAtk(int atk) => AtkText.text = "ATK : " + atk;
    
    public void SetDef(int def) => DefText.text = "DEF : " + def;
    
    public void SetCritRate(int cr) => CritRate.text = "Crit Rate : " + cr;
     
    public void SetHP(float hp) => hP.text = "HP : " + hp;

    void SetSprite(Unit unit) { imageRef.sprite = unit.UnitData.Sprite; imageRef.color = unit.GetComponentInChildren<SpriteRenderer>().color; }

    void SetName(string n) => nameP.text = n;
    #endregion
}