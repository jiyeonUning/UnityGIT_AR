using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultyImageTracker : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;
    [SerializeField] GameObject TossCardPrefab;
    [SerializeField] GameObject OtherCardPrefab;

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImageChange;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChange;
    }

    void OnImageChange(ARTrackedImagesChangedEventArgs args)
    {
        // 새로운 이미지가 추적되었을 때,
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // 이미지 라이브러이에서 이미지의 이름을 확인하고,
            string imageName = trackedImage.referenceImage.name;

            // 새로운 게임 오브젝트를 트래킹한 이미지의 자식으로 생성한다.
            switch (imageName)
            {
                case "TossCard":
                    GameObject Tosscard = Instantiate(TossCardPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    Tosscard.transform.parent = trackedImage.transform;
                    break;
                case "card":
                    GameObject Card = Instantiate(OtherCardPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    Card.transform.parent = trackedImage.transform;
                    break;
            }
        }

        // 기존의 이미지가 변경(이동 or 회전)되었을 때,
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // 이미지의 변경사항이 있는 경우, 자식으로 있던 게임 오브젝트의 위치와 회전을 갱신한다.
            trackedImage.transform.GetChild(0).position = transform.transform.position;
            trackedImage.transform.GetChild(0).rotation = transform.transform.rotation;
        }

        // 기존의 이미지가 사라졌을 때,
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            // 이미지가 사라진 경우, 자식으로 있었던 게임 오브젝트를 삭제한다.
            Destroy(trackedImage.transform.GetChild(0).gameObject);
        }
    }
}
