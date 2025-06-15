using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class SoundChanger : MonoBehaviour
    {
        private const string MUSIC_VOLUME = "Music";
        private const string MASTER_VOLUME = "Master";
        private const string SFX_VOLUME = "SFX";

        [SerializeField] private Slider masterScrollbar;
        [SerializeField] private Slider sfxScrollbar;
        [SerializeField] private Slider musicScrollbar;
        [SerializeField] private AudioMixer mixer;

        private void Start()
        {
            masterScrollbar.value = PlayerPrefs.GetFloat(MASTER_VOLUME, 1f);
            sfxScrollbar.value = PlayerPrefs.GetFloat(SFX_VOLUME, 1f);
            musicScrollbar.value = PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f);

            ChangeMaster(masterScrollbar.value);
            ChangeMusic(musicScrollbar.value);
            ChangeSFX(sfxScrollbar.value);

            masterScrollbar.onValueChanged.AddListener(ChangeMaster);
            sfxScrollbar.onValueChanged.AddListener(ChangeSFX);
            musicScrollbar.onValueChanged.AddListener(ChangeMusic);

            gameObject.SetActive(false);
        }

        public void ChangeMaster(float value)
        {
            if (value > 0)
                mixer.SetFloat(MASTER_VOLUME, Mathf.Log10(value) * 30);
            else
                mixer.SetFloat(MASTER_VOLUME, -80);

            PlayerPrefs.SetFloat(MASTER_VOLUME, masterScrollbar.value);
        }
        public void ChangeSFX(float value)
        {
            if (value > 0)
                mixer.SetFloat(SFX_VOLUME, Mathf.Log10(value) * 30);
            else
                mixer.SetFloat(SFX_VOLUME, -80);

            PlayerPrefs.SetFloat(SFX_VOLUME, sfxScrollbar.value);
        }
        public void ChangeMusic(float value)
        {
            if (value > 0)
                mixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(value) * 30);
            else
                mixer.SetFloat(MUSIC_VOLUME, -80);

            PlayerPrefs.SetFloat(MUSIC_VOLUME, musicScrollbar.value);
        }
    }
}