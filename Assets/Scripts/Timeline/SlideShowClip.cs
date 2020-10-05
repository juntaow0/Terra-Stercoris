using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SlideShowClip : PlayableAsset
{
    public Texture2D image;

    override public Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<SlideShowBehavior>.Create(graph);
        SlideShowBehavior slideShowBehavior = playable.GetBehaviour();
        slideShowBehavior.image = image;
        return playable;
    }
}
