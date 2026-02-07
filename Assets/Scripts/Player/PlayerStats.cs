using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //Estatus Atuais
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    //Experiência e level
    [Header("Experiência/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    // Classe para definir os ranges de nível e o aumento do experience cap correspondente
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [Header("I-Frames")]
    public float invencibilityDuration;
    float invencibilityTimer;
    bool isInvencible;

    public List<LevelRange> levelRanges;


    private void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    private void Start()
    {
        // Define o experience cap inicial com base no primeiro range de nível
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if (invencibilityTimer > 0)
        {
            invencibilityDuration -= Time.deltaTime;
        }
        // Se o timer de invencibilidade chegar a zero, desativa a invencibilidade
        else if (isInvencible)
        {
            isInvencible = false;
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap) //Aumenta o level se a experiência atual for maior que o cap
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        //Se o player não estiver invencível, aplica o dano e iniciar o timer de invencibilidade
        if (!isInvencible)
        {
            currentHealth -= dmg;

            invencibilityTimer = invencibilityDuration; // Reinicia o timer de invencibilidade
            isInvencible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("Player morreu");
    }

    public void RestoreHealth(float amount)
        {
            currentHealth += amount;

            // Garante que a vida não ultrapasse o máximo permitido
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
}