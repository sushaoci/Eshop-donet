# DONET课程项目

## 项目概述

本项目是一个简易的电商平台。

核心功能分为以下几个模块：

> 用户模块
>  	1、注册：使用邮箱进行注册，且邮箱必须符合格式
>  	2、验证：使用邮箱进行注册时，通过发送邮件，点击邮件中的链接进行用户验证。
>  	3、登陆：使用账号密码进行登录
>  	4、查看和修改个人信息：在个人账户板块可以看到并修改个人信息
> 浏览商品模块
>  	1、浏览商品：用户在首页可以浏览
> 	 2、查看商品详情：用户点击商品详情可以进入详情页，包含等信息
> 购买商品模块
>  	1、添加购物车：用户可将感兴趣的商品添加进购物车
>  	2、浏览购物车：用户可以浏览已经加入购物车的商品
>  	3、删除购物车
>  	4、结算购物车

## 框架

前端：JQuery

后端：.NET Framework 4.5.2

数据库访问：EF5 + Linq

## 程序集说明

|      | 文件名                | 功能           | 语言        | 说明       |
| :--: | :-------------------- | -------------- | ----------- | ---------- |
|  1   | SendEmail.dll         | 发送认证邮件   | C#          | 私有程序集 |
|  2   | EntryptAndDetrypt.dll | 加密           | C#          | 私有程序集 |
|  3   | RandomCode.dll        | 生成随机码     | C#          | 共享程序集 |
|  4   | Verify.dll            | 验证格式       | c#          | 私有程序集 |
|  5   | CLRDLL.dll            | 验证格式       | c++/CLI     | 私有程序集 |
|  6   | CppDLL.dll            | 验证格式       | c++win32DLL | 动态链接库 |
|  7   | MSGBUS.dll            | 报错说明硬编码 | ATLCOM      | COM组件    |

`SendEmail.dll`此程序集用于注册过程中的验证邮件的发送。


`EntryptAndDetrypt.dll`此程序集用于可用于用户密码的加密和解析。

---

`RandomCode.dll`此程序集可以根据指定的长度和线程是否被唤醒来决定生成的随机码，在本项目中用于生成用户的初始名字。

**1.生成共享程序集的说明**

Q：所引用的程序集没有强命名

A：为此程序集生成一个密钥对，然后将程序集添加到程序集缓存中。最后引用程序集的时候会发现在该项目的文件夹中找不到引用该DLL的语句（私有程序集会存在该项目中），说明这就是引用了共享程序集。

---

`Verify.dll`，`CLRDLL.dll`，`CppDLL.dll`这三个程序集实现了一个共同的功能——格式验证。

**1. c#调用c++/cli托管c++的说明**

Q：c#中的string在c++/cli中为未知类型，且c++的char\*对应c#中的sbyte\*，而sbyte\*无法直接获取。

A：通过Encoding.Default.GetBytes()进行格式转换



Q：异常错误：System.BadImageFormatException: 试图加载格式不正确的程序。

A：由于64位程序是不能加载32位dll。我们更改项目属性进行重新配置：项目右键属性->项目设计器->生成->平台->把'默认设置(任何 CPU)'改为x86。

**2. c++动态链接库的说明**

Q：找不到该dll

A：将该动态链接库拷贝到项目生成之后的Debug或者release文件夹中



Q：c++参数类型改为char*，csharp传入为byte[]，不转换格式会报错（Debug Assertion Fail）

A：通过Encoding.Default.GetBytes()进行格式转换

---

`MSGBUS.dll`该COM组件对各种报错信息进行了硬编码。

**COM组件注册和调用**

Q：如果在bin下注册失败，报错为unable to start pargram

A：可以将DLL文件拷贝到\windows\syswow64目录中，然后再以管理员身份运行CMD.EXE，进入\windows\syswow64目录，利用regsvr32命令重新注册



Q：访问OLE注册表的错误

A：vs没有权限进行操作，以管理员身份运行Visual Studio就能解决问题



## 界面展示

#### 用户模块

 	1、注册：使用邮箱进行注册，且邮箱必须符合格式

![截屏2020-06-26 21.09.49](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg60wykztuj31c00u013s.jpg)

​	如果不符合邮箱的格式，会报错提示用户进行更改

![截屏2020-06-26 21.10.27](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg60yr8r7qj31c00u0tld.jpg)

 	2、验证：使用邮箱进行注册时，通过发送邮件，点击邮件中的链接进行用户验证。



 	3、登陆：使用账号密码进行登录

![截屏2020-06-26 21.09.34](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg60z7661zj31c00u049l.jpg)

 	4、查看和修改个人信息：在个人账户板块可以看到并修改个人信息

![截屏2020-06-26 21.50.37](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg615hau3vj31c00u07f3.jpg)

![截屏2020-06-26 21.50.43](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg615vejdfj31c00u0n8x.jpg)





#### 浏览商品模块

 	1、浏览商品：用户在首页可以浏览

![截屏2020-06-26 21.10.55](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg610y3luoj31c00u01kx.jpg)

​	 2、查看商品详情：用户点击商品详情可以进入详情页，包含等信息

![截屏2020-06-26 21.08.32](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg611886jsj31c00u04h3.jpg)







#### 购买商品模块

 	1、添加购物车：用户可将感兴趣的商品添加进购物车



 	2、浏览购物车：用户可以浏览已经加入购物车的商品

![截屏2020-06-26 21.11.35](https://tva1.sinaimg.cn/large/007S8ZIlgy1gg611trnphj31c00u018d.jpg)

 	3、删除购物车：用户可将加入购物车的商品删除



 	4、结算购物车：用户可以结算加入购物车的商品