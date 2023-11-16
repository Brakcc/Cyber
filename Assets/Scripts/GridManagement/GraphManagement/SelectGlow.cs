using UnityEngine;

[System.Serializable]
public class SelectGlow
{
    #region fields
    [SerializeField] HexaRefs hexaRefs;
    [SerializeField] GLowColors glowColors;
    bool isRangeGlowing;
    bool isPathGlowing;
    bool isButtonGlowing;
    bool isKapaGlowing;

    /// <summary>
    /// Ensemble des Tiles des reférences en enfant d'un Hex pour modifs les renderTex
    /// </summary>
    [System.Serializable]
    public class HexaRefs
    {
        public SpriteRenderer moveRange;
        public SpriteRenderer pathSelect;
        public SpriteRenderer buttonsKapa;
        public SpriteRenderer kapaSelect;
    }
    /// <summary>
    /// Ensemble des couleurs dispos pour les différents types de Glow
    /// </summary>
    [System.Serializable]
    public class GLowColors
    {
        public Color moveRangeColor;
        public Color selectedPathColor;
        public Color selectedKapaColor;
        public Color selectedKapaColorButton;
    }
    #endregion

    #region methodes 
    /// <summary>
    /// premier Set de secu pour desactiver l'ensemble des SpriteRenderer ref des RenderTextures
    /// </summary>
    public void SetHexaRefs()
    {
        hexaRefs.moveRange.enabled = false;
        hexaRefs.pathSelect.enabled = false;
        hexaRefs.buttonsKapa.enabled = false;
        hexaRefs.kapaSelect.enabled = false;
    }

    #region range glow
    void ToggleRange()
    {
        if (!isRangeGlowing) { hexaRefs.moveRange.enabled = true; }
        else { hexaRefs.moveRange.enabled = false; }

        isRangeGlowing = !isRangeGlowing;
    }
    /// <summary>
    /// active ou desactive le glow de la range de deplacement
    /// </summary>
    /// <param name="b"></param>
    public void ToggleRangeGlow(bool b) 
    {
        if (isRangeGlowing == b) return;
        isRangeGlowing = !b;
        ToggleRange();
    }
    #endregion

    #region path glow
    void TogglePath()
    {
        if (!isPathGlowing) { hexaRefs.pathSelect.enabled = true; }
        else { hexaRefs.pathSelect.enabled = false; }

        isPathGlowing = !isPathGlowing;
    }
    /// <summary>
    /// active ou desactive le glow du path par dessus celui de la range de deplacement
    /// </summary>
    /// <param name="b"></param>
    public void TogglePathGlow(bool b)
    {
        if (isPathGlowing == b) return;
        isPathGlowing = !b;
        TogglePath();
    }
    #endregion

    #region button glow
    void ToggleButton()
    {
        if (!isButtonGlowing) { hexaRefs.buttonsKapa.enabled = true; }
        else { hexaRefs.buttonsKapa.enabled = false; }

        isButtonGlowing = !isButtonGlowing;
    }
    /// <summary>
    /// active ou desactive les glow des boutons des capacites
    /// </summary>
    /// <param name="b"></param>
    public void ToggleButtonGlow(bool b)
    {
        if (isButtonGlowing == b) return;
        isButtonGlowing = !b;
        ToggleButton();
    }
    #endregion

    #region kapa glow
    void ToggleKapa()
    {
        if (!isKapaGlowing) { hexaRefs.kapaSelect.enabled = true; }
        else { hexaRefs.kapaSelect.enabled = false; }

        isKapaGlowing = !isKapaGlowing;
    }
    /// <summary>
    /// active ou desactive le glow des kapas par dessus celui des boutons
    /// </summary>
    /// <param name="b"></param>
    public void ToggleKapaGlow(bool b)
    {
        if (isKapaGlowing == b) return;
        isKapaGlowing = !b;
        ToggleKapa();
    }
    #endregion
    #endregion
}
