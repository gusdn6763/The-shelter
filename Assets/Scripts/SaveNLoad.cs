using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Data
{
    public bool bgmOn;
    public bool soundOn;

    public float bgmVolume;
    public float soundVolume;

    public int playerClearStage;
}

public class SaveNLoad : MonoBehaviour
{
    public Data data;

    public void CallSave()
    {
        data.bgmOn = SoundManager.instance.bgmIsOn;
        data.soundOn = SoundManager.instance.soundIsOn;
        data.bgmVolume = SoundManager.instance.audioSourceBgm.volume;
        data.soundVolume = SoundManager.instance.audioSourceEffects[0].volume;
        data.playerClearStage = DatabaseManager.instance.currentPlayerClearStage;

        Debug.Log("기초 데이터 성공");

        BinaryFormatter bf = new BinaryFormatter();                             //파일 변환
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");  //파일 입출력

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (!(File.Exists(Application.dataPath + "/SaveFile.dat")))
        {
            Debug.Log("없음");
            return;
        }

        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            SoundManager.instance.bgmIsOn = data.bgmOn;
            SoundManager.instance.soundIsOn = data.soundOn;
            SoundManager.instance.audioSourceBgm.volume = data.bgmVolume;
            SoundManager.instance.audioSourceEffects[0].volume = data.soundVolume;
            DatabaseManager.instance.currentPlayerClearStage = data.playerClearStage;
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }
        file.Close();
    }
}
