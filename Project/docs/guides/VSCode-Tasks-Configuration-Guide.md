# VS Code Tasks 和快捷键配置详解

## 🎯 Ctrl+Shift+B 的工作原理

VS Code 的 `Ctrl+Shift+B` 是**默认构建快捷键**，它会自动寻找并执行标记为默认构建任务的任务。

## 🔧 关键配置解析

### 1. **group 配置**
```json
"group": {
    "kind": "build",      // 任务类型：build（构建）或 test（测试）
    "isDefault": true     // 设为默认任务，Ctrl+Shift+B 会直接执行
}
```

**kind 的选项**：
- `"build"`: 构建任务 → `Ctrl+Shift+B` 执行
- `"test"`: 测试任务 → `Ctrl+Shift+P` → `Tasks: Run Test Task`

### 2. **dependsOn 配置**
```json
"dependsOrder": "parallel",    // 并行执行依赖任务
"dependsOn": [                // 依赖的子任务
    "启动后端服务",
    "启动前端应用"
]
```

**dependsOrder 选项**：
- `"parallel"`: 同时执行所有依赖任务
- `"sequence"`: 按顺序执行依赖任务

### 3. **presentation 配置**
```json
"presentation": {
    "echo": true,              // 显示执行的命令
    "reveal": "always",        // 总是显示终端
    "focus": false,           // 不自动聚焦到终端
    "panel": "new",           // 每个任务使用新的终端面板
    "showReuseMessage": false, // 不显示重用终端消息
    "clear": true             // 执行前清空终端
}
```

## 🎨 其他常用快捷键配置

### 1. **自定义快捷键**

在 VS Code 中按 `Ctrl+K Ctrl+S` 打开键盘快捷方式，然后可以添加：

```json
// keybindings.json
[
    {
        "key": "ctrl+f5",                    // 自定义快捷键
        "command": "workbench.action.tasks.runTask",
        "args": "🚀 启动全栈应用"           // 任务名称
    },
    {
        "key": "ctrl+shift+f5",              // 停止所有任务
        "command": "workbench.action.tasks.terminate"
    },
    {
        "key": "f5",                         // F5 只启动后端
        "command": "workbench.action.tasks.runTask",
        "args": "启动后端服务"
    },
    {
        "key": "ctrl+f6",                    // Ctrl+F6 只启动前端
        "command": "workbench.action.tasks.runTask",
        "args": "启动前端应用"
    }
]
```

### 2. **任务选择快捷键**

```json
{
    "key": "ctrl+shift+t",                   // 选择任务执行
    "command": "workbench.action.tasks.runTask"
}
```

## 📝 完整的 tasks.json 模板

```json
{
    "version": "2.0.0",
    "tasks": [
        // 后端任务
        {
            "label": "启动后端服务",
            "type": "shell",
            "command": "dotnet run --project VueApp1.Server",
            "isBackground": true,
            "group": "build",
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": false,
                "panel": "new",
                "showReuseMessage": false,
                "clear": true
            },
            "problemMatcher": {
                "owner": "dotnet",
                "pattern": {
                    "regexp": "^(.*)\\((\\d+),(\\d+)\\):\\s+(error|warning|info)\\s+(.*):\\s+(.*)$",
                    "file": 1,
                    "line": 2,
                    "column": 3,
                    "severity": 4,
                    "code": 5,
                    "message": 6
                }
            }
        },
        
        // 前端任务
        {
            "label": "启动前端应用",
            "type": "shell",
            "command": "npm run dev",
            "options": {
                "cwd": "${workspaceFolder}/vueapp1.client"
            },
            "isBackground": true,
            "group": "build",
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": false,
                "panel": "new",
                "showReuseMessage": false,
                "clear": true
            },
            "problemMatcher": {
                "owner": "typescript",
                "fileLocation": ["relative", "${workspaceFolder}/vueapp1.client"],
                "pattern": {
                    "regexp": "^(.*)\\((\\d+),(\\d+)\\):\\s+(error|warning|info)\\s+TS(\\d+):\\s+(.*)$",
                    "file": 1,
                    "line": 2,
                    "column": 3,
                    "severity": 4,
                    "code": 5,
                    "message": 6
                }
            }
        },
        
        // 复合任务（默认构建任务）
        {
            "label": "🚀 启动全栈应用",
            "dependsOrder": "parallel",
            "dependsOn": [
                "启动后端服务",
                "启动前端应用"
            ],
            "group": {
                "kind": "build",
                "isDefault": true                // 🔑 这个设置让 Ctrl+Shift+B 直接执行
            },
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": false,
                "panel": "new"
            }
        },
        
        // 其他实用任务
        {
            "label": "🛑 停止所有服务",
            "type": "shell",
            "command": "echo '停止所有后台服务...'",
            "group": "build",
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": false,
                "panel": "shared"
            }
        },
        
        {
            "label": "🧹 清理项目",
            "type": "shell",
            "command": "echo '清理项目文件...' && cd vueapp1.client && rmdir /s /q node_modules dist 2>nul && cd ../VueApp1.Server && rmdir /s /q bin obj 2>nul && echo '清理完成'",
            "group": "build",
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": true,
                "panel": "shared"
            }
        },
        
        {
            "label": "📦 安装依赖",
            "type": "shell", 
            "command": "cd vueapp1.client && npm install",
            "group": "build",
            "presentation": {
                "echo": true,
                "reveal": "always",
                "focus": true,
                "panel": "shared"
            }
        }
    ]
}
```

## 🎮 所有可用快捷键

### **默认快捷键**
- `Ctrl+Shift+B`: 运行默认构建任务
- `Ctrl+Shift+P` → `Tasks: Run Task`: 选择任务运行
- `Ctrl+Shift+P` → `Tasks: Terminate Task`: 终止任务
- `Ctrl+Shift+P` → `Tasks: Restart Running Task`: 重启任务

### **自定义建议**
- `F5`: 启动后端
- `Ctrl+F5`: 启动前端  
- `Ctrl+Shift+F5`: 启动全栈应用
- `Shift+F5`: 停止所有服务

## 🔍 调试和问题排查

### **查看任务输出**
1. 打开 VS Code 终端面板 (`Ctrl+``)
2. 在终端右侧选择对应的任务
3. 查看实时输出和错误信息

### **任务不执行的常见原因**
1. ❌ `isDefault: true` 没设置
2. ❌ `group.kind` 不是 "build"
3. ❌ 命令路径错误
4. ❌ 依赖任务名称不匹配

### **检查任务状态**
- `Ctrl+Shift+P` → `Tasks: Show Running Tasks` 查看正在运行的任务
- 在问题面板 (`Ctrl+Shift+M`) 查看错误

## 💡 高级技巧

### **1. 条件任务执行**
```json
{
    "label": "条件启动",
    "type": "shell", 
    "command": "if not exist \"vueapp1.client\\node_modules\" (npm install) else (npm run dev)",
    "options": {
        "cwd": "${workspaceFolder}/vueapp1.client"
    }
}
```

### **2. 输入参数任务**
```json
{
    "label": "带参数的任务",
    "type": "shell",
    "command": "dotnet run --project VueApp1.Server --environment ${input:environment}",
    "group": "build"
}
```

配合输入定义：
```json
"inputs": [
    {
        "id": "environment",
        "description": "选择环境",
        "default": "Development",
        "type": "pickString",
        "options": [
            "Development",
            "Staging", 
            "Production"
        ]
    }
]
```

现在你知道为什么 `Ctrl+Shift+B` 这么方便了！关键就是 `"isDefault": true` 这个设置。🎉
