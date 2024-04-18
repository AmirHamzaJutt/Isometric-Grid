using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using IsometricGrid.Data;
using System;

namespace IsometricGrid.DataReader
{
    public class Json_Reader : MonoBehaviour
    {
        private string _json;
        [SerializeField] private string FilePath;
        [HideInInspector] public GridData GridDataa;

        public int GridRows;
        public int GridCol;
        public static Json_Reader instance;

        private void Awake()
        {
            instance = this;
            _json = File.ReadAllText(Application.dataPath + FilePath);
            GridDataa = JsonConvert.DeserializeObject<GridData>(_json);
            //GridRows = GridDataa.TerrainGrid.Length;
            // GridCol = GridDataa.TerrainGrid[0].Length; 
            GridRows = 4;
            GridCol = 7;
        }
        void Start()
        {
           
        }
    }
}