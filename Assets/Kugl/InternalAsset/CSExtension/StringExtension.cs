/// <summary>
/// C#拡張機能の名前空間
/// </summary>
namespace Kugl.CSExtensions
{

    /// <summary>
    ///  StringExtension
    ///  stringに関する拡張機能
    ///  
    /// Author:Windmill
    /// </summary>
    public static class StringExtension
    {

        #region メソッド

        /// <summary>
        /// 文字列にカラーコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <param name="colorCode">色コードつき文字列</param>
        public static string Color( this string self, string colorCode )
        {
            var fmt = "<color={0:s}>{1}</color>";
            return string.Format( fmt, colorCode, self );
        }

        /// <summary>
        /// 赤のカラーコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>赤のカラーコードつき文字列</returns>
        public static string Red( this string self )
        {
            return self.Color( "Red" );
        }

        /// <summary>
        /// 緑のカラーコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>緑のカラーコード付き文字列</returns>
        public static string Green( this string self )
        {
            return self.Color( "Green" );
        }

        /// <summary>
        /// 青のカラーコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>青のカラーコード付き文字列</returns>
        public static string Blue( this string self )
        {
            return self.Color( "Blue" );
        }

        /// <summary>
        /// ライムのカラーコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>ライム色のカラーコード付き文字列</returns>
        public static string Lime( this string self )
        {
            return self.Color( "Lime" );
        }

        /// <summary>
        /// シアンのカラーコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>シアンのカラーコード付き文字列</returns>
        public static string Cyan( this string self )
        {
            return self.Color( "Cyan" );
        }

        /// <summary>
        /// 文字列にボールドのコードを埋め込みます。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <returns>ボールドのコード付き文字列</returns>
        public static string Bold( this string self )
        {
            var fmt = "<b>{0}</b>";
            return string.Format( fmt, self );
        }

        /// <summary>
        /// 文字列から指定した文字列を削除します。
        /// </summary>
        /// <param name="self">文字列</param>
        /// <param name="target">削除したい文字列</param>
        /// <returns>指定した文字列を削除した文字列</returns>
        public static string Remove( this string self, string target )
        {
            return self.Replace( target, "" );
        }

        #endregion

    }

}
