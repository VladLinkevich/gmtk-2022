using UnityEngine;

namespace Code.Data
{
    public static class Path
    {
        public const string Images = "Image/";
        
        public static class Prefab
        {
            private const string PrefabName = "Prefab/";



            public static class Minimap
            {
                private const string GameName = PrefabName +  "Minimap/";
                
                public const string Navigator = GameName + "Navigator";
                public const string Camera = GameName + "Camera";
                public const string RoadPointer = GameName + "RoadPointer";
            }

            public static class Game
            {
                public const string GameName = PrefabName +  "Game/";

                public const string Character = GameName + "Character";
                public const string CarLine = GameName + "CarLineRenderer";
            }

            public static class Chunk
            {
                private const string ChunkName = "Chunk/";

                public static readonly string[] Chunks = 
                {
                    PrefabName + ChunkName + "Empty",
                    PrefabName + ChunkName + "TrafficLights",
                    PrefabName + ChunkName + "Left",
                    PrefabName + ChunkName + "Right",
                    PrefabName + ChunkName + "Crossroad",
                    PrefabName + ChunkName + "Pedestrian",
                };
            }

            public static class State
            {
                private const string StateName = "State/";

                public const string Bootstrap = StateName + "Bootstrap";
                public const string Preview = StateName + "Preview";
                public const string Runner = StateName + "Runner";
                public const string Finish = StateName + "Finish";
                public const string GameLoop = StateName + "GameLoop";
            }
            
            public static class Example
            {
                private const string StateName = "Example/";
                
                public const string Anchor = PrefabName + StateName + "AnchorExample";
                public const string Connect = PrefabName + StateName + "ConnectExample";
                public const string Image = PrefabName + StateName + "ImageExample";
            }
            
            public static class UI
            {
                private const string UIName = "UI/";
                public const string Window = UIName + "Window/";
                public const string UIRoot = UIName + "UIRoot";
                public const string Win = UIName + "WinScreen";
                public const string Submit = UIName + "Submit_button";
            }
        }
        
        public static class SerializeData
        {
        }
        
        public static class PlayerPrefs
        {
            public const string Progress = "Progress";
            public const string Level = "Level";
        }
    }
}