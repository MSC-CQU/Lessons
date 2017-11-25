#coding=utf-8
import requests
import re
import time

# 爬虫的入口
url = "https://m.weibo.cn/api/comments/show?id=4177685162249811&page={}"

# 请求头
headers = {
    'Accept': 'application/json, text/plain, */*',  # 客户端接受的文件类型
    'Accept-Encoding': 'gzip, deflate, br',  # 客户端接受的编码类型
    'Accept-Language': 'zh-CN,en-US,en;q=0.5',  # 客户端接受的自然语言类型
    'Cache-Control': 'max-age=0',  # 缓存控制
    'Connection': 'keep-alive',  # 持续连接
    'Cookie': '_T_WM=4e75b3f2fe19d18cafda9cfdea5e770e;SUHB=08J7VxkO8znAVi;SCF=ApxC1l39kF8tEQGnrZLTwENj4sGPp4RSyevBT5VBdazCyffFxLiCpy3l5c1c17ffeQlqGxpsovawR9LzxcGZiUo.;ALF=1513931800;SUB=_2A253EUhuDeRhGeNO4lQQ8CrEzDmIHXVU-mgmrDV6PUJbktAKLXj2kW1NHetkT087ewP8WpHrkVJr3qCZZxmKyKTe;SUBP=0033WrSXqPxfM725Ws9jqgMF55529P9D9WhAID4q-GJppZ15LFAYfjno5JpX5K-hUgL.Fo-71KqpehBRS0-2dJLoI0jLxKML1-2LB--LxKML1-2LB-HldgHrqCH8SEHFBCHWSEH8SC-Rxb-41FH8SEHWSE-RxFH8SE-RBEHFS5tt;M_WEIBOCN_PARAMS=featurecode=20000320&oid=4177350105480613&luicode=10000011&lfid=1076035085348970&uicode=20000061&fid=4177350105480613;H5:PWA:UID=1',
    'Host': 'm.weibo.cn',  # 域名
    'Referer': 'https://m.weibo.cn/status/4177350105480613',  # 上一个页面的url
    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0;...) Gecko/20100101 Firefox/57.0'  # 客户端类型
}

i = 1  # 页码

all_commtent = []  # 用于保存所有评论的列表
while True:
    if i == 200:  # 爬取109页评论
        break     # 结束
    print("正在读取第%d页" % i)
    req = requests.get(url=url.format(i), headers=headers)  # 请求一个页面
    content = req.json()  # json解析
    if req.status_code == 200:  # 如果状态码为200 则请求成功
        try:
            data = content['data']
            # print(content)
            for j in range(len(data)):
                comment = data[j]  # 一条评论的所有信息
                created_at = comment['created_at']  # 发布时间
                source = comment['source']  # 微博来源
                user_name = comment['user']['screen_name']  # 用户名
                comment_text = re.sub("<.*?>", "", comment['text'])  # 评论文本内容
                #print(comment_text)
                all_commtent.append(comment_text)  # 将一条评论加入all_comment列表
        except:
            time.sleep(5)  # 休息5秒
    else:
        break
    i += 1  # 页码+1

# 因为json()返回的数据是随机的所以可能有重复，所以这里是一个去重操作
# 先把all_comment类型转换为set set不能有重复元素 再转换为list
all_commtent = list(set(all_commtent))

# 将获得的评论写入文件
# open()里传入参数的意思就是 文件名：weibo_comment.txt，打开方式：w，编码方式：utf-8
with open('weibo_comment.txt', 'w', encoding='utf-8') as f:
    for comm in all_commtent:
        f.write(comm + '\n')