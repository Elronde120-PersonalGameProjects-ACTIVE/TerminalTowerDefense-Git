using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ConsoleTowerDefense.AI
{ 
    public class AIBaseController : MonoBehaviour
    {
        public AIData baseData;
        public TextMeshProUGUI healthText;
        private IAIMovementProvider movement;
        private IAIPathProvider pathGetter;
        private Vector2Int[] path;
        private int currentPathTarget;
        private int currentNavigationTick = 0;
        private int currentHealth = 0;

        // Start is called before the first frame update
        void Start()
        {
            movement = GetComponent<IAIMovementProvider>();
            pathGetter = GetComponent<IAIPathProvider>();

            if(movement == null){
                Debug.LogError("ERROR: " + this.gameObject.name + " does not have an IAIMovment interface script on it. It cannot preform any movement!");
            }else{
                TimeTickSystem.onTick += OnTick;
            }

            if(pathGetter == null){
                Debug.LogError("ERROR: " + this.gameObject.name + " does not have an IAIPathGetter interface script on it. It cannot preform any movement!");
            }else{
                path = pathGetter.GetPath();
            }

            currentHealth = baseData.startingHealth;
            SetHealthText();
        }

        void OnTick(object sender, TimeTickSystem.OnTickEventArgs args){
            if(movement == null || pathGetter == null)
                return;
            currentNavigationTick += 1;

            if(currentNavigationTick % baseData.navigationTickInterval == 0){
                movement.Move(path, ref currentPathTarget, baseData.moveSpeed);             
                currentNavigationTick = 0;
            }
        }

        public void TakeDamage(int damageAmount)
        {
            Debug.Log("taking damage");
            currentHealth -= damageAmount;
            SetHealthText();

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            TimeTickSystem.onTick -= OnTick;
        }

        private void SetHealthText()
        {
            healthText.text = currentHealth.ToString();
        }
    }
}
