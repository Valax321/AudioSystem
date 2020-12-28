using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valax321.AudioSystem.Utility;

namespace Valax321.AudioSystem
{
    [AddComponentMenu("")]
    public class PlaybackManager : MonoBehaviour
    {
        /// <summary>
        /// Play a sound event at the given position.
        /// </summary>
        /// <param name="event">The event to play.</param>
        /// <param name="position">The world position to play the sound at.</param>
        /// <param name="playPersistent">Should this sound continue playing between scene loads?</param>
        /// <returns>The <see cref="AudioEmitter"/> instance spawned for this sound.</returns>
        public static AudioEmitter PlayAtPosition(AudioEvent @event, Vector3 position, bool playPersistent = false)
        {
            var instance = MakeEmitterInstance();
            instance.audioEvent = @event;
            instance.transform.position = position;
            if (playPersistent)
            {
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }

        /// <summary>
        /// Play a sound event attached to the given <see cref="Transform"/>.
        /// </summary>
        /// <param name="event">The event to play.</param>
        /// <param name="parent">The <see cref="Transform"/> to attach the audio emitter to.</param>
        /// <param name="offset">Local offset from the transform origin to play the sound at.</param>
        /// <param name="space">The space mode to apply <see cref="offset"/> in.</param>
        /// <returns>The <see cref="AudioEmitter"/> instance spawned for this sound.</returns>
        public static AudioEmitter PlayAttached(AudioEvent @event, Transform parent, Vector3 offset, Space space = Space.Self)
        {
            var instance = MakeEmitterInstance();
            instance.audioEvent = @event;
            var t = instance.transform;
            t.parent = parent;
            switch (space)
            {
                case Space.Self:
                    t.localPosition = offset;
                    break;
                case Space.World:
                    t.position = offset;
                    break;
            }
            return instance;
        }

        private static AudioEmitter MakeEmitterInstance()
        {
            var emitter = new GameObject("Audio Emitter", typeof(AudioEmitter), typeof(AudioSource))
            {
#if !PACKAGE_DEV
                hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector
#endif
            };
            DontDestroyOnLoad(emitter);
            var em = emitter.GetComponent<AudioEmitter>();
            em.playOnStart = true;
            em.destroyGameObjectOnComplete = true;
            return em;
        }
    }

    /// <summary>
    /// Extension methods for playing <see cref="AudioEvent"/>s.
    /// </summary>
    public static class PlaybackManagerExtensionMethods
    {
        /// <inheritdoc cref="PlaybackManager.PlayAtPosition"/>
        public static AudioEmitter PlayAtPosition(this AudioEvent @event, Vector3 position, bool playPersistent = false)
        {
            return PlaybackManager.PlayAtPosition(@event, position, playPersistent);
        }

        /// <inheritdoc cref="PlaybackManager.PlayAttached"/>
        public static AudioEmitter PlayAttached(this AudioEvent @event, Transform parent, Vector3 offset, Space space)
        {
            return PlaybackManager.PlayAttached(@event, parent, offset, space);
        }
    }
}
