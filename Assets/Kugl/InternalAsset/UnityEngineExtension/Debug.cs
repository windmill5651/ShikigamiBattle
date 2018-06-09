using System;
using UnityEngine;

/// <summary>
///  Debug
///  デバッグ要の機能をまとめたクラスです。
///  
/// Author:Windmill
/// </summary>
public static class Debug
{

    #region フィールド/プロパティ

    /// <summary>
    /// ロガーです。
    /// </summary>
    private static ILogger logger = null;

    /// <summary>
    /// ロガーへのアクセサです。
    /// </summary>
    private static ILogger Logger
    {
        get
        {
            if ( logger == null )
            {
                logger = UnityEngine.Debug.unityLogger;
            }

            return logger;
        }
    }

    /// <summary>
    /// ログの有効状態を取得します。
    /// </summary>
    public static bool IsLogEnable
    {
        get { return Logger.logEnabled; }
    }

    /// <summary>
    /// 現在のログタイプです。
    /// </summary>
    public static LogType CurrentType
    {
        get { return Logger.filterLogType; }
    }

    #endregion


    #region メソッド

    /// <summary>
    /// ログのフィルターを指定します。
    /// </summary>
    /// <param name="logType">ログのタイプ</param>
    public static void LogFilter( LogType logType )
    {
        Logger.filterLogType = logType;
    }

    /// <summary>
    /// ログの有効状態を設定します。
    /// </summary>
    /// <param name="isEnable"></param>
    public static void SetLogEnabled( bool isEnable )
    {
        Logger.logEnabled = isEnable;
    }

    /// <summary>
    /// ログ出力をします。
    /// </summary>
    /// <param name="message">ログメッセージ</param>
    public static void Log( object message )
    {
        Logger.Log( message );
    }

    /// <summary>
    /// ワーニングのログを出します。
    /// </summary>
    /// <param name="tag">タグ</param>
    /// <param name="message">ログメッセージ</param>
    public static void LogWarning( string tag , object message )
    {
        Logger.LogWarning( tag, message );
    }

    /// <summary>
    /// ワーニングのログを出力します
    /// </summary>
    /// <param name="message">メッセージ</param>
    public static void LogWarning( object message )
    {
        Logger.LogWarning( "", message );
    }

    /// <summary>
    /// エラーログを出します。
    /// </summary>
    /// <param name="tag">タグ</param>
    /// <param name="message">ログメッセージ</param>
    public static void LogError( string tag, object message )
    {
        Logger.LogError( tag, message );
    }

    /// <summary>
    /// エラーログを出力します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    public static void LogError( object message )
    {
        Logger.LogError( "", message );
    }

    /// <summary>
    /// 例外のログを出力します。
    /// </summary>
    /// <param name="exception">例外</param>
    public static void LogException( Exception exception )
    {
        Logger.LogException( exception );
    }

    #endregion

}
