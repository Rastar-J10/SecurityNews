using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.Common.Tools
{
    /// <summary>
    /// 分页处理帮助类
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// 分页处理
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public static int PageCurrent(int pageIndex,int pageSize)
        {
            //当前页数为1的时候 查询你输入的pagesize数量的前几条数据
            if (pageIndex <= 1)
            {
                pageIndex = 0;
            }
            else
            {
                //否则 当前页减1*页面数据条数得到的要跳过的数据
                pageIndex = (pageIndex - 1) * pageSize;
            }
            return pageIndex;
        }
    }
}
