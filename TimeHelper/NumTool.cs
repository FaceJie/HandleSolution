using System;
using System.Collections.Generic;
using System.Text;

namespace CommonHelper
{
    public static class NumTool
    {
        /**
     * 根据当前系统时间加随机序列来生成订单号
     * @return 订单号
     **/
        public static string GenerateParentNo()
        {
            Random ran = new Random();
            return string.Format("P{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }
        /**
       * 根据当前系统时间加随机序列来生成订单号
       * @return 订单号
       **/
        public static string GenerateBatchNo()
        {
            Random ran = new Random();
            return string.Format("B{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }
        /**
      * 根据当前系统时间加随机序列来生成订单号
      * @return 订单号
      **/
        public static string GenerateOrderNo()
        {
            Random ran = new Random();
            return string.Format("U{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }
        /**
         * 根据当前系统时间加随机序列来生成订单号
         * @return 订单号
         **/
        public static string GenerateTransportNo()
        {
            Random ran = new Random();
            return string.Format("BT{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }
    }
}
