using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.Entities
{
    /// <summary>
    /// 操作日志实体类
    /// </summary>
    [Serializable]
    public class OperateLogEntity
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 操作栏目
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// 操作功能
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 请求接口路径
        /// </summary>
        public string InterfaceUrl { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public long Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
    }
}
