
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = System.Random;

public class SpineSkelController : MonoBehaviour
{
    
    [SerializeField] private SkeletonGraphic spineGraphic;
    private GUIController _guiController;
    private TrackEntry _trackEntry;
    List<string> animList = new List<string>();
    private Random random = new Random();
    private void Start()
    {
        _guiController = GetComponent<GUIController>();
    }

    public void LoadCharacter(string addressable)
    {
        Addressables.LoadAssetAsync<SkeletonDataAsset>(addressable).Completed += OnLoadDone;
    }
    
    void OnLoadDone(AsyncOperationHandle<SkeletonDataAsset> obj)
    {
        
        try
        {
            obj.Result.scale = 0.005f;
            spineGraphic.skeletonDataAsset = obj.Result;
            spineGraphic.Initialize(true);
            spineGraphic.AnimationState.AddAnimation(0, "idle_Normal", true, 0f);
            spineGraphic.MatchRectTransformWithBounds();
            ListAnimations();
            
        }
        catch
        {
            // ignored
        }
        
    }

    public void ListAnimations()
    {
        var anims = spineGraphic.AnimationState.Data.SkeletonData.Animations;
        anims.RemoveAll(anim => anim.Name.Contains("eye"));
        anims.RemoveAll(anim => anim.Name.Contains("still"));
        anims.RemoveAll(anim => anim.Name.ToLower().Contains("dialog"));
        anims.TrimExcess();
        
        
        foreach (var anim in anims)
        {
            animList.Add(anim.Name);   
        }
        _guiController.EnableAnimationBtns(animList);
    }

    public void PlayAnimation(TMP_Text animationTmpText)
    {
        var animationName = animationTmpText.text;
        spineGraphic.AnimationState.SetEmptyAnimation(0, 0);
        try
        {
            _trackEntry= spineGraphic.AnimationState.AddAnimation(0, animationName, true, 0);
            _trackEntry.Complete += AnimationComplete;
        }
        catch
        {
            //ignore
        }
    }

    private void AnimationComplete(TrackEntry trackentry)
    {
        _trackEntry = spineGraphic.AnimationState.AddAnimation(0, "idle_Normal", true,0f);
    }
    public void PlayOnClick()
    {
        if (animList.Count > 0)
        {
            _trackEntry =
                spineGraphic.AnimationState.AddAnimation(0, animList[random.Next(animList.Count)], false, 0.2f);
            _trackEntry.Complete += AnimationComplete;
        }
    }

    public void SetScale(float scale)
    {
        spineGraphic.rectTransform.localScale= new Vector3(1*scale, 1*scale,1*scale);
    }
    
}
