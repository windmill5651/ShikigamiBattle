using UnityEngine;
using Game.Util.Animation;
using System.Collections;

/// <summary>
/// 式神のゲームキャラクターの名前空間です。
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


        #region フィールド/プロパティ

        /// <summary>
        /// ベースとなる方向
        /// </summary>
        private Transform baseDir = null;

        /// <summary>
        /// 物理挙動です
        /// </summary>
        private Rigidbody rigidBody = null;

        /// <summary>
        /// キャラクターのアニメーションをコントロールします。
        /// </summary>
        private CharacterAnimationControl animController = null;

        /// <summary>
        /// ステートマシンです
        /// </summary>
        private CharacterStateMachine stateMachine = null;
    
        /// <summary>
        /// バトルキャラクターのパラメータです
        /// </summary>
        private BattleCharacterParameter characterParam = null;

        /// <summary>
        /// アニメーションのイベントハンドラです
        /// </summary>
        private AnimationEventHandler animationHandler = null;

        /// <summary>
        /// バトルキャラの移動処理です。
        /// </summary>
        private BattleCharacterMover mover = null;

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

            stateMachine = new CharacterStateMachine();
            stateMachine.CreateStateMachine( animController );

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

        /// <summary>
        /// アニメーションのステートが変わった時の処理
        /// </summary>
        /// <param name="info">アニメーション情報</param>
        private void OnStateEnter( AnimatorStateInfo info )
        {
            if ( isInit )
            {
                stateMachine.OnAnimStateEnter( info );
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
                stateMachine.OnAnimStateFinish( info );
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
                var inputVector = forward + side;

                inputVector.y = 0;
                // 移動ベクトル
                if ( inputVector.sqrMagnitude > 1.0f )
                {
                    inputVector.Normalize();
                }

                // ステートマシンに入力ベクトルを渡す
                stateMachine.OnInputMove( inputVector );
            }
        }

        /// <summary>
        /// 攻撃の入力を行います。
        /// </summary>
        public void Attack()
        {
            if ( isInit )
            {
                stateMachine.OnInputAttack();
            }
        }

        /// <summary>
        /// ジャンプ入力を行います。
        /// </summary>
        public void InputJump( bool isInput )
        {
            if ( isInit )
            {
                stateMachine.OnInputJump( isInput );
            }
        }

        /// <summary>
        /// 固定フレームレートでの定期更新処理です。
        /// </summary>
        protected void FixedUpdate()
        {
            if ( isInit )
            {
                stateMachine.OnUpdate();
            }
        }

        #endregion
    }

}
