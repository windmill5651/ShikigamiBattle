using System;
using UnityEngine;


/// <summary>
/// 式神のゲームキャラクター名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{
    
    /// <summary>
    ///  CharacterStateMachine
    ///  キャラクターのステートマシンです。
    ///  
    /// Author:Windmill
    /// </summary>
    public class CharacterStateMachine
    {
        #region フィールド/プロパティ

        /// <summary>
        /// ステートマシンパラメータです。
        /// </summary>
        private StateParameter stateParam = null;

        /// <summary>
        /// ステートリストです
        /// </summary>
        private CharacterStateBase[] states = null;

        /// <summary>
        /// ステートです。
        /// </summary>
        private CharacterState currentState;

        /// <summary>
        /// 次のステートです。
        /// </summary>
        private CharacterState nextState;

        #endregion


        #region メソッド


        public void CreateStateMachine( CharacterAnimationControl controller )
        {
            var parameter = new StateParameter();
            var stateList = new CharacterStateBase[]
            {
                new IdolState( parameter, controller, OnChangeState ),
                new MoveState( parameter, controller, OnChangeState ),
                new AttackState( parameter, controller, OnChangeState ),
                new JumpState( parameter, controller, OnChangeState ),
            };

            states = stateList;
        }

        /// <summary>
        /// アニメーションステートに入った時
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        public void OnAnimStateEnter( AnimatorStateInfo info )
        {
            states[ ( int )currentState ].OnAnimationStateEnter();
        }

        /// <summary>
        /// アニメーションステートの終わり時
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        public void OnAnimStateFinish( AnimatorStateInfo info )
        {
            states[ ( int )currentState ].OnAnimationStateExit();
        }

        /// <summary>
        /// 移動入力です
        /// </summary>
        /// <param name="inputDir">移動が入力された時</param>
        public void OnInputMove( Vector3 inputDir )
        {
            states[ ( int )currentState ].InputMove( inputDir );
        }

        /// <summary>
        /// 攻撃入力です
        /// </summary>
        public void OnInputAttack()
        {
            states[ ( int )currentState ].InputAttack();
        }

        /// <summary>
        /// ジャンプ入力を行います。
        /// </summary>
        /// <param name="isInput">ジャンプ入力されているか</param>
        public void OnInputJump( bool isInput )
        {
            states[ ( int )currentState ].InputJump( isInput );
        }

        /// <summary>
        /// 固定フレームレートでの更新定期処理です。
        /// </summary>
        public void OnUpdate()
        {
            states[ ( int )currentState ].OnUpdate();

            if ( currentState != nextState )
            {
                currentState = nextState;
            }
        }

        /// <summary>
        /// ステート変更時処理です。
        /// </summary>
        /// <param name="nextState"></param>
        private void OnChangeState( CharacterState nextState )
        {
            this.nextState = nextState;
        }

        #endregion

    }

}
