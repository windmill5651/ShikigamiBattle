using Game.Library.Camera;
using Shikigami.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームユーティリティの名前空間です。
/// </summary>
namespace Game.Util.InputControl
{
    /// <summary>
    ///  InputController
    ///  入力をコントロールします。
    ///  
    /// Author:Windmill
    /// </summary>
    public class InputController : MonoBehaviour
    {

        #region インスペクター設定フィールド

        /// <summary>
        /// カメラターゲットです。
        /// </summary>
        [ SerializeField ]
        private CameraTarget target = null;

        [ SerializeField ]
        private PlayerCharacterControl characterControl = null;

        /// <summary>
        /// カメラターゲットのパラメータです。
        /// </summary>
        [ SerializeField ]
        private CameraTargetParameter parameter = null;

        #endregion

        // Use this for initialization
        void Start()
        {
            target.Setup( parameter );
        }

        // Update is called once per frame
        void Update()
        {
            target.Move( new Vector3( Input.GetAxis( "Mouse X" ), Input.GetAxis( "Mouse Y" ), 0 ) );
            characterControl.MovePlayer( new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) ) );
        }
    }

}
