using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Constant
{
    public class ConstantInfo
    {
        public const string ROLE_ADMIN = "admin";
        public const string ROLE_CHEKER1 = "checker1";
        public const string ROLE_CHEKER2 = "checker2";
        public const string ROLE_MAKER = "maker";

        public const string BTSTAT_01 = "01";     //N'未授权'
        public const string BTSTAT_02 = "02";     //N'部分授权'
        public const string BTSTAT_03 = "03";     //N'已授权'
        public const string BTSTAT_04 = "04";     //N'已发送'
        public const string BTSTAT_05 = "05";     //N'已完成'
        public const string BTSTAT_97 = "97";     //N'被拒绝'
        public const string BTSTAT_98 = "98";     //N'超上限'
        public const string BTSTAT_99 = "99";     //N'未成功'


        public const string PAYSTAT_01 = "01";     //N'未发送'
        public const string PAYSTAT_02 = "02";     //N'已发送'
        public const string PAYSTAT_S = "S";     //N'成功'
        public const string PAYSTAT_F = "F";     //N'失败'
        public const string PAYSTAT_B = "B";     //N'退票'
    }
}