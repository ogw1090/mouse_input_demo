using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public List<MeshRenderer> meshRenderers;
    Plane groundPlane;

    Ray ray;
    float rayDistance;

    void Start()
    {
        groundPlane = new Plane(Vector3.up, 0f);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0)) // ���N���b�N����������
        {
            Vector3 p = GetCursorPosition3D();
            Debug.Log(p);

            foreach (MeshRenderer m in meshRenderers)
            {
                m.material.SetVector("_Vector3", p); // �}�E�X�N���b�N�������W
                Debug.Log("      "+ m.material.GetVector("_Vector3"));
            }

            StartCoroutine("ExpandCircle");
        }
    }

    IEnumerator ExpandCircle()
    {
        float n = 0;

        while (true)
        {
            foreach (MeshRenderer m in meshRenderers)
            {
                m.material.SetFloat("_Float", n); // �����ɑ����l���Z�b�g
            }

            //gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Float", n);

            n -= 0.5f; // �����ɑ����l�����炷

            yield return new WaitForSeconds(0.01f);

            if (n < -20f) // �\���ɉ~���L����ƏI��
            {
                foreach (MeshRenderer m in meshRenderers)
                {
                    m.material.SetFloat("_Float", 2); // �~�̉��𒆐S�֖߂�
                }

                //gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Float", 2);


                yield break;
            }
        }
    }

    Vector3 GetCursorPosition3D()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//�}�E�X�J�[�\������J���������������ւ̃��C
        groundPlane.Raycast(ray, out rayDistance);//���C���΂�

        return ray.GetPoint(rayDistance);//Plane�ƃ��C���Ԃ������_�̍��W��Ԃ�
    }


}