using System.Collections;
using UnityEngine;

/// <summary>
/// シキガミの名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{

    /// <summary>
    ///  CharacterAnimationControl
    ///  キャラクターのアニメーションを制御します。
    ///  
    /// Author:Windmill
    /// </summary>
    public class CharacterAnimationControl : MonoBehaviour
    {
        #region インスペクター設定フィールド

        /// <summary>
        /// アニメーターです。
        /// </summary>
        [ SerializeField ]
        private Animator animator = null;

        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// 攻撃中か
        /// </summary>
        private bool isAttacking = false;

        /// <summary>
        /// フィニッシュ攻撃中か
        /// </summary>
        private bool isFinishing = false;

        #endregion


        #region メソッド

        /// <summary>
        /// 移動速度を設定します。
        /// </summary>
        /// <param name="moveSpeed">移動速度です</param>
        public void SetMoveSpeed( float moveSpeed )
        {
            animator.SetFloat( "Speed", moveSpeed );
        }

        /// <summary>
        /// 攻撃のトリガーを設定します。
        /// </summary>
        public void SetAttackTrigger()
        {
            if( isAttacking || isFinishing )
            {
                return;
            }

            animator.SetTrigger( "Attack" );
            isAttacking = true;

            animator.SetBool( "IsAttack", isAttacking );
        }

        /// <summary>
        /// 攻撃可能状態になったら呼び出されます。
        /// </summary>
        private void SetAttackable()
        {
            isAttacking = false;
            animator.SetBool( "IsAttack", isAttacking );
        }

        /// <summary>
        /// コンボフィニッシュ時に呼び出されます。
        /// </summary>
        private void ComboFinish()
        {
            isFinishing = true;
            SetAttackable();
            StartCoroutine( FinishWait() );
        }

        /// <summary>
        /// コンボフィニッシュンアニメーションを待ってから攻撃可能にします。
        /// </summary>
        private IEnumerator FinishWait()
        {
            var state = animator.GetCurrentAnimatorStateInfo( 0 );
            var hash = state.fullPathHash;    

            // 現在のアニメーションが完了するまで待つ
            while ( state.normalizedTime < 1 || hash == state.fullPathHash )
            {
                yield return null;
                state = animator.GetCurrentAnimatorStateInfo( 0 );
            }

            isFinishing = false;

            yield break;
        }

        #endregion
    }

}
