using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    enum SettingButton { BGM, SFX }

    public ButtonState[] settingsButtons;
    // Start is called before the first frame update
    private bool isBgmActive;
    private bool isSfxActive;

    void Awake()
    {
        LoadSettings();
    }

    // Load Game Settings
    public void LoadSettings()
    {

        if (!DataManager.init.GetBoolean(DataConstants.INITAL_LAUNCH))
        {
            DataManager.init.SetBoolean(DataConstants.BGM_STATE, true);
            AudioManager.init.PlayBGMAudio((int)AudioManager.bgmClip.PLAYING);
            DataManager.init.SetBoolean(DataConstants.SFX_STATE, true);
            DataManager.init.SetBoolean(DataConstants.INITAL_LAUNCH, true);
        }

        isBgmActive = DataManager.init.GetBoolean(DataConstants.BGM_STATE);
        isSfxActive = DataManager.init.GetBoolean(DataConstants.SFX_STATE);

        if (isBgmActive)
        {
            AudioManager.init.MuteBGM(false);
            AudioManager.init.PlayBGMAudio((int)AudioManager.bgmClip.PLAYING);
            settingsButtons[(int)SettingButton.BGM].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.toggled);
        }
        else
        {
            AudioManager.init.MuteBGM(true);
            settingsButtons[(int)SettingButton.BGM].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.untoggled);
        }

        if (isSfxActive)
        {
            AudioManager.init.MuteSFX(false);
            settingsButtons[(int)SettingButton.SFX].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.toggled);
        }
        else
        {
            AudioManager.init.MuteSFX(true);
            settingsButtons[(int)SettingButton.SFX].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.untoggled);
        }

    }
    // Mute or unmute BGM.
    public void SoundBgmSetting()
    {
        if (isBgmActive)
        {
            isBgmActive = false;
            DataManager.init.SetBoolean(DataConstants.BGM_STATE, isBgmActive);
            AudioManager.init.MuteBGM(DataManager.init.GetBoolean(DataConstants.BGM_STATE));
            settingsButtons[(int)SettingButton.BGM].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.untoggled);


        }
        else
        {
            isBgmActive = true;
            DataManager.init.SetBoolean(DataConstants.BGM_STATE, isBgmActive);
            AudioManager.init.MuteBGM(isBgmActive);
            AudioManager.init.PlayBGMAudio((int)AudioManager.bgmClip.PLAYING);
            settingsButtons[(int)SettingButton.BGM].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.toggled);

        }

        LoadSettings();

    }

    // Mute or unmute SFX.
    public void SoundSfxSetting()
    {
        if (isSfxActive)
        {
            isSfxActive = false;
            DataManager.init.SetBoolean(DataConstants.SFX_STATE, isSfxActive);
            AudioManager.init.MuteSFX(DataManager.init.GetBoolean(DataConstants.SFX_STATE));
            settingsButtons[(int)SettingButton.SFX].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.untoggled);

        }
        else if (!isSfxActive)
        {
            isSfxActive = true;
            DataManager.init.SetBoolean(DataConstants.SFX_STATE, isSfxActive);
            AudioManager.init.MuteSFX(isSfxActive);
            settingsButtons[(int)SettingButton.SFX].SetButtonGraphic((int)ButtonState.ButtonStateGraphic.toggled);
        }

        LoadSettings();


    }
    






}
