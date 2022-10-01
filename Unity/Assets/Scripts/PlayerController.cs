using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer Target;
    
    private void Update()
    {
        // TODO: Get current mouse position to determine target position
        Vector2 mouse = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        Ray ray;
        ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
         
        if(Physics.Raycast(ray,out hit, 10))
        {
            Target.transform.position = hit.point;
            
            if(hit.point.x < transform.position.x)
                Debug.Log("Left");
            else
                Debug.Log("Right");
        }
    }
}
