using UnityEngine;

namespace UI.MenuUI
{
    [System.Serializable]
    public struct MapData
    {
        public string mapName;
        public string sceneName;
        public Sprite mapImage;

        public MapData(string mapName, Sprite mapImage, string sceneName)
        {
            this.mapName = mapName;
            this.mapImage = mapImage;
            this.sceneName = sceneName;
        }
    }
}