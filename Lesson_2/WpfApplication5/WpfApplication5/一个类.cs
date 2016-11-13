using System.Collections.Generic;
using 基本;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 系统 = System;
using 字符串到字符串的字典 = System.Collections.Generic.Dictionary<string,string>;
using GUID到字符串的字典 = System.Collections.Generic.Dictionary<System.Guid, string>;
using 全局统一标识符 = System.Guid;
using System.Runtime.InteropServices;

namespace 人脸识别和验证
{
    class 一个类
    {
        public static 字符串到字符串的字典 map = new 字符串到字符串的字典()
        {
            {"baiyunpeng"   ,   "白云鹏"   },
            {"daixinyue"    ,   "代欣月"   },
            {"duansiqi"     ,   "段思其"   },
            {"huangweiyao"  ,   "黄维耀"   },
            {"jiangjunyu"   ,   "姜竣予"   },
            {"lihaiyang"    ,   "李海洋"   },
            {"liuziyi"      ,   "刘紫依"   },
            {"maruining"    ,   "马瑞宁"   },
            {"niuyanrui"    ,   "牛艳蕊"   },
            {"shenyuxiang"  ,   "沈渝翔"   },
            {"tianqing"     ,   "田晴"     },
            {"wangyuxiang"  ,   "王羽翔"   },
            {"wangyuzhuo"   ,   "王玉琢"   },
            {"xialongyu"    ,   "夏隆宇"   },
            {"xuxuelan"     ,   "徐雪兰"   },
            {"yanzhe"       ,   "闫哲"     },
            {"yangzhuqin"   ,   "杨竹琴"   },
            {"yuxiaoxuan"   ,   "于小轩"   },
            {"zhangjunyang" ,   "张钧洋"   },
            {"zhangtingxuan",   "张廷轩"   },
            {"zhangyuning"  ,   "张渝宁"   },
            {"zhongxinying" ,   "钟新颖"   },
            {"wangyuezhang" ,   "王悦章"   },
            {"wangyongqin"  ,   "王勇钦"   },
            {"GuanDY"       ,   "管德煜"   },
            {"houboyu"      ,   "侯博宇"   },
            {"zhangxingyi"  ,   "张兴逸"   },
            {"zhulin"       ,   "朱琳"     },
            {"xialiyun"     ,   "夏俪芸"   }
        };

        public static GUID到字符串的字典 personIdToPersonName = new GUID到字符串的字典()
        {
            {全局统一标识符.Parse("8aa27edb-9b7f-4144-9de4-9e0deb77ac2b"), "baiyunpeng"   },
            {全局统一标识符.Parse("9fec278c-cb52-48dc-b2dd-cb66a958936b"), "daixinyue"    },
            {全局统一标识符.Parse("0bc05490-4d76-44b0-bddd-10693b569fee"), "duansiqi"     },
            {全局统一标识符.Parse("683baaf8-9a95-43f6-8111-bb4a225a0689"), "huangweiyao"  },
            {全局统一标识符.Parse("d72025bd-310b-43ba-aa52-0ea299359965"), "jiangjunyu"   },
            {全局统一标识符.Parse("1427ad06-2d0e-4c1e-9ce0-1654866433ca"), "lihaiyang"    },
            {全局统一标识符.Parse("3be45095-cd49-4e5b-83e4-b144d3aee723"), "liuziyi"      },
            {全局统一标识符.Parse("97f88e27-633a-4c6b-a726-bd8a760a706c"), "maruining"    },
            {全局统一标识符.Parse("b39ff652-3219-4c39-9f5e-2966455d0b6a"), "niuyanrui"    },
            {全局统一标识符.Parse("7785adeb-1762-4d38-9675-3cc7b4cd3b1b"), "shenyuxiang"  },
            {全局统一标识符.Parse("2eafe32f-03d7-447d-9337-ff84a0fbec6a"), "tianqing"     },
            {全局统一标识符.Parse("d785f0ce-8c13-4a63-91f3-03a043127e8f"), "wangyuxiang"  },
            {全局统一标识符.Parse("92043dee-5631-4cd7-a8be-958393d22c6b"), "wangyuzhuo"   },
            {全局统一标识符.Parse("c93a67a2-26d2-4809-8925-bc9e7838be03"), "xialongyu"    },
            {全局统一标识符.Parse("db638d6a-71ad-4c7e-805c-a5e934eacab4"), "xuxuelan"     },
            {全局统一标识符.Parse("0585694d-9395-4e13-8557-1115c619caa8"), "yanzhe"       },
            {全局统一标识符.Parse("b30e3d99-c117-4ac8-8503-0c2c3dfc8ff0"), "yangzhuqin"   },
            {全局统一标识符.Parse("6cb79c31-860f-4994-9b55-03153b221e4f"), "yuxiaoxuan"   },
            {全局统一标识符.Parse("df201651-17d6-4cc7-94bf-1534a52f204d"), "zhangjunyang" },
            {全局统一标识符.Parse("767f9a00-e474-457c-872e-3727abb9a7e3"), "zhangtingxuan"},
            {全局统一标识符.Parse("3b3bc54b-ee70-43ba-9365-57ebe1f6b777"), "zhangyuning"  },
            {全局统一标识符.Parse("1dff4460-7e25-4e2e-bc1e-3c5f6b298b05"), "zhongxinying" },
            {全局统一标识符.Parse("122d4027-faa3-4342-8e34-2c14df2fd3a5"), "wangyuezhang" },
            {全局统一标识符.Parse("2381756d-a948-4330-9144-18816aa5966e"), "wangyongqin"  },
            {全局统一标识符.Parse("cd2e364c-98d2-48e9-a0f2-5823a33360bb"), "GuanDY"       },
            {全局统一标识符.Parse("1bbd57a6-9bfc-4da8-b5ee-41ba5b227ad8"), "houboyu"      },
            {全局统一标识符.Parse("1d0ee46c-1f76-499d-ad63-40652533e8aa"), "zhangxingyi"  },
            {全局统一标识符.Parse("7842cc40-8d82-4b52-bc70-5dffb9ff1d2c"), "zhulin"       },
            {全局统一标识符.Parse("a255f7e9-d98f-4775-b2ad-0bcd3631845f"), "xialiyun"     }
        };
    }
}
