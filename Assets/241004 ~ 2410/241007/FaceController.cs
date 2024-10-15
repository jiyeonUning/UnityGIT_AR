using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceController : MonoBehaviour
{
    [SerializeField] ARFaceManager faceManager;
    [SerializeField] List<GameObject> cubes = new List<GameObject>(468);
    [SerializeField] GameObject cubePrefabs;

    private void Awake()
    {
        for (int i = 0; i < 468; i++)
        {
            GameObject cube = Instantiate(cubePrefabs);
            cubes.Add(cube);
        }
    }

    private void OnEnable()
    {
        faceManager.facesChanged += OnFaceChange;    
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFaceChange;
    }

    void OnFaceChange(ARFacesChangedEventArgs args)
    {
        // �������� �󱼿� ������� (��ġ or ȸ��)�� ���� ��,
        if (args.updated.Count > 0)
        {
            // ARFace�� �����ͼ�,
            ARFace face = args.updated[0];

            // �󱼿� �ִ� ��� ����,
            for (int i = 0; i < face.vertices.Length; i++)
            {
                // �� ������ ��ġ�� ������ġ�� ��ȯ�ϰ�,
                Vector3 vertPos = face.transform.TransformPoint(face.vertices[i]);
                // ������ ť����� ������ ��ġ�� �̵��Ѵ�.
                cubes[i].transform.position = vertPos;
            }
        }
    }
}
