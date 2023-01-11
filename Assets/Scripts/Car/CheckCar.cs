using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCar : MonoBehaviour
{
    CarMovement carMVM;
    public Sprite onLightSprite; //Đèn sáng
    public Sprite offLightSprite; //Đèn tắt
    private LineRenderer lineDrift; //Đường vẽ từ đèn đến xe
    public Transform carComing; //Xe đang đến
    private bool isCarComing; //Check xe đến gần đoạn cua hay chưa

    // Start is called before the first frame update
    void Start()
    {
        isCarComing = false;
        carMVM = FindObjectOfType<CarMovement>();
        lineDrift = GetComponent<LineRenderer>();
        carComing = GameObject.Find("Car").GetComponent<Transform>();
        lineDrift.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCarComing && carMVM.pressControlStatus == true){
            lineDrift.SetPosition(0,this.transform.position); //Điểm vẽ bắt đầu
            lineDrift.SetPosition(1,carComing.transform.position); //Điểm vẽ kết thúc
        }else{
            lineDrift.SetPosition(0,this.transform.position); //Không vẽ nữa
            lineDrift.SetPosition(1,this.transform.position);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) //Phát hiện xe vào cua
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = onLightSprite; //Đèn thông báo bật
        isCarComing = true;
        
    }
    private void OnTriggerExit2D(Collider2D other) //Phát hiện xe ra khỏi cua
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = offLightSprite; //Đèn thông báo tắt
        isCarComing = false;
    }

}
