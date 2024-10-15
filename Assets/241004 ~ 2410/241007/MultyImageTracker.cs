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
        // ���ο� �̹����� �����Ǿ��� ��,
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // �̹��� ���̺귯�̿��� �̹����� �̸��� Ȯ���ϰ�,
            string imageName = trackedImage.referenceImage.name;

            // ���ο� ���� ������Ʈ�� Ʈ��ŷ�� �̹����� �ڽ����� �����Ѵ�.
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

        // ������ �̹����� ����(�̵� or ȸ��)�Ǿ��� ��,
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            // �̹����� ��������� �ִ� ���, �ڽ����� �ִ� ���� ������Ʈ�� ��ġ�� ȸ���� �����Ѵ�.
            trackedImage.transform.GetChild(0).position = transform.transform.position;
            trackedImage.transform.GetChild(0).rotation = transform.transform.rotation;
        }

        // ������ �̹����� ������� ��,
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            // �̹����� ����� ���, �ڽ����� �־��� ���� ������Ʈ�� �����Ѵ�.
            Destroy(trackedImage.transform.GetChild(0).gameObject);
        }
    }
}
