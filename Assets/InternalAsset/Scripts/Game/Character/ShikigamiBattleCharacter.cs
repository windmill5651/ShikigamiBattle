using UnityEngine;
using Game.Util.Animation;
using System.Collections;
using Shikigami.Game.InputUtil;

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
        /// バトルキャラクターのパラメータです
        /// </summary>
        private BattleCharacterParameter characterParam = null;

        /// <summary>
        /// アニメーションのイベントハンドラです
        /// </summary>
        private AnimationEventHandler animationHandler = null;

        /// <summary>
        /// キャラクターのステートマシンです。
        /// </summary>
        private CharacterStateMachine stateMachine = null;

        /// <summary>
        /// 現在の入力状況です。
        /// </summary>
        private CurrentInput input = null;

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

                stateMachine = new CharacterStateMachine();
                input = new CurrentInput();
                stateMachine.Setup( animationControl, input, rigidBody );
            }
            else
            {
                Debug.LogError( "Model is Not Loaded ID:" + characterId );
            }
        }

        /// <summary>
        /// アニメーションのステートが変わった時の処理
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        private void OnStateEnter( AnimatorStateInfo info )
        {
            if ( isInit )
            {
                stateMachine.OnStateEnter( info );
            }
        }

        /// <summary>
        /// アニメーションのステートが完了した時の処理
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        private void OnStateFinish( AnimatorStateInfo info )
        {
            if ( isInit )
            {
                stateMachine.OnStateExit( info );
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

                // 移動入力
                input.inputMoveVec = moveDir;
            }
        }

        /// <summary>
        /// 攻撃の入力を行います。
        /// </summary>
        public void Attack( bool isInput )
        {
            if ( isInit )
            {
                Debug.Log( "InputAttack:" + isInput );
                // 攻撃入力
                input.isAttack = isInput;
            }
        }

        /// <summary>
        /// ジャンプ入力を行います。
        /// </summary>
        public void InputJump( bool isInput )
        {
            if ( isInit )
            {
                // ジャンプ入力
                input.isJump = isInput;
            }
        }

        /// <summary>
        /// 固定フレームレートでの定期更新処理です。
        /// </summary>
        protected void FixedUpdate()
        {
            if ( isInit )
            {
                if ( Physics.Raycast( transform.position + Vector3.up , Vector3.down,1.1f, 1<<8  ) )
                {
                    // Yの絶対値があれば0をセット
                    if( Mathf.Abs( rigidBody.velocity.y ) > 0 )
                    {
                        var currentVel = rigidBody.velocity;                    
                        currentVel.y = 0;
                        rigidBody.velocity = currentVel;
                    }

                    stateMachine.StateValues.isGround = true;
                }
                else
                {
                    stateMachine.StateValues.isGround = false;
                    var currentVel = rigidBody.velocity;
                    currentVel.y = 0;
                    rigidBody.velocity = currentVel;
                }

                stateMachine.OnUpdate();
            }

        }

        #endregion
    }

}
