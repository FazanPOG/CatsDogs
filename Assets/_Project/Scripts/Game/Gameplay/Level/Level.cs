using System;
using _Project.Audio;
using UnityEngine;

namespace _Project.Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private BaseChunk[] _chunks;
        [SerializeField] private FinishChunk _finishChunk;

        public FinishChunk FinishChunk => _finishChunk;

        public void Init(AudioPlayer audioPlayer)
        {
            _finishChunk.Init(audioPlayer);
        }
        
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