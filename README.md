# PersonalBlog

## 🚀 项目简介

该个人博客是一个展示现代 Web 开发技术栈的全栈应用项目，采用 **Vue 3 + TypeScript** 前端和 **ASP.NET Core** 后端架构，演示了前后端分离开发的最佳实践。

## 🏗️ 技术栈

### 前端技术栈
- **[Vue 3](https://vuejs.org/)** - 渐进式 JavaScript 框架
- **[TypeScript](https://www.typescriptlang.org/)** - 带类型的 JavaScript 超集
- **[Vite](https://vitejs.dev/)** - 现代前端构建工具

### 后端技术栈
- **[ASP.NET Core](https://docs.microsoft.com/aspnet/core/)** - 跨平台 Web 框架
- **[C# 12](https://docs.microsoft.com/dotnet/csharp/)** - 现代面向对象语言
- **[.NET 9](https://dotnet.microsoft.com/)** - 统一的开发平台

## ⚡ 快速开始

### 环境要求
- **Node.js**: >= 20.19.0 
- **.NET**: >= 9.0
- **IDE**: Visual Studio Code (推荐)

### 📦 依赖安装

#### 前端依赖安装
```powershell
# 进入前端目录
cd Frontend

# 安装前端依赖（Vue、Vite、TypeScript等）
npm install
```

#### 后端依赖安装
```powershell
# 进入后端目录
cd Backend

# 恢复.NET包依赖
dotnet restore
```

**后端包依赖列表**：
- `Microsoft.AspNetCore.OpenApi` (9.0.6) - OpenAPI文档支持
- `Microsoft.AspNetCore.SpaProxy` (9.0.8) - SPA代理服务
- `SQLite` (3.13.0) - SQLite数据库
- `System.Data.SQLite.Core` (1.0.119) - SQLite核心库

> 💡 **提示**: .NET项目的依赖会在首次运行时自动恢复，但建议手动执行 `dotnet restore` 确保依赖完整。


### 🚀 完整设置流程

```powershell
# 1. 克隆项目(未克隆时)
git clone <[repository-url](https://github.com/polymath985/PersonalBlog.git)>
cd PersonalBlog/Project

# 2. 安装前端依赖
cd Frontend
npm install

# 3. 恢复后端依赖
cd ../Backend
dotnet restore

# 4. 返回根目录
cd ..

# 5. 启动项目（方式1：VS Code任务）
# 在VS Code中按 Ctrl+Shift+P，选择 "Tasks: Run Task" → "🚀 启动全栈应用"

# 6. 启动项目（方式2：手动启动）
# 终端1 - 启动后端
dotnet run --project Backend/Backend.csproj

# 终端2 - 启动前端
cd Frontend && npm run dev
```

### 启动应用

安装依赖后，在VS Code中按 `Ctrl+Shift+B` 快速启动前后端应用

## 📚 完整文档

本项目提供了详细的技术文档，帮助你深入理解现代 Web 开发：

文档结构：
```
📁 docs/
├── 🏛️ architecture/     # 架构设计文档
├── ⚙️ backend/          # 后端开发文档  
├── 🎨 frontend/         # 前端开发文档
└── 📝 guides/           # 开发指南和教程
```

## ✨ 项目特色

### 🎨 现代开发体验
- **极速热重载**: Vite + dotnet watch 毫秒级更新
- **类型安全**: 前后端全 TypeScript/C# 类型覆盖
- **智能提示**: 完整的代码补全和错误检查

### 🏗️ 架构最佳实践
- **前后端分离**: 清晰的职责分工和接口定义
- **组件化设计**: 可重用的 UI 组件和业务逻辑
- **RESTful API**: 标准的 HTTP 接口设计
- **响应式设计**: 适配各种屏幕尺寸

### 🔧 开发工具集成
- **VS Code 支持**: 完整的调试和开发体验
- **自动构建**: 一键构建和部署
- **代码质量**: 自动化的代码检查和格式化
- **开发调试**: 完善的错误处理和日志记录

## 📊 项目结构

```
Project/
├── 📁 docs/                    # 📚 项目文档
├── 📁 Backend/          # ⚙️ ASP.NET Core 后端
│   ├── Controllers/            #   API 控制器
│   ├── Models/                 #   数据模型  
│   └── Program.cs              #   应用启动
├── 📁 vueapp1.client/          # 🎨 Vue.js 前端
│   ├── src/                    #   源代码
│   │   ├── components/         #     Vue 组件
│   │   ├── assets/             #     静态资源
│   │   └── main.ts             #     应用入口
│   ├── package.json            #   依赖配置
│   └── vite.config.ts          #   构建配置
└── VueApp1.sln                 # Visual Studio 解决方案
```

**Happy Coding! 🎉**

