using DG.Tweening;
using Runtime.Data.UnityObject;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

namespace Runtime.Controllers
{
    public class SpinController : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private Transform currentZoneImage;
        [SerializeField] private Transform wheel;
        [SerializeField] private CD_WheelData wheelSlice;
        [SerializeField] private TextMeshProUGUI spinText;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Image spinIndicator;
        [SerializeField] private float spinDuration = 3f;
        [SerializeField] private byte currentZone = 1;

        #endregion

        private void OnEnable()
        {
            WheelGameSignals.Instance.onSpin += SpinWheel;
            WheelGameSignals.Instance.onNextZone += NextZone;
        }


        private void SpinWheel()
        {
            if (currentZone > currentZoneImage.transform.childCount) return;
            WheelGameSignals.Instance.onGameObjectList?.Invoke(0, false);
            WheelGameSignals.Instance.onGameObjectList?.Invoke(1, false);
            float randomAngle = Random.Range(90f, 360f);
            wheel.DOLocalRotate(new Vector3(0, 0, randomAngle) * 10, spinDuration,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutCubic)
                .OnComplete(DetermineResult);
        }

        void DetermineResult()
        {
            float zRotation = wheel.eulerAngles.z;

            float snappedRotation = Mathf.Round(zRotation / 45f) * 45f;
            wheel.DORotate(new Vector3(0, 0, snappedRotation), 0.5f).SetEase(Ease.Linear);


            int totalSlices = wheelSlice.WheelSlicesData.Count;
            float sliceAngle = 360f / totalSlices;

            int sliceIndex = Mathf.RoundToInt(zRotation / sliceAngle) % wheelSlice.WheelSlicesData.Count;

            if (wheelSlice.WheelSlicesData[sliceIndex].isBomb)
            {
                resultText.text = wheelSlice.WheelSlicesData[sliceIndex].rewardedText;
                WheelGameSignals.Instance.onGameObjectList?.Invoke(2, true);
            }
            else
            {
                WheelGameSignals.Instance.onAddReward?.Invoke(wheelSlice.WheelSlicesData[sliceIndex].sliceName,
                    wheelSlice.WheelSlicesData[sliceIndex].rewardPoints,
                    wheelSlice.WheelSlicesData[sliceIndex].spriteObject);
                resultText.text = wheelSlice.WheelSlicesData[sliceIndex].rewardedText;
                WheelGameSignals.Instance.onGameObjectList?.Invoke(1, true);
                DOVirtual.DelayedCall(1f, () => { NextZone(); });
            }
        }

        private void NextZone()
        {
            wheel.parent.transform.DOScale(0.5f, 0.3f).OnComplete((() =>
            {
                wheel.parent.transform.DOScale(1f, 0.3f);
            }));
            
            resultText.text = "";
            WheelGameSignals.Instance.onGameObjectList?.Invoke(0, false);

            currentZone++;

            currentZoneImage.transform.DOLocalMoveX(currentZoneImage.transform.localPosition.x - 72, 1f);


            var wheelSlices = Resources.Load<CD_WheelData>($"ZonePrefabs/Zone {currentZone}");
            if (wheelSlices != null)
            {
                this.wheelSlice = wheelSlices;
            }

            if (wheel != null)
            {
                wheel.GetComponent<Image>().sprite = wheelSlice.MainObject;
                spinText.text = wheelSlice.SpinName;
                spinIndicator.sprite = wheelSlice.SpinIndicator;
                for (int i = 0; i < wheel.childCount; i++)
                {
                    wheel.GetChild(i).GetComponent<Image>().sprite = wheelSlice.WheelSlicesData[i].spriteObject;
                }

                WheelGameSignals.Instance.onGameObjectList?.Invoke(0, true);
            }
        }

        private void OnDisable()
        {
            WheelGameSignals.Instance.onSpin -= SpinWheel;
            WheelGameSignals.Instance.onNextZone -= NextZone;
        }
    }
}