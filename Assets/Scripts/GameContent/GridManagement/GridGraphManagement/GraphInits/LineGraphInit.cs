using UnityEngine;

namespace GameContent.GridManagement.GridGraphManagement.GraphInits
{
    [System.Serializable]
    public class LineGraphInit
    {
        #region fields

        public Gradient colorGrad;
        public AnimationCurve widthCurve;
        public LineAlignment align;
        public int orderInLayer;
        public Material mat;

        #endregion
    }
}