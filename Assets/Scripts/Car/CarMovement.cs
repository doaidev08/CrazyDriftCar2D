
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CarMovement : MonoBehaviour
{
    //Main Road
    [SerializeField] private LayerMask roadLeft;
    [SerializeField] private LayerMask roadUp;
    [SerializeField] private LayerMask roadRight;
    [SerializeField] private LayerMask roadDown;
    //Corner Road
    [SerializeField] private LayerMask cornerTurnLeft;
    [SerializeField] private LayerMask cornerTurnRight;
    private float time = .5f;
    public bool pressControlStatus; //Check Control button is pressed ?
   
    float pressControlFactor ; //Control Car will turn left or turn right (-1: left; 1: right)
    private float torqueForce = -150f; // Lực xoay xe (lực mômen xoắn)
    private float speed = 15f; //Tốc độ di chuyển
    public Rigidbody2D car;
    private float driftFactorSticky = 0.8f; //Hệ số drift khi mới bắt đầu vào cua
    private float driftFactorSlippy = 1f; //Hệ số drift tạo lực ly tâm tốt hơn (ra xa khỏi tâm)
    //  => Slippy > Sticky 
    private float maxStickyVelocity = .2f; //Vị trí cần thêm vào lực ly tâm

    float momenForce;
    
    void Start(){
        pressControlStatus = false;
        car = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision){ //Thua
        if(collision.gameObject.tag == "corner" || collision.gameObject.tag == "side"){
            PlayerManager.gameOver = true;
        }
    }


    void FixedUpdate(){
            if (!PlayerManager.isGameStarted || PlayerManager.gameOver) return;
            time += Time.deltaTime;
            PlayerManager.score = (int)time;
            float driftFactor = driftFactorSticky; //Hệ số drift ban đầu 
        
                car.AddForce(transform.up * speed); //Xe di chuyển hướng về trước
         
            
            if(RightVelocity().magnitude >maxStickyVelocity){    
                driftFactor = driftFactorSlippy; 
            }
            car.velocity = ForwardVelocity() + RightVelocity() * driftFactor; //vận tốc của xe vào cua
    
            //Raycast hit Main Road
            RaycastHit2D hitRight= Physics2D.Raycast(this.transform.position, -transform.right, 3f, roadRight);
            RaycastHit2D hitUp= Physics2D.Raycast(this.transform.position, -transform.right, 3f, roadUp);
            RaycastHit2D hitLeft= Physics2D.Raycast(this.transform.position, -transform.right, 3f, roadLeft);
            RaycastHit2D hitDown= Physics2D.Raycast(this.transform.position, -transform.right, 3f, roadDown);

            //Raycast hit Corner Road
            RaycastHit2D hitCornerLeft= Physics2D.Raycast(this.transform.position, transform.up, 3f, cornerTurnLeft);
            RaycastHit2D hitCornerRight= Physics2D.Raycast(this.transform.position, transform.up, 3f, cornerTurnRight);

            //Turn left or turn right at corner when Control Button is pressed
            // pressControlFactor = -1 -> turn left
            // pressControlFactor =  1 -> turn right
            // Do not press button -> Not turning -> pressControlFactor = 0
            if(pressControlStatus){
                if(!hitRight && !hitLeft && !hitUp && !hitDown){ // If car at Corner Road (= Ray cast not hitting Main Road)
                    if(hitCornerLeft){
                        pressControlFactor = -1;
                    }else if(hitCornerRight){
                        pressControlFactor = 1;
                    }
                }
            }else{
                pressControlFactor = 0;
            }

            momenForce = pressControlFactor * torqueForce; //Lực momenxoay
            car.angularVelocity = momenForce; //vận tốc xoay của xe

            //Balance Car to Horizontal or vertical of line
            //right : down to up
            //up: right to left
            //left: up to down
            //down: left to right

            if(hitRight){
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, 0), Time.fixedDeltaTime *8f); // cân bằng xe hướng down - up
            }
            else if(hitUp){
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, 90), Time.fixedDeltaTime *8f); // cân bằng xe hướng right - left
            }
            else if(hitLeft){
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, 180), Time.fixedDeltaTime *8f); //cân bằng xe hướng up - down
            }
            else if(hitDown){
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, -90), Time.fixedDeltaTime *8f); //cân bằng xe hướng left - right
            }
           
            
    }
    
    public void PressDrift(){  //Button Drift Control is Pressed
        pressControlStatus = true;
    }
    public void ReleaseDrift(){ //Button Drift Control is Release
        pressControlStatus = false;
    }

    Vector2 ForwardVelocity(){
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up); //Độ lớn vận tốc hướng trước khi vào cua
    }

    Vector2 RightVelocity(){
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right); //Độ lớn vận tốc hướng phải khi vào cua
    }
    // => Khi vào cua, vận tốc xe = v xe hướng trước + v xe hướng phải * hệ số drift
    //Dot -> Dot Products (tích vô hướng của 2 vector)
    
}
 // Debug.DrawRay(hitRight.point,hitRight.normal,Color.blue);
