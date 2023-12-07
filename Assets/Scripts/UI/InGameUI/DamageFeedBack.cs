using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageFeedBack : MonoBehaviour
{
    #region fields
    [SerializeField] TMP_Text damageText;
    #endregion

    #region methodes
    void OnEnable()
    {
        StartCoroutine(UIEffect());
    }

    public void OnInit(float damage)
    {
        damageText.text = "-" + damage;
        damageText.color = Color.red;
        transform.DOMoveY(transform.position.y + 0.75f, 2);
    }

    IEnumerator UIEffect()
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