using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace IsometricGrid.Data
{
    public class JsonData
    {
        public TileProperty[][] TerrainGrid { get; set; }
    }

    public class TileProperty
    {
        public int TileType { get; set; }
    }

    public class json_reader : MonoBehaviour
    {
        public string filePath;

        void Start()
        {
            string json = File.ReadAllText(filePath);
            JsonData terrainGrid = JsonConvert.DeserializeObject<JsonData>(json);
            if (terrainGrid == null || terrainGrid.TerrainGrid == null)
            {
                Debug.LogError("Failed to parse JSON data.");
                return;
            }
            int rowCount, coloumn = 0;
            rowCount = 0;
            foreach (var row in terrainGrid.TerrainGrid)
            {
                rowCount++;
                coloumn = 0;
                foreach (var tile in row)
                {
                    coloumn++;
                   // Debug.Log(tile.TileType);
                }
            }
            //tileGenerator.Generate(terrainGrid);
             print("Rows: " + rowCount + " Coloumns: " + coloumn);

        }
    }
}