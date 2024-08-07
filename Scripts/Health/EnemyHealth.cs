using Scripts.Controllers;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts
{
    public class EnemyHealth : MonoBehaviour, ITakeDamage, IDie
    {
        public EnemyController enemy;
        [Header(("Health"))]
        public FloatData maxHp;
        public float curHp;
        public Slider slider;
        public Color low;
        public Color high;
        //public Vector3 Offset;
        public float xOffset;
        public float yOffset;
    
        [Header ("Loot Drop")]
        public InstancerDataList lootDrop;
        [SerializeField]
        private UnityEvent deathEvent;

        private void Start()
        {
            curHp = maxHp.value;
            SetHealth(curHp);
        }
        void Update()
        {
            transform.position = new Vector3(enemy.transform.position.x + xOffset, enemy.transform.position.y + yOffset, transform.position.z); //Setting the position of the health bar 
        }
        public void SetMaxHealth(float health, float maxHealth)
        {
            slider.gameObject.SetActive(health < maxHealth);
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(float health)
        {
        
            slider.value = health;
            slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue); //Changing the color of the health from max to low health
        }

        public void TakeDamage(FloatData damage)
        {
            curHp -= damage.value;
            SetHealth(curHp);
        
            if(curHp <= 0)
            {
                Die();
                LootDrop();
            }
        }

        public void Die()
        {
            Debug.Log("Enemy Has Died!!!!");
            deathEvent.Invoke();
        }
        public void LootDrop()
        {
            if (lootDrop == null)
            {
                return;
            }
            //int lootIndex =Random.Range(0, lootDrop);
            Debug.Log("Loot Drop");
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }
}
