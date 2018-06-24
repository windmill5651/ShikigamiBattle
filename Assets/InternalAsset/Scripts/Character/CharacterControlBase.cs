using UnityEngine;

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
    public class CharacterControlBase : MonoBehaviour
    {
        #region インスペクター設定フィールド

        /// <summary>
        /// 速度
        /// </summary>
        [ SerializeField ]
        private float speed = 1;

        /// <summary>
        /// アニメーションをコントローラです。
        /// </summary>
        [ SerializeField ]
        private CharacterAnimationControl animationControl;

        #endregion


        #region フィールド/プロパティ

        /// <summary>
        /// 物理挙動のプロパティ
        /// </summary>
        private Rigidbody rigidBody = null;

        /// <summary>
        /// 移動ベクトル
        /// </summary>
        private Vector3 moveVector = new Vector3();

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
        }

        /// <summary>
        /// キャラクターを移動させます。
        /// </summary>
        /// <param name="moveDirection">移動方向のベクトルです。</param>
        public void Move( Vector3 moveDirection )
        {
            moveVector = moveDirection * speed;
        }

        /// <summary>
        /// 攻撃の入力を行います。
        /// </summary>
        public void Attack()
        {
            animationControl.SetAttackTrigger();
        }

        /// <summary>
        /// 固定フレームレートでの定期更新処理です。
        /// </summary>
        protected void FixedUpdate()
        {
            // velocity移動
            rigidBody.velocity = moveVector * Time.fixedDeltaTime;

            // 移動中のみ方向を変える
            if ( moveVector.sqrMagnitude > 0 )
            {
                var lookDir = moveVector;
                lookDir.y = 0;
                rigidBody.rotation = Quaternion.LookRotation( moveVector );
            }

            animationControl.SetMoveSpeed( moveVector.magnitude );
        }

        #endregion
    }
}
