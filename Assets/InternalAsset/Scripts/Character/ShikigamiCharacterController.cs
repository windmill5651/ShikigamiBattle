using UnityEngine;
using Kugl.CSExtensions;

/// <summary>
/// シキガミのゲームキャラクターの名前空間です。
/// </summary>
namespace Shikigami.Game.Character
{
    /// <summary>
    ///  CharacterControl
    ///  キャラクターをコントロールするクラスです
    ///  
    /// Author:Windmill
    /// </summary>
    [ RequireComponent( typeof( Rigidbody ) ) ]
    public class ShikigamiCharacterController : MonoBehaviour
    {

        #region インスペクター設定フィールド

        /// <summary>
        /// 速度
        /// </summary>
        [ SerializeField ]
        private float speed = 1;


        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// 物理挙動です
        /// </summary>
        private Rigidbody rigidBody = null;

        /// <summary>
        /// キャラクターのステートです。
        /// </summary>
        private CharacterState currentState = CharacterState.Idole;

        /// <summary>
        /// 遷移予定のステートです。
        /// </summary>
        private CharacterState nextState = CharacterState.Idole;

        /// <summary>
        /// 入力のステートです。
        /// </summary>
        private InputableState inputState = InputableState.Enable;

        /// <summary>
        /// パラメータです。
        /// </summary>
        private CharacterStateBase.StateParameter param = null;
 
        /// <summary>
        /// キャラクターのアニメーションをコントロールします。
        /// </summary>
        private CharacterAnimationControl animController;

        /// <summary>
        /// キャラクターのステート配列です。
        /// </summary>
        private CharacterStateBase[] states = null;


        #endregion


        #region メソッド

        /// <summary>
        /// 初期化処理です。
        /// </summary>
        protected void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            rigidBody = GetComponent< Rigidbody >();

            // ステートのパラメータを生成
            param = new CharacterStateBase.StateParameter()
            {
                maxSpeed = speed,
            };

            states =  CharacterStateBase.CreateStateMachine( param, null, OnChangeState );
        }

        /// <summary>
        /// キャラクターを移動させます。
        /// </summary>
        /// <param name="moveDirection">移動方向のベクトルです。</param>
        public void Move( Vector3 moveDirection )
        {
            states[ ( int )currentState ].InputMove( moveDirection );
        }

        /// <summary>
        /// 攻撃の入力を行います。
        /// </summary>
        public void Attack()
        {
            states[ ( int )currentState ].InputAttack();
        }

        /// <summary>
        /// ジャンプ入力を行います。
        /// </summary>
        public void InputJump( bool isInput )
        {
            states[ ( int )currentState ].InputJump( isInput );
        }

        /// <summary>
        /// 固定フレームレートでの定期更新処理です。
        /// </summary>
        protected void FixedUpdate()
        {
            states[ ( int )currentState ].OnUpdate( rigidBody );
        }

        /// <summary>
        /// Update処理後の定期更新処理です。
        /// </summary>
        private void LateUpdate()
        {
            if( currentState != nextState )
            {
                currentState = nextState;
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
