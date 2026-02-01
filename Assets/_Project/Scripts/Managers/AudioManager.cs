using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region　シングルトン

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [Header("ミキサー")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("オーディオソース")]
    [SerializeField] private AudioSource seAudioSource;

    [Header("スライダー")]
    [SerializeField] private Slider bgmSlider;
    //[SerializeField] private Slider seSlider;

    private void Start()
    {
        // 各スライダーにリスナーを設定
        SetupSlider(bgmSlider, "BGM", "BGM_VOLUME");
        //SetupSlider(seSlider, "SFX", "SFX_VOLUME");
    }

    #region スライダーと音量のリンクと保存
    private void SetupSlider(Slider slider, string parameterName, string saveKey)
    {
        if (slider != null)
        {
            float volume = PlayerPrefs.GetFloat(saveKey, 1f);
            slider.value = volume;
            SetVolume(parameterName, volume);
            slider.onValueChanged.AddListener(value => SetVolume(parameterName, value));
        }
    }

    #endregion

    #region 音量
    private void SetVolume(string parameterName, float value)
    {
        value = Mathf.Clamp01(value);  // 0～1の範囲に制限
        float decibel = Mathf.Clamp(20f * Mathf.Log10(value), -80f, 0f);
        audioMixer.SetFloat(parameterName, decibel);

        // 変更した音量を保存
        if (parameterName == "BGM")
        {
            PlayerPrefs.SetFloat("BGM_VOLUME", value);
        }
        else if (parameterName == "SFX")
        {
            PlayerPrefs.SetFloat("SFX_VOLUME", value);
        }
    }
    #endregion

}
