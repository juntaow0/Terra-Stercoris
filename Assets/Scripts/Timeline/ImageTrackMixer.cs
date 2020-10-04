using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine;

public class ImageTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        RawImage rawImage = playerData as RawImage;
        Texture2D currentTexture = null;
        float curentAlpha = 0f;
        if (!rawImage) { return; }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++) {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f) {
                ScriptPlayable<SlideShowBehavior> inputPlayable = (ScriptPlayable<SlideShowBehavior>)playable.GetInput(i);
                SlideShowBehavior input = inputPlayable.GetBehaviour();
                currentTexture = input.image;
                curentAlpha = inputWeight;
            }
        }
        rawImage.texture = currentTexture;
        rawImage.color = new Color(1,1,1,curentAlpha);
    }
}
