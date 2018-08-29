
/// <summary>
/// 汎用ゲームシステム名前空間です。
/// </summary>
namespace Game.Library.PlayerInput
{

    /// <summary>
    /// 入力オーナーのベースクラスです
    /// </summary>
    public abstract class InputOwnerBase
    {

        /// <summary>
        /// このオーナーの持つアダプターです
        /// </summary>
        protected IInputAdapter inputAdapter;

        /// <summary>
        /// 初期化処理を行います
        /// </summary>
        /// <param name="adapter">アダプタです</param>
        public void Initialize( IInputAdapter adapter )
        {
            // 入力アダプタを取得
            this.inputAdapter = adapter;

            // それぞれ初期化
            OnInitialize();
        }

        /// <summary>
        /// 初期化時処理です
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// 定期更新処理です。
        /// </summary>
        public abstract void OnUpdate();

    }

}
