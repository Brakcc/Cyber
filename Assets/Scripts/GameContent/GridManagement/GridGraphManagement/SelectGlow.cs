using UnityEngine;

[System.Serializable]
public class SelectGlow
{
    #region fields
    [SerializeField] HexaRefs hexaRefs;
    [SerializeField] GLowColors glowColors;
    bool _isRangeGlowing;
    bool _isPathGlowing;
    bool _isButtonGlowing;
    bool _isKapaGlowing;
    bool _isBaseNetGlowing;
    bool _isDynaNetGlowing;

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
        public SpriteRenderer baseNetwork;
        public SpriteRenderer dynamicNetwork;
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
        public Color selectedBaseNetwork;
        public Color selectedDynamicNetwork;
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
        hexaRefs.baseNetwork.enabled = false;
        hexaRefs.dynamicNetwork.enabled = false;
    }

    #region range glow
    void ToggleRange()
    {
        if (!_isRangeGlowing) { hexaRefs.moveRange.enabled = true; }
        else { hexaRefs.moveRange.enabled = false; }

        _isRangeGlowing = !_isRangeGlowing;
    }
    /// <summary>
    /// active ou desactive le glow de la range de deplacement
    /// </summary>
    /// <param name="b"></param>
    public void ToggleRangeGlow(bool b) 
    {
        if (_isRangeGlowing == b) return;
        _isRangeGlowing = !b;
        ToggleRange();
    }
    #endregion

    #region path glow
    void TogglePath()
    {
        if (!_isPathGlowing) { hexaRefs.pathSelect.enabled = true; }
        else { hexaRefs.pathSelect.enabled = false; }

        _isPathGlowing = !_isPathGlowing;
    }
    /// <summary>
    /// active ou desactive le glow du path par dessus celui de la range de deplacement
    /// </summary>
    /// <param name="b"></param>
    public void TogglePathGlow(bool b)
    {
        if (_isPathGlowing == b) return;
        _isPathGlowing = !b;
        TogglePath();
    }
    #endregion

    #region button glow
    void ToggleButton()
    {
        if (!_isButtonGlowing) { hexaRefs.buttonsKapa.enabled = true; }
        else { hexaRefs.buttonsKapa.enabled = false; }

        _isButtonGlowing = !_isButtonGlowing;
    }
    /// <summary>
    /// active ou desactive les glow des boutons des capacites
    /// </summary>
    /// <param name="b"></param>
    public void ToggleButtonGlow(bool b)
    {
        if (_isButtonGlowing == b) return;
        _isButtonGlowing = !b;
        ToggleButton();
    }
    #endregion

    #region kapa glow
    void ToggleKapa()
    {
        if (!_isKapaGlowing) { hexaRefs.kapaSelect.enabled = true; }
        else { hexaRefs.kapaSelect.enabled = false; }

        _isKapaGlowing = !_isKapaGlowing;
    }
    /// <summary>
    /// active ou desactive le glow des kapas par dessus celui des boutons
    /// </summary>
    /// <param name="b"></param>
    public void ToggleKapaGlow(bool b)
    {
        if (_isKapaGlowing == b) return;
        _isKapaGlowing = !b;
        ToggleKapa();
    }
    #endregion

    #region Base network glow
    void ToggleBaseNet()
    {
        if (!_isBaseNetGlowing) { hexaRefs.baseNetwork.enabled = true; }
        else { hexaRefs.baseNetwork.enabled = false; }

        _isBaseNetGlowing = !_isBaseNetGlowing;
    }
    /// <summary>
    /// active ou desactive le glow des kapas par dessus celui des boutons
    /// </summary>
    /// <param name="b"></param>
    public void ToggleBaseNetGlow(bool b)
    {
        if (_isBaseNetGlowing == b) return;
        _isBaseNetGlowing = !b;
        ToggleBaseNet();
    }
    #endregion

    #region Dynamic network glow
    void ToggleDynaNet()
    {
        if (!_isDynaNetGlowing) { hexaRefs.dynamicNetwork.enabled = true; }
        else { hexaRefs.dynamicNetwork.enabled = false; }

        _isDynaNetGlowing = !_isDynaNetGlowing;
    }
    /// <summary>
    /// active ou desactive le glow des kapas par dessus celui des boutons
    /// </summary>
    /// <param name="b"></param>
    public void ToggleDynaNetGlow(bool b)
    {
        if (_isDynaNetGlowing == b) return;
        _isDynaNetGlowing = !b;
        ToggleDynaNet();
    }
    #endregion
    #endregion
}
