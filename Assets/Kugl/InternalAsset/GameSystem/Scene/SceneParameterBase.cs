using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kuglのトランジションシステムの名前空間です。
/// </summary>
namespace Kugl.Transition.Scene
{

    /// <summary>
    ///  SceneParameterBase
    ///   シーンパラメータのベース
    ///   
    /// Author:Windmill
    /// </summary>
    public class SceneParameterBase {

        /// <summary>
        /// シーン遷移時にGCを呼び出すか
        /// </summary>
        public bool isCallGC = true;

        /// <summary>
        /// 履歴を残すか
        /// </summary>
        public bool isPushHistory = true;

    }

}
