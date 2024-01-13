using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Health
{
    public class HealthHeart : MonoBehaviour
    {
        public Sprite emptyHeart, quarterHeart, halfHeart, threeQuarterHeart, fullHeart;
        Image heartImage;

        private void Awake()
        {
            heartImage = GetComponent<Image>();
        }

        public void SetHeartState(HeartState hState)
        {
            switch (hState)
            {
                case HeartState.Empty:
                    heartImage.sprite = emptyHeart;
                    break;
                case HeartState.Quarter:
                    heartImage.sprite = quarterHeart;
                    break;
                case HeartState.Half:
                    heartImage.sprite = halfHeart;
                    break;
                case HeartState.ThreeQuarter:
                    heartImage.sprite = threeQuarterHeart;
                    break;
                case HeartState.Full:
                    heartImage.sprite = fullHeart;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hState), hState, null);
            }
        }
    }
    public enum HeartState
    {
        Empty = 0,
        Quarter = 1,
        Half = 2,
        ThreeQuarter = 3,
        Full = 4
    }
}