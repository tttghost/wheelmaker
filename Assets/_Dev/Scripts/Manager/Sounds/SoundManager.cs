using UnityEngine;
using WheelMaker.Manager.Interfaces;

namespace WheelMaker.Manager.Sounds
{
    {

        {
        }

        public void Play()
        {
            Debug.Log("Play");
            AudioSource.Play();
        }

        public void Stop()
        {
            Debug.Log("Stop");
            AudioSource.Stop();
        }
    }
}