using System;
using UnityEngine;

/// <summary>
/// 式神のゲームキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    public class IdolState : CharacterStateBase
    {

        #region メソッド

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="parameter">ステートのパラメータです。</param>
        /// <param name="onChange">ステート変更時の処理です。</param>
        public IdolState( StateParameter parameter,Action< CharacterState > onChange ) : base( parameter, onChange ) { }

        /// <summary>
        /// 攻撃入力です。
        /// </summary>
        /// <returns>遷移後ステート</returns>
        public override void InputAttack()
        {
            ChangeState( CharacterState.Attack );
        }

        /// <summary>
        /// ジャンプ入力です。
        /// </summary>
        /// <param name="isInput">ジャンプ入力状態</param>
        /// <returns>遷移後ステート</returns>
        public override void InputJump( bool isInput )
        {
            if ( isInput )
            {
                ChangeState( CharacterState.Jump );
            }
        }

        /// <summary>
        /// 移動です。
        /// </summary>
        /// <param name="inputVec">入力ベクトル</param>
        /// <returns>遷移後ステート</returns>
        public override void InputMove( Vector3 inputVec )
        {
            // 入力されたらステート移行
            if ( inputVec.sqrMagnitude > 0 )
            {
                ChangeState( CharacterState.Move );
            }
        }

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        /// <param name="rigid">キャラクターのBodyです。</param>
        /// <returns>遷移後ステートです。</returns>
        public override void OnUpdate( Rigidbody rigid ){}

        #endregion
    }

}
