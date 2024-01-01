using Enums.FeedBackEnums;
using GameContent.Entity.Unit.UnitWorking;
using Interfaces.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGameUI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        #region fields
        
        [SerializeField] private Unit unit;
        [SerializeField] private TMP_Text nameP;
        [SerializeField] private TMP_Text mpText;
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text defText;
        [SerializeField] private TMP_Text critRate;
        [SerializeField] private TMP_Text hP;
        [SerializeField] private TMP_Text uPText;
        [SerializeField] private Image imageRef;
        
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
            SetPrec(unit);
            SetSprite(unit);
        }

        #region recurent methodes
        
        public void SetMP(IUnit uRef, UIColorType uCT) { mpText.text = uRef.CurrentMp.ToString(); mpText.color = SetColor(uCT); }
        public void SetMP(IUnit uRef) { mpText.text = uRef.CurrentMp.ToString(); mpText.color = SetColor(UIColorType.Default); }

        public void SetAtk(IUnit uRef, UIColorType uCT) { atkText.text = uRef.CurrentAtk.ToString(); atkText.color = SetColor(uCT); }
        public void SetAtk(IUnit uRef) { atkText.text = uRef.CurrentAtk.ToString(); atkText.color = SetColor(UIColorType.Default); }

        public void SetDef(IUnit uRef, UIColorType uCT) { defText.text = uRef.CurrentDef.ToString(); defText.color = SetColor(uCT); }
        public void SetDef(IUnit uRef) { defText.text = uRef.CurrentDef.ToString(); defText.color = SetColor(UIColorType.Default); }

        public void SetCritRate(IUnit uRef, UIColorType uCT) { critRate.text = uRef.CurrentCritRate.ToString(); critRate.color = SetColor(uCT); }
        public void SetCritRate(IUnit uRef) { critRate.text = uRef.CurrentCritRate.ToString(); critRate.color = SetColor(UIColorType.Default); }
        
        public void SetPrec(IUnit uRef, UIColorType uCT) { uPText.text = uRef.CurrentPrecision.ToString(); uPText.color = SetColor(uCT); }
        public void SetPrec(IUnit uRef) { uPText.text = uRef.CurrentPrecision.ToString(); uPText.color = SetColor(UIColorType.Default); }

        public void SetHP(IUnit uRef)
        {
            hP.text = ((int)uRef.CurrentHealth).ToString();
            hP.color = Color.Lerp(Color.red, Color.green, uRef.CurrentHealth / uRef.UnitData.HealthPoint);
        }

        public void SetUP(IUnit uRef)
        {
            uPText.text = uRef.UltPoints.ToString();
            uPText.color = uRef.UltPoints > 0 ? Color.green : Color.red;
        }
        
        #endregion

        private void SetSprite(IUnit uRef)
        {
            imageRef.sprite = uRef.UnitData.Sprite; 
            /*imageRef.color = uRef.GetComponentInChildren<SpriteRenderer>().color;*/
        }

        private void SetName(string n) { nameP.text = n; }

        private static Color SetColor(UIColorType uCT) => (int)uCT switch
        {
            0 => Color.green,
            1 => Color.red,
            2 => Color.white,
            _ => Color.white,
        };
        
        #endregion
    }
}
