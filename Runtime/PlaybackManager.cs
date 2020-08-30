using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valax321.AudioSystem.Utility;

namespace Valax321.AudioSystem
{
    [HelpURL("https://github.com/Valax321/AudioSystem/Documentation~/PlaybackManager.md")]
    [AddComponentMenu("Audio/Audio System/Playback Manager (DO NOT ADD)")]
    public class PlaybackManager : MonoBehaviour
    {
        private static PlaybackManager s_instance;
        
        public static PlaybackManager instance
        {
            get
            {
                if (s_instance)
                    return s_instance;
                
                var go = new GameObject("[Audio System Playback Manager]", typeof(PlaybackManager));
                s_instance = go.GetComponent<PlaybackManager>();
                DontDestroyOnLoad(go);

                return s_instance;
            }
        }

        private Pool<AudioEmitter> m_emitterPool;

        private void Awake()
        {
            m_emitterPool = new Pool<AudioEmitter>(100, MakeEmitterInstance);
        }

        private AudioEmitter MakeEmitterInstance()
        {
            var emitter = new GameObject("Audio Emitter", typeof(AudioEmitter), typeof(AudioSource))
            {
                hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector
            };
            DontDestroyOnLoad(emitter);
            return emitter.GetComponent<AudioEmitter>();
        }
    }
}
