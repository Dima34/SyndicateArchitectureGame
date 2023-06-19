using System;
using System.Collections;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class WorldData
    {
        [SerializeField] private PositionOnLevel _positionOnLevel;
        [SerializeField] private LootData _lootData;
        [SerializeField] private UnEarnedLootpiecesData _unEarnedLootpieces;
        
        public PositionOnLevel PositionOnLevel
        {
            get => _positionOnLevel;
            set => _positionOnLevel = value;
        }

        public LootData LootData
        {
            get => _lootData;
            set => _lootData = value;
        }

        public UnEarnedLootpiecesData UnEarnedLootpieces
        {
            get => _unEarnedLootpieces;
            set => _unEarnedLootpieces = value;
        }

        public WorldData()
        {
            _positionOnLevel = new PositionOnLevel();
            _unEarnedLootpieces = new UnEarnedLootpiecesData();
            _lootData = new LootData();
        }
    }
}