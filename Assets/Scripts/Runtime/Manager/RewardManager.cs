using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Manager
{
    public class RewardManager : MonoBehaviour
    {
        
        private Dictionary<string, int> rewards = new Dictionary<string, int>();

       
       [SerializeField] private Transform rewardPanel; 
       [SerializeField] private GameObject rewardIconPrefab;

        private void OnEnable()
        {
            WheelGameSignals.Instance.onAddReward += AddReward;
        }

        public void AddReward(string rewardName, int amount, Sprite rewardIcon)
        {
            
            if (rewards.ContainsKey(rewardName))
            {
                
                rewards[rewardName] += amount;

              
                UpdateExistingReward(rewardName);
            }
            else
            {
            
                rewards[rewardName] = amount;

               
                AddNewRewardIcon(rewardName, amount, rewardIcon);
            }
        }

        
        private void UpdateExistingReward(string rewardName)
        {
            
            
            foreach (Transform reward in rewardPanel)
            {
                if (reward.name == rewardName)
                {
                   
                    TextMeshProUGUI rewardText = reward.GetComponentInChildren<TextMeshProUGUI>();
                    rewardText.text = rewards[rewardName].ToString(); 
                }
            }
        }

        
        private void AddNewRewardIcon(string rewardName, int amount, Sprite rewardIcon)
        {
            
            GameObject newIcon = Instantiate(rewardIconPrefab, rewardPanel);
            newIcon.name = rewardName; 
        
            
            Image iconImage = newIcon.GetComponentInChildren<Image>();
            iconImage.sprite = rewardIcon;

            
            TextMeshProUGUI rewardText = newIcon.GetComponentInChildren<TextMeshProUGUI>();
            rewardText.text = amount.ToString(); 
        }

        private void OnDisable()
        {
            WheelGameSignals.Instance.onAddReward -= AddReward;
        }
    }
    
}
