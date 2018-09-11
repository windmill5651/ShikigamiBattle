using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ Serializable ]
public class MasterBattleCharacterStatus {

    #region フィールド/プロパティ

    /// <summary>
    /// 移動速度です
    /// </summary>
    [ SerializeField ]
    public float moveSpeed = 0;

    /// <summary>
    /// ダッシュ速度
    /// </summary>
    [SerializeField]
    public float dashSpeed = 0;

    /// <summary>
    /// 重力半減フラグ
    /// </summary>
    [SerializeField]
    public bool isGravityHalf = false;

    /// <summary>
    /// 移動速度
    /// </summary>
    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    /// <summary>
    /// ダッシュ速度
    /// </summary>
    public float DashSpeed
    {
        get { return dashSpeed; }
    }

    /// <summary>
    /// 重力半減フラグ
    /// </summary>
    public bool IsGravityHalf
    {
        get { return isGravityHalf; }
    }

    #endregion


    #region メソッド

    #endregion

}
