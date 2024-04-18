using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using IsometricGrid.Data;
using System;

namespace IsometricGrid.DataReader
{
    public class Json_Reader : MonoBehaviour
    {
        [SerializeField] private string FilePath;
        private string _json;
        public GridData GridDataa;

        public static Json_Reader instance;

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            _json = File.ReadAllText(Application.dataPath + FilePath);
            Debug.LogError(_json);
            GridDataa = JsonConvert.DeserializeObject<GridData>(_json);
        }
    }
}