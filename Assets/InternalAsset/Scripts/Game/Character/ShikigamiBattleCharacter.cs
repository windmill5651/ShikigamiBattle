using UnityEngine;
using Game.Util.Animation;
using System.Collections;

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
    public class ShikigamiBattleCharacter : MonoBehaviour
    {

        #region 定数値

        /// <summary>
        /// 無効なオーナーです
        /// </summary>
        public const int INVALID_OWNER_NO = -1;

        #endregion


        #region インスペクター設定フィールド

        /// <summary>
        /// 速度
        /// </summary>
        [ SerializeField ]
        private float speed = 1;

        /// <summary>
        /// ベースとなる方向
        /// </summary>
        private Transform baseDir = null;


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
        /// ステートマシンパラメータです。
        /// </summary>
        private CharacterStateBase.StateParameter stateParam = null;
 
        /// <summary>
        /// キャラクターのアニメーションをコントロールします。
        /// </summary>
        private CharacterAnimationControl animController;

        /// <summary>
        /// キャラクターのステート配列です。
        /// </summary>
        private CharacterStateBase[] states = null;
    
        /// <summary>
        /// バトルキャラクターのパラメータです
        /// </summary>
        private BattleCharacterParameter characterParam = null;

        /// <summary>
        /// アニメーションのイベントハンドラです
        /// </summary>
        private AnimationEventHandler animationHandler = null;

        /// <summary>
        /// 初期化済み？
        /// </summary>
        private bool isInit = false;

        /// <summary>
        /// オーナーの名前です
        /// </summary>
        public int OwnerNo
        {
            get
            {
                var ownerNo = INVALID_OWNER_NO;

                if( characterParam != null )
                {
                    ownerNo = characterParam.ownerNo;
                }

                return ownerNo;
            }
        }

        /// <summary>
        /// オーナーのタイプです
        /// </summary>
        public CharacterOwnerType OwnerType
        {
            get
            {
                var type = CharacterOwnerType.INVALID;

                if( characterParam != null )
                {
                    type = characterParam.ownerType;
                }

                return type;
            }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// 非同期でセットアップを行います
        /// </summary>
        /// <param name="param">パラメータ</param>
        public IEnumerator SetupAsync( BattleCharacterParameter param, Transform baseDirection = null )
        {
            rigidBody = GetComponent< Rigidbody >();

            // キャラクターのパラメータです
            characterParam = param;

            if ( baseDirection != null )
            {
                baseDir = baseDirection;
            }
            else
            {
                baseDir = transform;
            }

            // モデルのセットアップを行います
            yield return SetupModelAsync( param.characterId );
            
            // ステートのパラメータを生成
            stateParam = new CharacterStateBase.StateParameter()
            {
                maxSpeed = speed,
            };

            states = CharacterStateBase.CreateStateMachine( stateParam, animController, OnChangeState );

            isInit = true;
        }

        /// <summary>
        /// 非同期でモデルをセットアップします
        /// </summary>
        /// <param name="characterId">キャラクターID</param>
        private IEnumerator SetupModelAsync( int characterId )
        {
            var modelName = CharacterConst.GetCharacterModelName( characterId );
            var modelLoadRequest = Resources.LoadAsync< GameObject >( "GameScene/BattleCharacter/" + modelName );

            yield return modelLoadRequest;

            // モデルのロードが出来たらモデルを生成してアニメーションセット
            if( modelLoadRequest.isDone )
            {
                // モデルのインスタンス元を取得
                var modelSrc = modelLoadRequest.asset as GameObject;
           
                // インスタンス化してコンポーネント取得
                var modelObj = Instantiate( modelSrc );
                var animationControl = modelObj.GetComponent< CharacterAnimationControl >();
                var animator = modelObj.GetComponent< Animator >();
                
                // モデル位置を0にする
                modelObj.transform.SetParent( transform );
                modelObj.transform.localPosition = Vector3.zero;

                // アニメーションのハンドラ設定
                animationHandler = new AnimationEventHandler(); 
                animationHandler.Setup( animator, OnStateEnter, OnStateFinish );

                // アニメーションコントローラを設定
                animController = animationControl;
            }
            else
            {
                Debug.LogError( "Model is Not Loaded ID:" + characterId );
            }
        }

        private void OnStateEnter( AnimatorStateInfo info )
        {
            if ( isInit )
            {
                states[ ( int )currentState ].OnAnimationStateEnter();
            }
        }

        private void OnStateFinish( AnimatorStateInfo info )
        {
            if ( isInit )
            {
                states[ ( int )currentState ].OnAnimationStateExit();
            }
        }

        /// <summary>
        /// キャラクターを移動させます。
        /// </summary>
        /// <param name="inputDir">入力ベクトルです。</param>
        public void Move( Vector3 inputDir )
        {
            if ( isInit )
            {
                var forward = baseDir.forward * inputDir.z;
                var side = baseDir.right * inputDir.x;
                var moveDir = forward + side;

                moveDir.y = 0;
                // 移動ベクトル
                if ( moveDir.sqrMagnitude > 1.0f )
                {
                    moveDir.Normalize();
                }

                states[ ( int )currentState ].InputMove( moveDir );
            }
        }

        /// <summary>
        /// 攻撃の入力を行います。
        /// </summary>
        public void Attack()
        {
            if ( isInit )
            {
                states[ ( int )currentState ].InputAttack();
            }
        }

        /// <summary>
        /// ジャンプ入力を行います。
        /// </summary>
        public void InputJump( bool isInput )
        {
            if ( states != null )
            {
                states[ ( int )currentState ].InputJump( isInput );
            }
        }

        /// <summary>
        /// 固定フレームレートでの定期更新処理です。
        /// </summary>
        protected void FixedUpdate()
        {
            if ( isInit )
            {
                states[ ( int )currentState ].OnUpdate( rigidBody );
            }
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
