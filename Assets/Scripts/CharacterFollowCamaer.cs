using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class cama : MonoBehaviour
{

	// �ҵı���ͼƬΪ���� y ��ԳƵ�
	// ��������ӳ�ʱ��
	public float smoothTime = 0.2f;
	// ����ͼƬ
	public SpriteRenderer bgBounds;
	// ��ɫ
	public Transform target;
	// ���ڻ��������
	private Vector3 velocity = Vector3.one;
	// �����
	private Camera mainCamera;
	// ����ͼƬ�Ŀ��
	private float bgWidth;
	// ����ͼƬ�Ŀ��
	private float bgHeight;

	private void Start()
	{
		// �õ������
		mainCamera = GetComponent<Camera>();
		// ��ȡ����ͼƬ���
		bgWidth = bgBounds.sprite.bounds.size.x * bgBounds.transform.localScale.x;
		// ��ȡ����ͼƬ���
		bgHeight = bgBounds.sprite.bounds.size.y * bgBounds.transform.localScale.y;
	}
	// �� Update ��һִ֡��
	void LateUpdate()
	{
		// �������һ����
		float hight = mainCamera.orthographicSize;
		// �������һ��߶ȣ�����ֱ��ʵĿ�߱ȳ�����
		// �ø߶ȳ��Էֱ��ʵĿ�߱ȵõ����
		float width = hight * Screen.width / Screen.height;
		// Ҫ�ƶ�����λ��
		Vector3 temp;
		// ����ɫ�ĺ����� + �������һ���ȴ��ڱ���ͼƬ��һ���ȣ���Ϊ���˱߽磬��������Ŀ����Ϊ�ٽ��
		//if (width + Mathf.Abs(target.position.x) > bgWidth / 2)
		if ((width + Mathf.Abs(target.position.x) > bgWidth))
		{
			temp = new Vector3(Mathf.Sign(target.position.x) * (bgWidth - width), target.position.y, transform.position.z);
		}
		else
		{
			// ������Ϊ�����ƶ�
			temp = new Vector3(target.position.x, target.position.y, transform.position.z);
		}
		if ((hight + Mathf.Abs(target.position.y) > bgHeight))
		{
			temp = new Vector3(temp.x, Mathf.Sign(target.position.y) * (bgHeight - hight), transform.position.z);
		}
		else
		{
			// ������Ϊ�����ƶ�
			temp = new Vector3(temp.x, target.position.y, transform.position.z);
		}

		Debug.Log("position��x��" + bgBounds.transform.position.x + "��y��" + bgBounds.transform.position.y);
		//����ٽ�
		//ͼƬ�߽�
		//С��ͼƬ�ĵױ�
		//�ܺý��ͣ�����һ���������λ�ã������������޾����������������
		//���޾������������ͼƬ���������ұߵ�λ�ã����������λ�õ������λ�ò��Ҽ����Լ����������ҵ�һ�볤��
		//x�Ĵ˴��Ӽ�2����Ϊ����������ͼ���ڵ�
		temp.y = temp.y <= bgBounds.transform.position.y - bgHeight / 2 + hight ? bgBounds.transform.position.y - bgHeight / 2 + hight : temp.y;
		temp.y = temp.y >= bgBounds.transform.position.y + bgHeight / 2 - hight ? bgBounds.transform.position.y + bgHeight / 2 - hight : temp.y;
		temp.x = temp.x <= bgBounds.transform.position.x - bgWidth / 2 + width ? bgBounds.transform.position.x - bgWidth / 2 + width - 2 : temp.x;
		temp.x = temp.x >= bgBounds.transform.position.x + bgWidth / 2 - width ? bgBounds.transform.position.x + bgWidth / 2 - width + 2 : temp.x;
		// ʵ����������ӳ��ƶ�

		transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime);


	}
}
