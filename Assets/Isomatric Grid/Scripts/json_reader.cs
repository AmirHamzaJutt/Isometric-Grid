using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using IsometricGrid.Data;
namespace IsometricGrid.DataReader
{
    public class Json_Reader : MonoBehaviour
    {
        [SerializeField] private string filePath;
        public GridData GridDataa;

        public int GridRows;
        public int GridCols;

        public static Json_Reader instance;

        private void Awake()
        {
            instance = this;

            // Load the JSON file from the Resources folder
            TextAsset jsonFile = Resources.Load<TextAsset>(filePath);

            if (jsonFile != null)
            {
                // Deserialize JSON data
                GridDataa = JsonConvert.DeserializeObject<GridData>(jsonFile.text);

                // Set grid rows and columns
                GridRows = GridDataa.TerrainGrid.Length;
                GridCols = GridDataa.TerrainGrid[0].Length;
            }
            else
            {
                Debug.LogError("Failed to load JSON file from Resources folder: " + filePath);
            }
        }
    }
}