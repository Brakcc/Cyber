using UnityEngine;

namespace GameContent.GridManagement.GridGraphManagement
{
    [System.Serializable]
    public class SelectGlow
    {
        #region fields
        
        [SerializeField] private HexaRefs hexaRefs;
        private bool _isRangeGlowing;
        private bool _isPathGlowing;
        private bool _isButtonGlowing;
        private bool _isKapaGlowing;
        private bool _isBaseNetGlowing;
        private bool _isHackedNetGlowing;
        private bool _isDynaNetGlowing;

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
            public SpriteRenderer hackedNetwork;
            public SpriteRenderer dynamicNetwork;
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
            hexaRefs.hackedNetwork.enabled = false;
            hexaRefs.dynamicNetwork.enabled = false;
        }

        #region range glow

        private void ToggleRange()
        {
            hexaRefs.moveRange.enabled = !_isRangeGlowing;
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

        private void TogglePath()
        {
            hexaRefs.pathSelect.enabled = !_isPathGlowing;
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

        private void ToggleButton()
        {
            hexaRefs.buttonsKapa.enabled = !_isButtonGlowing;
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

        private void ToggleKapa()
        {
            hexaRefs.kapaSelect.enabled = !_isKapaGlowing;
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

        private void ToggleBaseNet()
        {
            hexaRefs.baseNetwork.enabled = !_isBaseNetGlowing;
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
        
        #region Hacked network glow

        private void ToggleHackedNet()
        {
            hexaRefs.hackedNetwork.enabled = !_isHackedNetGlowing;
            _isHackedNetGlowing = !_isHackedNetGlowing;
        }
        /// <summary>
        /// active ou desactive le glow des kapas par dessus celui des boutons
        /// </summary>
        /// <param name="b"></param>
        public void ToggleHackedNetGlow(bool b)
        {
            if (_isHackedNetGlowing == b) return;
            _isHackedNetGlowing = !b;
            ToggleHackedNet();
        }
        
        #endregion

        #region Dynamic network glow

        private void ToggleDynaNet()
        {
            hexaRefs.dynamicNetwork.enabled = !_isDynaNetGlowing;
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
}
