using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class UnEarnedLootpiecesData
    {
        [SerializeField] private List<LootPieceData> _lootPieces = new List<LootPieceData>();
        
        public List<LootPieceData> LootPieces
        {
            get => _lootPieces;
            set => _lootPieces = value;
        }
    }
}