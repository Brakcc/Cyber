using System.Collections.Generic;
using DG.Tweening;
using GameContent.Entity.Unit.UnitWorking;
using TMPro;
using UnityEngine;

namespace UI.InGameUI
{
    public class DamageFeedBack : MonoBehaviour
    {
        #region fields
        
        [SerializeField] private TMP_Text damageText;
        
        #endregion

        #region methodes
        
        public void OnInit(float damage, List<BuffDatas> allBuffs)
        {
            var txt = damage == 0 ? "" : $"<color=red>-{(int)damage} HP</color>";
            if (allBuffs.Count != 0)
            {
                foreach (var b in allBuffs)
                {
                    switch (b.buffValue)
                    {
                        case 0:
                            break;
                        case < 0:
                            txt += $"<color=red> {b.buffValue} {b.GetBuffTypeName()}</color>";
                            break;
                        case > 0:
                            txt += $"<color=green> {b.buffValue} {b.GetBuffTypeName()}</color>";
                            break;
                    }
                }
            }
            
            damageText.text = txt;
            transform.DOMoveY(transform.position.y + 0.75f, 2);
            damageText.DOColor(new Color(0, 0, 0, Time.deltaTime), 0.75f);
        }
        
        #endregion
    }
}
