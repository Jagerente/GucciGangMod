using UnityEngine;

public class BTN_START_SINGLE_GAMEPLAY : MonoBehaviour
{
    private void OnClick()
    {
        var selection = GGM.Caching.GameObjectCache.Find("PopupListMap").GetComponent<UIPopupList>().selection;
        var str2 = GGM.Caching.GameObjectCache.Find("PopupListCharacter").GetComponent<UIPopupList>().selection;
        var num = !GGM.Caching.GameObjectCache.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? !GGM.Caching.GameObjectCache.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? 0 : 2 : 1;
        IN_GAME_MAIN_CAMERA.difficulty = num;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.SINGLE;
        IN_GAME_MAIN_CAMERA.singleCharacter = str2.ToUpper();
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
        {
            Screen.lockCursor = true;
        }
        Screen.showCursor = false;
        if (selection == "trainning_0")
        {
            IN_GAME_MAIN_CAMERA.difficulty = -1;
        }
        FengGameManagerMKII.level = selection;
        Application.LoadLevel(LevelInfo.getInfo(selection).mapName);
    }
}

