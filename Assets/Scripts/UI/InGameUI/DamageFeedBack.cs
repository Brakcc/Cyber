using System.Collections;
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

        private void OnEnable()
        {
            StartCoroutine(UIEffect());
        }

        public void OnInit(float damage, List<BuffDatas> allBuffs)
        {
            var txt = damage == 0 ? "" : $"<color=red>-{(int)damage} HP</color>";
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
            damageText.text = txt;
            transform.DOMoveY(transform.position.y + 0.75f, 2);
        }

        
        
        private IEnumerator UIEffect()
        {
            float t = 0;
            yield return new WaitForSeconds(0.75f);
            while (t < 1.5f)
            {
                damageText.color -= new Color(0, 0, 0, Time.deltaTime);
                t += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
        
        #endregion
    }
}
