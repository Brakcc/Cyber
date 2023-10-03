using UnityEngine;
using UnityEngine.UI;

public class KapaUISO : ScriptableObject, IKapaUISO
{
    [SerializeField] private Image kapaImage;
    public Image KapaImage {  get => kapaImage; }

    [SerializeField] private string kapaName;
    public string KapaName { get => kapaName; }

    [SerializeField] private int kapaCost;
    public int KapaCost { get => kapaCost; }

    [SerializeField] private string kapaDescription;
    public string KapaDescription { get => kapaDescription; }
}
