using UnityEngine;

namespace FeedBacks
{
    [System.Serializable]
    public class VFXManager 
    {
        #region fields

        [SerializeField] private Sprite[] sprites;
        [SerializeField] private GameObject partSys;
        [SerializeField] private float lifeTime;
        [SerializeField] private float size;
        
        #endregion

        #region methodes

        public void OnGenerateParticlesSys(Vector3 pos)
        {
            var newSys = Object.Instantiate(partSys, pos, Quaternion.identity);
            var part = newSys.GetComponent<ParticleSystem>();
            
            foreach (var s in sprites)
            {
                part.textureSheetAnimation.AddSprite(s);
            }

            var main = part.main;
            main.startLifetime = lifeTime;
            main.startSize = size;
            
            part.Emit(1);
            
            Object.Destroy(newSys, part.main.startLifetime.constant);
        }
        
        #endregion
    }
}