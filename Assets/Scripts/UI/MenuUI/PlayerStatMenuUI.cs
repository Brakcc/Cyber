using System.Globalization;
using Interfaces.Unit;
using TMPro;
using UnityEngine;

namespace UI.MenuUI
{
    public class PlayerStatMenuUI : MonoBehaviour
    {
        #region fields
        
        [SerializeField] private TMP_Text mpText;
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text defText;
        [SerializeField] private TMP_Text critText;
        [SerializeField] private TMP_Text hPText;
        [SerializeField] private TMP_Text kapa0DescriptionText;
        [SerializeField] private TMP_Text kapa1DescriptionText ;

        #endregion

        #region methodes

        public void OnInitUI(IUnitData uData)
        {
            SetMp(uData, mpText);
            SetAtk(uData, atkText);
            SetDef(uData, defText);
            SetCrit(uData, critText);
            SetHp(uData, hPText);
            SetKapa0(uData, kapa0DescriptionText);
            SetKapa1(uData, kapa1DescriptionText);
        }
        
        #region setter Methodes

        private static void SetMp(IUnitData uD, TMP_Text txt) { txt.text = uD.MovePoints.ToString(); }
        private static void SetAtk(IUnitData uD, TMP_Text txt) { txt.text = uD.Attack.ToString(); }
        private static void SetDef(IUnitData uD, TMP_Text txt) { txt.text = uD.Defense.ToString(); }
        private static void SetCrit(IUnitData uD, TMP_Text txt) { txt.text = uD.CritRate.ToString(); }
        private static void SetHp(IUnitData uD, TMP_Text txt) { txt.text = uD.HealthPoint.ToString(CultureInfo.InvariantCulture); }

        private static void SetKapa0(IUnitData uD, TMP_Text txt)
        {
            txt.text = $"{uD.KapasList[0].KapaName} : \r\n {uD.KapasList[0].Description}";
        }
        private static void SetKapa1(IUnitData uD, TMP_Text txt)
        {
            txt.text = $"{uD.KapasList[1].KapaName} : \r\n {uD.KapasList[1].Description}";
        }

        #endregion

        #endregion
    }
}