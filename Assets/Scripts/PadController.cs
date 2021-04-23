using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    public float SpeedIncrease;
    public float SpeedDuration;

    public float SpeedDecrease;
    public float SlowDuration;

    public float HeightIncrease;

    public float GrowthIncrease;
    public float GrowthSmoothing;
    public float GrowthDuration;

    public bool IsSpeed;
    public bool IsSlow;
    public bool IsJump;
    public bool IsGrowth;

    ThirdPersonController PlayerRef;
    Transform PlayerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerRef = other.GetComponent<ThirdPersonController>();
            PlayerTransform = other.gameObject.transform;

            if (IsSpeed)
                StartCoroutine(SpeedPad());
            if (IsSlow)
                StartCoroutine(SlowPad());
            if (IsJump)
                JumpPad();
            if (IsGrowth)
            {
                StartCoroutine(GrowthPad());
            }
        }
    }

    void JumpPad()
    {
        PlayerRef.velocity.y = Mathf.Sqrt(HeightIncrease * -2f * PlayerRef.gravity);
    }

    IEnumerator SpeedPad()
    {
        PlayerRef.Speed += SpeedIncrease;
        yield return new WaitForSeconds(SpeedDuration);
        PlayerRef.Speed -= SpeedIncrease;
    }

    IEnumerator SlowPad()
    {
        PlayerRef.Speed -= SpeedDecrease;
        yield return new WaitForSeconds(SlowDuration);
        PlayerRef.Speed += SpeedDecrease;
    }

    IEnumerator GrowthPad()
    {
        var TargetScale = PlayerTransform.localScale.x + GrowthIncrease;
        Debug.Log("Target: " + (TargetScale - PlayerTransform.localScale.x));
        yield return null;
        while ((TargetScale - PlayerTransform.localScale.x) > 0.1)
        {
            PlayerTransform.localScale += new Vector3(GrowthSmoothing, GrowthSmoothing, GrowthSmoothing);
            yield return null;
        }
        PlayerRef.rb.mass = 1500;
        yield return new WaitForSeconds(GrowthDuration);
        while ((TargetScale - PlayerTransform.localScale.x) < GrowthIncrease)
        {
            PlayerTransform.localScale += new Vector3(-GrowthSmoothing, -GrowthSmoothing, -GrowthSmoothing);
            yield return null;
        }
        PlayerRef.rb.mass = 50;
    }
}
