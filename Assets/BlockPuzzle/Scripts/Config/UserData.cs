using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;

namespace BlockPuzzle
{
    [System.Serializable]
    public class UserData : PlayerData
    {
        public int fishLevel = 1;

        public int energy;

        public int currentMergeLevel;

        public int[] slotsMergeData = new int[] { 1, 0, 0 };


        public bool isFirstTimePlay = true;
        public bool hasMergeTutorial = true;
        public bool hasCombatTutorial = true;


        public int tutorialIndex = 1;
        //public bool hasTutorial = true;

        public bool isMusicOn = true;
        public bool isSoundOn = true;
        public bool isVibrateOn = true;

        public string[] UnlockSkins = new string[] { "1" };

        public string SkinInUse = "1";

        public int[] buyCount = new int[] { 0 };

        public int[] lastBuyCost = new int[] { 0 };

        public void CheckNullableObject()
        {
            if (UnlockSkins == null)
            {
                UnlockSkins = new string[] { "1" };
            }
            if (slotsMergeData == null)
            {
                slotsMergeData = new int[] { 1, 0, 0 };
            }
            if (buyCount == null)
            {
                buyCount = new int[] { 0 };
            }
            if (lastBuyCost == null)
            {
                lastBuyCost = new int[] { 0 };

            }
            //currentMergeLevel = 0;
            //UnlockSkins = new string[] { "1" };
            //slotsMergeData = new int[] { 1, 0, 0, 0 };
            //buyCount = new int[] { 0 };
            //lastBuyCost = new int[] { 0 };
        }

        public bool HasSkin(string skinID)
        {
            for (int i = 0; i < UnlockSkins.Length; i++)
            {
                if (skinID == UnlockSkins[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void AddSkin(string skinID)
        {
            if (!HasSkin(skinID))
            {
                UnlockSkins = UnlockSkins.AddItemToArray(skinID);
            }
        }
        public void AddBuyCount(int level, int price)
        {
            buyCount[level - 1]++;
            lastBuyCost[level - 1] = price;
        }
        public UserData(UserData playerData)
        {
            _currentSkinId = playerData._currentSkinId;
            userName = playerData.userName;
            isFirstTimePlay = playerData.isFirstTimePlay;
            currentMergeLevel = playerData.currentMergeLevel;


            _currentWalkingId = playerData._currentWalkingId;
            _currentRunningId = playerData._currentRunningId;
            _currentCoin = playerData._currentCoin;
            _currentDiamond = playerData._currentDiamond;
            _musicVolumeSettings = playerData._musicVolumeSettings;
            _soundFXVolumeSettings = playerData._soundFXVolumeSettings;
            _isNotificationOn = playerData._isNotificationOn;
            _isLiked = playerData._isLiked;
            _isRated = playerData._isRated;
            _qualitySettingsIndex = playerData._qualitySettingsIndex;
            _joystickSettings = playerData._joystickSettings;
            isGuest = playerData.isGuest;
            _highTravel = playerData._highTravel;
            _hightCoinEarned = playerData._hightCoinEarned;
            _highDiamondEarned = playerData._highDiamondEarned;
            _purchasedWalkingAnims = playerData._purchasedWalkingAnims;
            _purchasedRunningAnims = playerData._purchasedRunningAnims;
            _purchasedCharacterSkin = playerData._purchasedCharacterSkin;
        }

        public UserData()
        {
            _playerId = "guest-01";
            userName = "Player";

            isFirstTimePlay = true;
            hasMergeTutorial = true;
            hasCombatTutorial = true;
            energy = 60;
            currentMergeLevel = 1;
            tutorialIndex = 1;
            fishLevel = 1;
            _currentSkinId = 1207;
            _currentWalkingId = 1;
            _currentRunningId = 2;
            _currentCoin = 100;
            _currentDiamond = 0;
            _musicVolumeSettings = 1f;
            _soundFXVolumeSettings = 1f;
            _isNotificationOn = true;
            _qualitySettingsIndex = 2;
            _joystickSettings = 1;
            isGuest = true;
            _isLiked = false;
            _isRated = false;
            _highTravel = 0;
            _hightCoinEarned = 0;
            _highDiamondEarned = 0;
            _purchasedWalkingAnims = new int[1] { 1 };
            _purchasedRunningAnims = new int[1] { 2 };
            _purchasedCharacterSkin = new int[1] { 1207 };
        }

    }

}
