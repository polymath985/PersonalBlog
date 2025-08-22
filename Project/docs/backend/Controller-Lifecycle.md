# ASP.NET Core Controller 生命周期

## 📋 概述

ASP.NET Core Controller采用**每请求一实例**的生命周期模型。

## 🔄 生命周期步骤

### 1. 请求处理流程

```
HTTP请求 → 路由匹配 → 创建Controller实例 → 依赖注入 → 执行Action → 返回响应 → 销毁实例
```

### 2. 详细步骤

1. **请求到达**：ASP.NET Core接收HTTP请求
2. **路由匹配**：确定需要调用哪个Controller和Action
3. **实例创建**：通过依赖注入容器创建Controller实例
4. **构造函数执行**：调用Controller构造函数，注入依赖
5. **Action执行**：调用对应的Action方法
6. **响应发送**：返回结果给客户端
7. **实例销毁**：Controller实例被GC回收

## 👥 并发特性

### 多用户场景

```
时间T1:
用户A请求 → Controller实例A
用户B请求 → Controller实例B  ← 同时进行
用户C请求 → Controller实例C

时间T2:
所有实例处理完成后被销毁
```

### 关键特点

- ✅ **线程安全**：每个请求有独立的Controller实例
- ✅ **无状态**：Controller不应保存请求间的状态
- ✅ **高并发**：可同时处理数千个请求

## 🎯 最佳实践

### Controller职责
- 处理HTTP请求/响应
- 参数验证
- 调用业务服务
- 返回结果

### 避免的做法
- ❌ 在Controller中维护状态
- ❌ 在构造函数中执行复杂业务逻辑
- ❌ 直接操作数据存储

## 📚 相关文档

- [Singleton与Controller配合使用](./Singleton-Controller.md)
- [依赖注入最佳实践](./Dependency-Injection.md)
