using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public int maxJumps = 3;

    private int currentJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        currentJumps = 0;
    }

    public bool IsGrounded()
    {
        return currentJumps == 0;
    }

    public bool CanJump()
    {
        return currentJumps < maxJumps;
    }

    public void Jump()
    {
        currentJumps++;
    }
}
