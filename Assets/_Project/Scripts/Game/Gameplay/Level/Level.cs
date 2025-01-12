using System;
using UnityEngine;

namespace _Project.Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private BaseChunk[] _chunks;
        [SerializeField] private FinishChunk _finishChunk;

        public FinishChunk FinishChunk => _finishChunk;

        private void OnValidate()
        {
            foreach (var chunk in _chunks)
            {
                if(chunk is FinishChunk)
                    throw new Exception($"FinishChunk must be in a separate reference and cannot be part of BaseChunks.");
            }
        }
    }
}