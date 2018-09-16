using UnityEngine;
using Shikigami.Game.InputUtil;

/// <summary>
/// 式神のキャラクター名前空間です。
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
        /// キャラクターのステート配列です。
        /// </summary>
        private CharacterStateBase[] states = null;

        /// <summary>
        /// キャラクターのステートです。
        /// </summary>
        private CharacterState currentState = CharacterState.Idole;

        /// <summary>
        /// 遷移予定のステートです。
        /// </summary>
        private CharacterState nextState = CharacterState.Idole;

        /// <summary>
        /// 現在の入力情報です。
        /// </summary>
        private CurrentInput input;

        /// <summary>
        /// ステート共有のパラメータです。
        /// </summary>
        public CharacterStateSharedValues StateValues
        {
            get; private set; 
        }

        #endregion


        #region メソッド

        /// <summary>
        /// セットアップを行います。
        /// </summary>
        /// <param name="control">アニメーションのコントローラです</param>
        public void Setup( CharacterAnimationControl control, CurrentInput input, Rigidbody rigidBody )
        {
            var parameter = new CharacterStateSharedValues();

            parameter.input = input;
            parameter.rigidBody = rigidBody;

            StateValues = parameter;
            states = new CharacterStateBase[]
                {
                    new IdolState( parameter, control, OnChangeState ),
                    new MoveState( parameter, control, OnChangeState ),
                    new AttackState( parameter, control, OnChangeState ),
                    new JumpState( parameter, control, OnChangeState ),
                };
        }

        /// <summary>
        /// アニメーションに入った時の処理です
        /// </summary>
        /// <param name="info"></param>
        public void OnStateEnter( AnimatorStateInfo info )
        {
            states[ ( int )currentState ].OnAnimationStateEnter();
        }

        /// <summary>
        /// アニメーションが終わった時の処理です。
        /// </summary>
        /// <param name="info"></param>
        public void OnStateExit( AnimatorStateInfo info )
        {
            states[ ( int )currentState ].OnAnimationStateExit();
        }


        /// <summary>
        /// 固定フレームレートでの定期更新処理です。
        /// </summary>
        public void OnUpdate()
        {
            Debug.Log( "CurrentState:" + currentState );
            states[ ( int )currentState ].OnUpdate();

            if ( currentState != nextState )
            {
                currentState = nextState;
                states[ ( int )nextState ].OnChangedState();
            }
        }

        /// <summary>
        /// ステートを変更します。
        /// </summary>
        /// <param name="nextState"></param>
        private void OnChangeState( CharacterState nextState )
        {
            this.nextState = nextState;
        }

        #endregion

    }

}
