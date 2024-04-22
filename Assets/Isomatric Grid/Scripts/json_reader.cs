using UnityEngine;
using Newtonsoft.Json;
using IsometricGrid.Data;

namespace IsometricGrid.DataReader
{
    public class Json_Reader : MonoBehaviour
    {
        [SerializeField] private string FilePath;

        [HideInInspector] public GridData GridDataa;
        [HideInInspector] public int GridRows;
        [HideInInspector] public int GridCols;

        public static Json_Reader Instance;

        private void Awake()
        {
            Instance = this;
            TextAsset jsonFile = Resources.Load<TextAsset>(FilePath);
            if (jsonFile != null)
            {
                GridDataa = JsonConvert.DeserializeObject<GridData>(jsonFile.text);
                GridRows = GridDataa.TerrainGrid.Length;
                GridCols = GridDataa.TerrainGrid[0].Length;
            }
            else
            {
                Debug.LogError("Failed to load JSON file from Resources folder: " + FilePath);
            }
        }
    }
}