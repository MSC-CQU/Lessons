using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Face2
{
    class AClass
    {
        public static Dictionary<string, string> personNameToChineseName = new Dictionary<string, string>()
        {
            {"baiyunpeng"   ,   "∞◊‘∆≈Ù"   },
            {"daixinyue"    ,   "¥˙–¿‘¬"   },
            {"duansiqi"     ,   "∂ŒÀº∆‰"   },
            {"huangweiyao"  ,   "ª∆Œ¨“´"   },
            {"jiangjunyu"   ,   "Ω™ø¢”Ë"   },
            {"lihaiyang"    ,   "¿Ó∫£—Û"   },
            {"liuziyi"      ,   "¡ı◊œ“¿"   },
            {"maruining"    ,   "¬Ì»ƒ˛"   },
            {"niuyanrui"    ,   "≈£—ﬁ»Ô"   },
            {"shenyuxiang"  ,   "…Ú”ÂœË"   },
            {"tianqing"     ,   "ÃÔ«Á"     },
            {"wangyuxiang"  ,   "Õı”œË"   },
            {"wangyuzhuo"   ,   "Õı”Ò◊¡"   },
            {"xialongyu"    ,   "œƒ¬°”Ó"   },
            {"xuxuelan"     ,   "–Ï—©¿º"   },
            {"yanzhe"       ,   "„∆’‹"     },
            {"yangzhuqin"   ,   "—Ó÷Ò«Ÿ"   },
            {"yuxiaoxuan"   ,   "”⁄–°–˘"   },
            {"zhangjunyang" ,   "’≈æ˚—Û"   },
            {"zhangtingxuan",   "’≈Õ¢–˘"   },
            {"zhangyuning"  ,   "’≈”Âƒ˛"   },
            {"zhongxinying" ,   "÷”–¬”±"   },
            {"wangyuezhang" ,   "Õı‘√’¬"   },
            {"wangyongqin"  ,   "Õı”¬«’"   },
            {"GuanDY"       ,   "π‹µ¬Ïœ"   },
            {"houboyu"      ,   "∫Ó≤©”Ó"   },
            {"zhangxingyi"  ,   "’≈–À“›"   },
            {"zhulin"       ,   "÷Ï¡’"     },
            {"xialiyun"     ,   "œƒŸ≥‹ø"   }
        };

        public static Dictionary<string, int> personNameToInt = new Dictionary<string, int>()
        {
            {"baiyunpeng"   , 0  },
            {"daixinyue"    , 1  },
            {"duansiqi"     , 2  },
            {"huangweiyao"  , 3  },
            {"jiangjunyu"   , 4  },
            {"lihaiyang"    , 5  },
            {"liuziyi"      , 6  },
            {"maruining"    , 7  },
            {"niuyanrui"    , 8  },
            {"shenyuxiang"  , 9  },
            {"tianqing"     , 10 },
            {"wangyuxiang"  , 11 },
            {"wangyuzhuo"   , 12 },
            {"xialongyu"    , 13 },
            {"xuxuelan"     , 14 },
            {"yanzhe"       , 15 },
            {"yangzhuqin"   , 16 },
            {"yuxiaoxuan"   , 17 },
            {"zhangjunyang" , 18 },
            {"zhangtingxuan", 19 },
            {"zhangyuning"  , 20 },
            {"zhongxinying" , 21 },
            {"wangyuezhang" , 22 },
            {"wangyongqin"  , 23 },
            {"GuanDY"       , 24 },
            {"houboyu"      , 25 },
            {"zhangxingyi"  , 26 },
            {"zhulin"       , 27 },
            {"xialiyun"     , 28 }
        };

        public static Dictionary<int, string> intToPersonName = new Dictionary<int, string>()
        {
            {0,  "baiyunpeng"    },
            {1,  "daixinyue"     },
            {2,  "duansiqi"      },
            {3,  "huangweiyao"   },
            {4,  "jiangjunyu"    },
            {5,  "lihaiyang"     },
            {6,  "liuziyi"       },
            {7,  "maruining"     },
            {8,  "niuyanrui"     },
            {9,  "shenyuxiang"   },
            {10, "tianqing"      },
            {11, "wangyuxiang"   },
            {12, "wangyuzhuo"    },
            {13, "xialongyu"     },
            {14, "xuxuelan"      },
            {15, "yanzhe"        },
            {16, "yangzhuqin"    },
            {17, "yuxiaoxuan"    },
            {18, "zhangjunyang"  },
            {19, "zhangtingxuan" },
            {20, "zhangyuning"   },
            {21, "zhongxinying"  },
            {22, "wangyuezhang"  },
            {23, "wangyongqin"   },
            {24, "GuanDY"        },
            {25, "houboyu"       },
            {26, "zhangxingyi"   },
            {27, "zhulin"        },
            {28, "xialiyun"      }
        };

        public static Dictionary<Guid, string> personIdToPersonName = new Dictionary<Guid, string>()
        {
            {Guid.Parse("8aa27edb-9b7f-4144-9de4-9e0deb77ac2b"), "baiyunpeng"   },
            {Guid.Parse("9fec278c-cb52-48dc-b2dd-cb66a958936b"), "daixinyue"    },
            {Guid.Parse("0bc05490-4d76-44b0-bddd-10693b569fee"), "duansiqi"     },
            {Guid.Parse("683baaf8-9a95-43f6-8111-bb4a225a0689"), "huangweiyao"  },
            {Guid.Parse("d72025bd-310b-43ba-aa52-0ea299359965"), "jiangjunyu"   },
            {Guid.Parse("1427ad06-2d0e-4c1e-9ce0-1654866433ca"), "lihaiyang"    },
            {Guid.Parse("3be45095-cd49-4e5b-83e4-b144d3aee723"), "liuziyi"      },
            {Guid.Parse("97f88e27-633a-4c6b-a726-bd8a760a706c"), "maruining"    },
            {Guid.Parse("b39ff652-3219-4c39-9f5e-2966455d0b6a"), "niuyanrui"    },
            {Guid.Parse("7785adeb-1762-4d38-9675-3cc7b4cd3b1b"), "shenyuxiang"  },
            {Guid.Parse("2eafe32f-03d7-447d-9337-ff84a0fbec6a"), "tianqing"     },
            {Guid.Parse("d785f0ce-8c13-4a63-91f3-03a043127e8f"), "wangyuxiang"  },
            {Guid.Parse("92043dee-5631-4cd7-a8be-958393d22c6b"), "wangyuzhuo"   },
            {Guid.Parse("c93a67a2-26d2-4809-8925-bc9e7838be03"), "xialongyu"    },
            {Guid.Parse("db638d6a-71ad-4c7e-805c-a5e934eacab4"), "xuxuelan"     },
            {Guid.Parse("0585694d-9395-4e13-8557-1115c619caa8"), "yanzhe"       },
            {Guid.Parse("b30e3d99-c117-4ac8-8503-0c2c3dfc8ff0"), "yangzhuqin"   },
            {Guid.Parse("6cb79c31-860f-4994-9b55-03153b221e4f"), "yuxiaoxuan"   },
            {Guid.Parse("df201651-17d6-4cc7-94bf-1534a52f204d"), "zhangjunyang" },
            {Guid.Parse("767f9a00-e474-457c-872e-3727abb9a7e3"), "zhangtingxuan"},
            {Guid.Parse("3b3bc54b-ee70-43ba-9365-57ebe1f6b777"), "zhangyuning"  },
            {Guid.Parse("1dff4460-7e25-4e2e-bc1e-3c5f6b298b05"), "zhongxinying" },
            {Guid.Parse("122d4027-faa3-4342-8e34-2c14df2fd3a5"), "wangyuezhang" },
            {Guid.Parse("2381756d-a948-4330-9144-18816aa5966e"), "wangyongqin"  },
            {Guid.Parse("cd2e364c-98d2-48e9-a0f2-5823a33360bb"), "GuanDY"       },
            {Guid.Parse("1bbd57a6-9bfc-4da8-b5ee-41ba5b227ad8"), "houboyu"      },
            {Guid.Parse("1d0ee46c-1f76-499d-ad63-40652533e8aa"), "zhangxingyi"  },
            {Guid.Parse("7842cc40-8d82-4b52-bc70-5dffb9ff1d2c"), "zhulin"       },
            {Guid.Parse("a255f7e9-d98f-4775-b2ad-0bcd3631845f"), "xialiyun"     }
        };
    }
}