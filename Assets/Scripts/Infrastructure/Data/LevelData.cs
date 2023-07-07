using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private string _name;
        [SerializeField] private Vector3Data _positionOnLevel;
        [SerializeField] private List<LootPieceData> _unEarnedLootPieces = new List<LootPieceData>();

        public Vector3Data PositionOnLevel
        {
            get => _positionOnLevel;
            set => _positionOnLevel = value;
        }

        public List<LootPieceData> UnEarnedLootPieces
        {
            get => _unEarnedLootPieces;
            set => _unEarnedLootPieces = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public LevelData(string name)
        {
            _name = name;
        }
    }
}