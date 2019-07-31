## Native.SDK 优点介绍

> 1. 程序集脱库打包
> 2. 原生c#开发体验
> 3. 完美翻译酷QApi
> 4. 支持酷Q应用打包
> 5. 支持代码实时调试

## Native.SDK 项目结构

![SDK结构](https://github.com/Jie2GG/Image/blob/master/NativeSDK(0).png "SDK结构") <br/>

## Native.SDK 开发环境

>1. Visual Studio 2012 或更高版本
>2. Microsoft .Net Framework 4.0 **(XP系统支持的最后一个版本)**

## Native.SDK 开发流程

	1. 下载并打开 Native.SDK
	2. 打开 Native.Csharp 项目属性, 修改 "应用程序" 中的 "程序集名称" 为你的AppId(规则参见http://d.cqp.me/Pro/开发/基础信息)
	3. 展开 Native.Csharp 项目, 修改 "Native.Csharp.json" 文件名为你的AppId
	4. 展开 Native.Csharp 项目, 找到 App -> Core -> LibExport.tt 文件, 右击选择 "运行自定义工具"
	
	此时 Native.SDK 的开发环境已经配置成功!
	要找到生成的 程序集, 请找 Native.Csharp -> bin -> x86 -> (Debug\Release) 

## Native.SDK 调试流程

	1. 打开菜单 生成 -> 配置管理器, 修改 "Native.Csharp" 项目的生成方式为 "Debug x86" 生成方式
	2. 打开项目 Native.Csharp 项目属性, 修改 "生成" 中的 "输出路径" 至酷Q的 "app" 目录
	3. 修改 "调试" 中的 "启动操作" 为 "启动外部程序", 并且定位到酷Q主程序
	4. 打开菜单 工具 -> 选项 -> 调试, 关闭 "要求源文件与原始版本匹配" 选项
	
	若还是不行调试?
	5. 打开项目 Native.Csharp 项目属性, 打开 "调试" 中的 "启用本地代码调试" 选项, 保存即可
	
	此时 Native.SDK 已经可以进行实时调试!

## Native.SDK 已知问题
	
> 1. ~~对于 "EnApi.GetMsgFont" 方法, 暂时无法根据酷Q回传的指针获取字体信息, 暂时无法使用~~ <span style="color:red">(由于酷Q不解析此参数, 弃用)</span>
> 2. ~~对于 "HttpHelper.GetData" 方法, 抛出异常, 暂时无法使用~~ <font color=#FF0000>(已经修复, 但是封装了新的HTTP类, 弃用)</font>
> 3. ~~对于 "AuthCode" 被多插件共用, 导致应用之间串数据~~ <font color=#FF0000>(已修复)</font>
> 4. ~~对于接收消息时, 颜文字表情, 特殊符号乱码, 当前正在寻找转换方式~~ <font color=#FF0000>(已修复)</font>

## Native.SDK 更新日志
> 2019年04月09日 版本: V2.7.3

	1. 修复 CqMsg 类针对 VS2012 的兼容问题
	2. 修复 HttpWebClient 类在增加 Cookies 时, 参数 "{0}" 为空字符串的异常
	3. 新增 HttpWebClient 类属性 "KeepAlive", 允许指定 HttpWebClient 在做请求时是否建立持续型的 Internal 连接

> 2019年04月06日 版本: V2.7.2

	1. 优化 Native.Csharp.Sdk 项目的结构, 修改类: CqApi 的命名空间
	2. 新增 消息解析类: CqMsg
	
``` C#
// 使用方法如下, 例如在群消息接受方法中
public void ReceiveGroupMessage (object sender, GroupMessageEventArgs e)
{
	var parseResult = CqMsg.Parse (e.Msg);		// 使用消息解析
	List<CqCode> cqCodes =  parseResult.Contents;	// 获取消息中所有的 CQ码
	
	// 此时, 获取到的 cqCodes 中就包含此条消息所有的 CQ码
}
```

> 2019年03月12日 版本: V2.7.1

	1. 新增 Sex 枚举中未知性别, 值为 255
	2. 优化 IOC 容器在获取对象时, 默认拉取所有注入的对象, 简化消息类接口的注入流程.

> 2019年03月03日 版本: V2.7.0

	本次更新于响应 "酷Q" 官方 "易语言 SDK" 的迭代更新
	
	1. 新增 CqApi.ReceiveImage (用于获取消息中 "图片" 的绝对路径)
	2. 新增 CqApi.GetSendRecordSupport (用于获取 "是否支持发送语音", 即用于区别 Air 和 Pro 版本之间的区别)
	3. 新增 CqApi.GetSendImageSupport (用于获取 "是否支持发送图片", 即用于区别 Air 和 Pro 版本指间的区别)
	4. 优化 CqApi.ReceiveRecord 方法, 使其获取到的语音路径为绝对路径, 而非相对路径

> 2019年02月26日 版本: V2.6.4

	1. 默认注释 Event_GroupMessage 中 ReceiveGroupMessage 方法的部分代码, 防止因为机器人复读群消息而禁言

> 2019年02月20日 版本: V2.6.3

	1. 还原 Event_AppMain.Resolvebackcall 方法的执行, 防止偶尔获取不到注入的类

> 2019年02月20日 版本: V2.6.2

	1. 更新 Native.Chsarp 项目的部分注释
	2. 新增 Event_AppMain.Initialize 方法, 位于 "Native.Csharp.App.Event" 下, 用于当作本项目的初始化方法
	3. 优化 Event_AppMain.Resolvebackcall 方法的执行, 默认将依据接口注入的类型全部实例化并取出分发到事件上 

> 2019年02月16日 版本: V2.6.1

	1. 优化 FodyWeavers.xml 配置, 为其加上注释. 方便开发者使用
	2. 修复 IniValue 中 ToType 方法导致栈溢出的

> 2019年01月26日 版本: V2.6.0

	说明: 此次更新重构了 Native.Csharp 项目, 全面采用依赖注入框架, 提升 SDK 可扩展性、可移植性
	注意: 此次更新核心代码重构, 开发者若要升级请备份好代码后升级.
	
	1. 新增 Unity 依赖注入框架, 提升 SDK 可扩展性,、可移植性
	2. 新增 LibExport 文件模板, 使用方式: 右击 "LibExport.tt" -> 运行自定义工具
	3. 新增 AppID 半自动填写 (运行 LibExport 模板即可生成)
	4. 新增 Event_AppMain 类, 改类主要用于注册回调和分发事件
	5. 新增 IEvent_AppStatus 接口, 用于实现 "酷Q事件"
	6. 新增 IEvent_DiscussMessage 接口, 用于实现 "讨论组事件"
	7. 新增 IEvent_FriendMessage 接口, 用于实现 "好友事件"
	8. 新增 IEvent_GroupMessage 接口, 用于实现 "群事件"
	9. 新增 IEvent_OtherMessage 接口, 用于实现 "其它事件"
	10. 新增 Ievent_UserExpand 接口, 用于事件开发者自定义的事件
	11. 修复 LibExport 中 "EventSystemGroupMemberDecrease" 事件传递的 FormQQ 不正确的问题 
	12. 优化 Model.GroupMessageEventArgs, 在其中添加 IsAnonymousMsg 的变量用于判断消息是否属于匿名消息
	13. 优化 对公共语言的支持
	14. 优化 对于原有不合理的事件分类进行重新整合.

> 2019年01月24日 版本: V2.5.0

	1. 新增 Fody 从 1.6.2 -> 3.2.1, 支持整体框架从 .Net Framework 4.0 到 .Net Framework 4.6.1, 开发者可以自行升级.

> 2019年01月23日 版本: V2.4.2

	1. 新增 IniObject.Load() 方法在加载了文件之后保持文件路径, 修改结束后可直接 Save() 不传路径参数保存
	2. 修复 IniObject.ToString() 方法的在转换 IniValue 时可能出错
	3. 补充 IniSection.ToString() 方法, 可以直接把当前实例转换为字符串, 可以直接被 IniObject.Parse() 解析
	4. 针对之后要推出的 Ini 配置项 序列化与反序列化问题做出优化

> 2019年01月22日 版本: V2.4.1

	1. 重载 IniSection 类的索引器, 现在获取值时 key 不存在不会抛异常, 而是返回 IniValue.Empty, 设置值时 key 不存在会直接调用 Add 方法将 key 加入到内部集合
	2. 重载 IniObject 类的 string 索引器, 现在设置 "节" 时, 不存在节会直接调用 Add 方法将节加入到内部集合

> 2019年01月21日 版本: V2.4.0
	
	说明: 本次更新主要解决 Native.Csharp.UI 项目不会被载入的问题, 描述为:当有多个 Native 开发的插件项目同时被酷
	      Q载入时, 会导致所有的插件项目只载入第一个 Native.Csharp.UI ! 所以请已经使用的 Native.Csharp.UI 项目
	      的开发者, 将现有的 Native.Charp.UI 项目的命名空间修改为其它命名空间(包括项目内的所有 xaml, cs)
	
	1. 移除 Native.Csharp.UI 项目, 保证后续被开发的项目窗体不会有冲突.
	
> 2019年01月21日 版本: V2.3.7

	1. 新增 IniValue 类针对基础数据类型转换时返回指定默认值的方法.

> 2019年01月20日 版本: V2.3.6

	1. 修复 IniObject 类针对解析过程中, 遇到 Value 中包含 "=", 从而导致匹配到的 Key 和 Value 不正确的问题 

> 2019年01月19日 版本: V2.3.5

	1. 新增 HttpWebClient 类针对 .Net Framework 4.0 的 https 的完整验证协议 感谢 @ycxYI[https://github.com/ycxYI]

> 2019年01月14日 版本: V2.3.4

	1. 修改 导出给酷Q的回调函数, 消息参数类型为 IntPtr 
	2. 修复 获取 酷Q GB18030 字符串, 转码异常的问题
	3. 修改 IniObject 类的继承类由 List<T> 转换为 Dictionary<TKey, TValue>
	4. 新增 IniObject 类的 string 类型索引器
	5. 新增 IniObject 类的 Add(IniSection) 方法, 默认以 IniSection.Name 作为键
	6. 新增 IniObject 类的 ToArray() 方法, 将返回一个 IniSection[]
	7. 重载 IniObject 类的 Add(string, IniSection) 方法, 无效化 string 参数, 默认以 IniSection.Name 作为键

> 2019年01月14日 版本: V2.3.3
	
	1. 还原 酷Q消息回调部分的导出函数的字符串指针 IntPtr -> String 类型, 修复此问题导致 酷Q 直接闪退
	
> 2019年01月11日 版本: V2.3.2

	1. 修复 传递给 酷Q 的消息编码不正确导致的许多文字在 QQ 无法正常显示的问题 感谢 @kotoneme[https://github.com/kotoneme], @gif8512 酷Q论坛[https://cqp.cc/home.php?mod=space&uid=454408&do=profile&from=space]
	2. 修复 酷Q 传递给 SDK 的消息由于编码不正确可能导致的开发问题 感谢 @kotoneme[https://github.com/kotoneme], @gif8512 酷Q论坛[https://cqp.cc/home.php?mod=space&uid=454408&do=profile&from=space]

> 2019年01月08日 版本: V2.3.1

	1. 修改 "nameof()" 方法调用为其等效的字符串形式, 修复 VS2012 编译报错

> 2018年12月29日 版本: V2.3.0
	
	说明: 此次更新改动较大, 请开发者在升级时备份好之前的代码!!!
	
	1. 分离了 Sdk.Cqp 为单独一个项目, 提升可移植性
	2. 修改 Native.Csharp.Sdk.Cqp.Api 中的 "EnApi" 的类名称为 "CqApi"
	3. 修改 "CqApi" 对象的构造方式, 由 "单例对象" 换为 "实例对象"
	4. 新增 "IniObject", "IniSection", "IniValue" , 位于 Native.Csharp.Tool.IniConfig.Linq (专门用于 Ini 配置项的类, 此类已完全面向对象)
	5. 弃用 Native.Csharp.Tool 中的 "IniFile" 类, (该类还能使用但是不再提供后续更新)

> 2018年12月13日 版本: V2.1.0

	1. 修复 DllExport 可能编译出失效的问题
  	2. 修复 异常处理 在try后依旧会向酷Q报告当前代码错误的问题
	3. 修复 异常处理 返回消息格式错误
	4. 修复 WPF窗体 多次加载会导致酷Q奔溃的问题
	5. 修复 "有效时间" 转换不正确 感谢 @BackRunner[https://github.com/backrunner]
	5. 分离 Sdk.Cqp.Tool -> Native.Csharp.Tool 提升代码可移植性

> 2018年12月07日 版本: V2.0.1

  	1. 修复 获取群列表永远为 null 感谢 @kotoneme[https://github.com/kotoneme]
	2. 修复 非简体中文系统下获取的字符串为乱码问题 感谢 @kotoneme[https://github.com/kotoneme]

> 2018年12月06日 版本: V2.0.0
	
	1. 重构 插件框架代码
	2. 修复 多插件同时运行时 "AuthCode" 发生串应用问题
	3. 优化 代码编译流程, 减少资源文件合并次数, 提升代码编译速率
	4. 优化 插件开发流程
	5. 新增 酷Q插件调试功能, 同时支持 Air/Pro 版本

> 2018年12月05日 版本: V1.1.2

	1. 修复 HttpWebClient 问题

> 2018年12月05日 版本: V1.1.1

	1. 尝试修复多应用由于 "AuthCode" 内存地址重复的问题导致调用API时会串应用的问题
	2. 优化SDK加载速度

> 2018年12月04日 版本: V1.1.0

	1. 由于酷Q废弃了消息接收事件中的 "font" 参数, SDK已经将其移除
	2. 修复 HttpHelper 类 "GetData" 方法中抛出异常
	3. 新增 HttpWebClient 类
	4. 新增 PUT, DELETE 请求方式
	5. 新增 在任何请求方式下 Cookies 提交, 回传, 自动合并更新
	6. 新增 在任何请求方式下 Headers 提交, 回传
	7. 新增 在任何请求方式下可以传入用于代理请求的 WebProxy 对象
	8. 新增 在任何请求方式下可以控制是否跟随响应 HTTP 服务器的重定向请求, 以及应和重定向次数
	9. 新增 可控制 "POST" 请求方式下的 "Content-Type" 标头的控制, 达到最大兼容性

> 2018年11月30日 版本: V1.0.0

	1. 打包上传项目
