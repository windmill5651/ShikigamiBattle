using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kugl.Transition.Scene;
using System;

/// <summary>
/// シキガミのゲームシーンの名前空間です。
/// </summary>
namespace Shikigami.InGame.Main
{
    /// <summary>
    ///  GameScene
    ///  シキガミのメインゲームシーンです。
    ///  
    /// Author:Windmiill
    /// </summary>
    public class GameScene : SceneBase
    {
        protected override IEnumerator OnCloseScene()
        {
            return null;
        }

        protected override IEnumerator OnLoadScene( SceneParameterBase param )
        {
            return null;
        }

        protected override IEnumerator OnOpenScene( SceneParameterBase param )
        {
            return null;
        }
    }

}
