using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool Running { get; private set; }
    public bool Done { get; private set; }

    private void Start()
    {
        Running = false;
        Done = true;
    }

    public void RunAnimations(Queue<IEnumerator> animations)
    {
        if (animations.Count < 1)
        {
            Running = false;
            Done = true;
            return;
        }

        Running = true;
        Done = false;
        StartCoroutine(RunAnimationsCo(animations));
    }

    private IEnumerator RunAnimationsCo(Queue<IEnumerator> animations)
    {
        var anim = animations.Dequeue();
        while (anim != null)
        {
            yield return StartCoroutine(anim);
            anim = animations.Count > 0 ? animations.Dequeue() : null;
        }
        Running = false;
        Done = true;
    }
}