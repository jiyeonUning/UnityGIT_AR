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
        // 추적중인 얼굴에 변경사항 (위치 or 회전)이 있을 때,
        if (args.updated.Count > 0)
        {
            // ARFace를 가져와서,
            ARFace face = args.updated[0];

            // 얼굴에 있는 모든 점을,
            for (int i = 0; i < face.vertices.Length; i++)
            {
                // 얼굴 기준의 위치를 월드위치로 변환하고,
                Vector3 vertPos = face.transform.TransformPoint(face.vertices[i]);
                // 생성한 큐브들을 기준의 위치로 이동한다.
                cubes[i].transform.position = vertPos;
            }
        }
    }
}
