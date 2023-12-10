using Enums.FeedBackEnums;
using GameContent.Entity.Unit.UnitWorking;
using Interfaces.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.InGameUI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        #region fields
        [SerializeField] Unit unit;
        [SerializeField] TMP_Text nameP;
        [SerializeField] TMP_Text mpText;
        [SerializeField] TMP_Text atkText;
        [SerializeField] TMP_Text defText;
        [SerializeField] TMP_Text critRate;
        [SerializeField] TMP_Text hP;
        [SerializeField] TMP_Text uPText;
        [SerializeField] Image imageRef;
        #endregion

        #region methodes

        public void OnInit()
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
        public void SetMP(IUnit unit, UIColorType uCT) { mpText.text = unit.CurrentMp.ToString(); SetColor(uCT); }
        public void SetMP(IUnit unit) { mpText.text = unit.CurrentMp.ToString(); SetColor(UIColorType.Default); }

        public void SetAtk(IUnit unit, UIColorType uCT) { atkText.text = unit.CurrentAtk.ToString(); SetColor(uCT); }
        public void SetAtk(IUnit unit) { atkText.text = unit.CurrentAtk.ToString(); SetColor(UIColorType.Default); }

        public void SetDef(IUnit unit, UIColorType uCT) { defText.text = unit.CurrentDef.ToString(); SetColor(uCT); }
        public void SetDef(IUnit unit) { defText.text = unit.CurrentDef.ToString(); SetColor(UIColorType.Default); }

        public void SetCritRate(IUnit unit, UIColorType uCT) { critRate.text = unit.CurrentCritRate.ToString(); SetColor(uCT); }
        public void SetCritRate(IUnit unit) { critRate.text = unit.CurrentCritRate.ToString(); SetColor(UIColorType.Default); }

        public void SetHP(IUnit unit)
        {
            hP.text = unit.CurrentHealth.ToString();
            hP.color = Color.Lerp(Color.red, Color.green, unit.CurrentHealth / unit.UnitData.HealthPoint);
        }

        public void SetUP(IUnit unit)
        {
            uPText.text = unit.UltPoints.ToString();
            if (unit.UltPoints > 0) { uPText.color = Color.green; }
            else uPText.color = Color.red;
        }
        #endregion

        void SetSprite(IUnit unit) { imageRef.sprite = unit.UnitData.Sprite; /*imageRef.color = unit.GetComponentInChildren<SpriteRenderer>().color;*/ }

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
}
