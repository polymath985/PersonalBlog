@echo off
chcp 65001 >nul
title VueApp1 启动器

echo.
echo ========================================
echo         VueApp1 一键启动
echo ========================================
echo.

echo 正在启动后端服务...
start "后端-ASP.NET Core" cmd /k "cd /d %~dp0VueApp1.Server && dotnet run"

echo 等待后端启动...
timeout /t 3 /nobreak >nul

echo 正在启动前端应用...
start "前端-Vue.js" cmd /k "cd /d %~dp0vueapp1.client && npm run dev"

echo.
echo ✅ 启动完成！
echo.
echo 🔗 访问地址:
echo    后端: http://localhost:5297
echo    前端: https://localhost:5157
echo.
echo 💡 两个服务会在新窗口中运行
echo    关闭对应窗口或按 Ctrl+C 可停止服务
echo.

pause
