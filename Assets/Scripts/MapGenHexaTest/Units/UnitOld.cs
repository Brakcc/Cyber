using UnityEngine;

public class UnitOld : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(Sprite sprite) {
        _renderer.sprite = sprite;
    }
}