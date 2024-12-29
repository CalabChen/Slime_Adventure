using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class cama : MonoBehaviour
{

	// 我的背景图片为关于 y 轴对称的
	// 摄像机的延迟时间
	public float smoothTime = 0.2f;
	// 背景图片
	public SpriteRenderer bgBounds;
	// 角色
	public Transform target;
	// 用于缓冲摄像机
	private Vector3 velocity = Vector3.one;
	// 摄像机
	private Camera mainCamera;
	// 背景图片的宽度
	private float bgWidth;
	// 背景图片的宽度
	private float bgHeight;

	private void Start()
	{
		// 拿到摄像机
		mainCamera = GetComponent<Camera>();
		// 获取背景图片宽度
		bgWidth = bgBounds.sprite.bounds.size.x * bgBounds.transform.localScale.x;
		// 获取背景图片宽度
		bgHeight = bgBounds.sprite.bounds.size.y * bgBounds.transform.localScale.y;
	}
	// 比 Update 慢一帧执行
	void LateUpdate()
	{
		// 摄像机的一半宽度
		float hight = mainCamera.orthographicSize;
		// 摄像机的一半高度，会随分辨率的宽高比成正比
		// 用高度乘以分辨率的宽高比得到宽度
		float width = hight * Screen.width / Screen.height;
		// 要移动到的位置
		Vector3 temp;
		// 当角色的横坐标 + 摄像机的一半宽度大于背景图片的一半宽度，则为到了边界，把摄像机的宽度设为临界点
		//if (width + Mathf.Abs(target.position.x) > bgWidth / 2)
		if ((width + Mathf.Abs(target.position.x) > bgWidth))
		{
			temp = new Vector3(Mathf.Sign(target.position.x) * (bgWidth - width), target.position.y, transform.position.z);
		}
		else
		{
			// 否则则为正常移动
			temp = new Vector3(target.position.x, target.position.y, transform.position.z);
		}
		if ((hight + Mathf.Abs(target.position.y) > bgHeight))
		{
			temp = new Vector3(temp.x, Mathf.Sign(target.position.y) * (bgHeight - hight), transform.position.z);
		}
		else
		{
			// 否则则为正常移动
			temp = new Vector3(temp.x, target.position.y, transform.position.z);
		}

		Debug.Log("position，x：" + bgBounds.transform.position.x + "，y：" + bgBounds.transform.position.y);
		//添加临界
		//图片边界
		//小于图片的底边
		//很好解释，拦截一下摄像机的位置，如果到这个界限就让他等于这个界限
		//界限就是先算出背景图片的上下左右边的位置，让摄像机的位置等于这个位置并且加上自己的上下左右的一半长度
		//x的此处加减2是因为有误差，根据视图调节的
		temp.y = temp.y <= bgBounds.transform.position.y - bgHeight / 2 + hight ? bgBounds.transform.position.y - bgHeight / 2 + hight : temp.y;
		temp.y = temp.y >= bgBounds.transform.position.y + bgHeight / 2 - hight ? bgBounds.transform.position.y + bgHeight / 2 - hight : temp.y;
		temp.x = temp.x <= bgBounds.transform.position.x - bgWidth / 2 + width ? bgBounds.transform.position.x - bgWidth / 2 + width - 2 : temp.x;
		temp.x = temp.x >= bgBounds.transform.position.x + bgWidth / 2 - width ? bgBounds.transform.position.x + bgWidth / 2 - width + 2 : temp.x;
		// 实现摄像机的延迟移动

		transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime);


	}
}
