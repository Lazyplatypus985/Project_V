using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    //Current stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    // exp method
    [Header("exp/lvl")]
    public int exp = 0;
    public int level = 1;
    public int expCap;

    [System.Serializable]
    public class LevelRange 
    {
        public int startLevel;
        public int endLevel;
        public int expCapIncrease;
    }

    public List<LevelRange> levelRanges;

    void Awake()
    {
        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMoveSpeed = playerData.MoveSpeed;
        currentMight = playerData.Might;
        currentProjectileSpeed = playerData.ProjectileSpeed;
    }


    void Start() 
    {
        expCap = levelRanges[0].expCapIncrease;

    }

    public void IncreaseExp(int amount) 
    {
        exp += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (exp >= expCap) 
        {
            level++;
            exp -= expCap;

            int expCapIncrease = 0;
            foreach (LevelRange range in levelRanges) 
            { 
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    expCapIncrease = range.expCapIncrease;
                    break;
                }
            }
            expCap += expCapIncrease;
        }
    }



    void Update()
    {
        
    }
}
