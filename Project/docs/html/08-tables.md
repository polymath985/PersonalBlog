# 表格

> [上一章: 链接和图片](./07-links-images.md) | [返回主目录](./README.md) | [下一章: 表单](./09-forms.md)

表格是展示结构化数据的重要工具，特别适合呈现行列数据关系。正确使用表格可以让数据更加清晰易读，提高用户体验。

## 表格基本结构

HTML表格由多个元素组成，形成完整的表格结构。

### 基本语法
```html
<table>
    <tr>
        <td>单元格内容</td>
        <td>单元格内容</td>
    </tr>
</table>
```

### 完整表格结构
```html
<table>
    <!-- 表格标题 -->
    <caption>员工信息表</caption>
    
    <!-- 表头 -->
    <thead>
        <tr>
            <th>姓名</th>
            <th>职位</th>
            <th>部门</th>
            <th>薪资</th>
        </tr>
    </thead>
    
    <!-- 表格主体 -->
    <tbody>
        <tr>
            <td>张三</td>
            <td>前端工程师</td>
            <td>技术部</td>
            <td>15000</td>
        </tr>
        <tr>
            <td>李四</td>
            <td>后端工程师</td>
            <td>技术部</td>
            <td>18000</td>
        </tr>
        <tr>
            <td>王五</td>
            <td>产品经理</td>
            <td>产品部</td>
            <td>20000</td>
        </tr>
    </tbody>
    
    <!-- 表格底部 -->
    <tfoot>
        <tr>
            <td colspan="3">总计</td>
            <td>53000</td>
        </tr>
    </tfoot>
</table>
```

## 表格元素详解

### `<table>` - 表格容器
```html
<!-- 基本表格 -->
<table>
    <tr>
        <td>数据1</td>
        <td>数据2</td>
    </tr>
</table>

<!-- 带边框的表格（通过CSS） -->
<table style="border-collapse: collapse; width: 100%;">
    <tr>
        <td style="border: 1px solid #ddd; padding: 8px;">数据1</td>
        <td style="border: 1px solid #ddd; padding: 8px;">数据2</td>
    </tr>
</table>
```

### `<caption>` - 表格标题
```html
<table>
    <caption>2024年第一季度销售数据</caption>
    <thead>
        <tr>
            <th>月份</th>
            <th>销售额</th>
            <th>增长率</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1月</td>
            <td>100万</td>
            <td>15%</td>
        </tr>
        <tr>
            <td>2月</td>
            <td>120万</td>
            <td>20%</td>
        </tr>
        <tr>
            <td>3月</td>
            <td>140万</td>
            <td>16.7%</td>
        </tr>
    </tbody>
</table>

<!-- 样式化的标题 -->
<table>
    <caption style="font-size: 1.2em; font-weight: bold; margin-bottom: 10px; color: #333;">
        公司部门预算分配表
    </caption>
    <!-- 表格内容 -->
</table>
```

### `<thead>` - 表头区域
```html
<table>
    <thead>
        <tr>
            <th>产品名称</th>
            <th>规格型号</th>
            <th>单价</th>
            <th>库存数量</th>
            <th>操作</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>iPhone 15</td>
            <td>128GB</td>
            <td>¥5999</td>
            <td>50</td>
            <td>
                <button>编辑</button>
                <button>删除</button>
            </td>
        </tr>
    </tbody>
</table>
```

### `<tbody>` - 表格主体
```html
<table>
    <thead>
        <tr>
            <th>学生姓名</th>
            <th>语文</th>
            <th>数学</th>
            <th>英语</th>
            <th>总分</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>张小明</td>
            <td>85</td>
            <td>92</td>
            <td>88</td>
            <td>265</td>
        </tr>
        <tr>
            <td>李小红</td>
            <td>90</td>
            <td>87</td>
            <td>91</td>
            <td>268</td>
        </tr>
        <tr style="background-color: #f0f8ff;">
            <td>王小强</td>
            <td>78</td>
            <td>95</td>
            <td>82</td>
            <td>255</td>
        </tr>
    </tbody>
</table>
```

### `<tfoot>` - 表格脚部
```html
<table>
    <thead>
        <tr>
            <th>项目</th>
            <th>数量</th>
            <th>单价</th>
            <th>金额</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>办公桌</td>
            <td>10</td>
            <td>800</td>
            <td>8000</td>
        </tr>
        <tr>
            <td>办公椅</td>
            <td>15</td>
            <td>500</td>
            <td>7500</td>
        </tr>
        <tr>
            <td>电脑</td>
            <td>8</td>
            <td>6000</td>
            <td>48000</td>
        </tr>
    </tbody>
    <tfoot>
        <tr style="font-weight: bold; background-color: #f5f5f5;">
            <td colspan="3">合计</td>
            <td>63500</td>
        </tr>
        <tr>
            <td colspan="3">税费 (10%)</td>
            <td>6350</td>
        </tr>
        <tr style="font-weight: bold;">
            <td colspan="3">总计</td>
            <td>69850</td>
        </tr>
    </tfoot>
</table>
```

### `<th>` - 表头单元格
```html
<!-- 行表头和列表头 -->
<table>
    <tr>
        <th></th>
        <th>2022年</th>
        <th>2023年</th>
        <th>2024年</th>
    </tr>
    <tr>
        <th>收入</th>
        <td>1000万</td>
        <td>1200万</td>
        <td>1500万</td>
    </tr>
    <tr>
        <th>支出</th>
        <td>800万</td>
        <td>900万</td>
        <td>1100万</td>
    </tr>
    <tr>
        <th>利润</th>
        <td>200万</td>
        <td>300万</td>
        <td>400万</td>
    </tr>
</table>

<!-- 指定表头作用域 -->
<table>
    <thead>
        <tr>
            <th scope="col">课程</th>
            <th scope="col">学分</th>
            <th scope="col">成绩</th>
            <th scope="col">绩点</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th scope="row">高等数学</th>
            <td>4</td>
            <td>85</td>
            <td>3.5</td>
        </tr>
        <tr>
            <th scope="row">大学物理</th>
            <td>3</td>
            <td>90</td>
            <td>4.0</td>
        </tr>
    </tbody>
</table>
```

### `<td>` - 数据单元格
```html
<!-- 各种类型的数据 -->
<table>
    <thead>
        <tr>
            <th>产品</th>
            <th>图片</th>
            <th>价格</th>
            <th>评分</th>
            <th>操作</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>无线耳机</td>
            <td><img src="earphones.jpg" alt="无线耳机" width="50" height="50"></td>
            <td style="font-weight: bold; color: #e74c3c;">¥299</td>
            <td>★★★★☆ (4.2)</td>
            <td>
                <a href="buy.html">购买</a> | 
                <a href="details.html">详情</a>
            </td>
        </tr>
        <tr>
            <td>智能手表</td>
            <td><img src="watch.jpg" alt="智能手表" width="50" height="50"></td>
            <td style="font-weight: bold; color: #e74c3c;">¥999</td>
            <td>★★★★★ (4.8)</td>
            <td>
                <a href="buy.html">购买</a> | 
                <a href="details.html">详情</a>
            </td>
        </tr>
    </tbody>
</table>
```

## 单元格合并

### 水平合并 `colspan`
```html
<table>
    <caption>公司组织架构</caption>
    <tr>
        <th colspan="4">总经理</th>
    </tr>
    <tr>
        <th colspan="2">技术总监</th>
        <th colspan="2">市场总监</th>
    </tr>
    <tr>
        <th>前端组</th>
        <th>后端组</th>
        <th>销售组</th>
        <th>推广组</th>
    </tr>
    <tr>
        <td>5人</td>
        <td>8人</td>
        <td>10人</td>
        <td>6人</td>
    </tr>
</table>

<!-- 课程表示例 -->
<table>
    <caption>周课程表</caption>
    <thead>
        <tr>
            <th>时间</th>
            <th>周一</th>
            <th>周二</th>
            <th>周三</th>
            <th>周四</th>
            <th>周五</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th>8:00-10:00</th>
            <td>高等数学</td>
            <td>英语</td>
            <td>高等数学</td>
            <td>物理</td>
            <td>英语</td>
        </tr>
        <tr>
            <th>10:00-12:00</th>
            <td colspan="2">实验课</td>
            <td>编程</td>
            <td colspan="2">项目实习</td>
        </tr>
        <tr>
            <th>14:00-16:00</th>
            <td>编程</td>
            <td>物理</td>
            <td colspan="3">大型讲座</td>
        </tr>
    </tbody>
</table>
```

### 垂直合并 `rowspan`
```html
<table>
    <caption>销售业绩统计</caption>
    <thead>
        <tr>
            <th>地区</th>
            <th>销售员</th>
            <th>第一季度</th>
            <th>第二季度</th>
            <th>总计</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td rowspan="3">华北区</td>
            <td>张三</td>
            <td>100万</td>
            <td>120万</td>
            <td>220万</td>
        </tr>
        <tr>
            <td>李四</td>
            <td>80万</td>
            <td>90万</td>
            <td>170万</td>
        </tr>
        <tr>
            <td>王五</td>
            <td>95万</td>
            <td>105万</td>
            <td>200万</td>
        </tr>
        <tr>
            <td rowspan="2">华南区</td>
            <td>赵六</td>
            <td>110万</td>
            <td>130万</td>
            <td>240万</td>
        </tr>
        <tr>
            <td>钱七</td>
            <td>85万</td>
            <td>95万</td>
            <td>180万</td>
        </tr>
    </tbody>
</table>
```

### 复杂合并示例
```html
<table>
    <caption>产品规格对比表</caption>
    <thead>
        <tr>
            <th rowspan="2">产品型号</th>
            <th colspan="3">硬件配置</th>
            <th rowspan="2">价格</th>
        </tr>
        <tr>
            <th>处理器</th>
            <th>内存</th>
            <th>存储</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td rowspan="2">MacBook Air</td>
            <td>M2</td>
            <td>8GB</td>
            <td>256GB</td>
            <td>¥8999</td>
        </tr>
        <tr>
            <td>M2</td>
            <td>16GB</td>
            <td>512GB</td>
            <td>¥11999</td>
        </tr>
        <tr>
            <td rowspan="2">MacBook Pro</td>
            <td>M2 Pro</td>
            <td>16GB</td>
            <td>512GB</td>
            <td>¥15999</td>
        </tr>
        <tr>
            <td>M2 Max</td>
            <td>32GB</td>
            <td>1TB</td>
            <td>¥25999</td>
        </tr>
    </tbody>
</table>
```

## 表格样式和布局

### 基本样式控制
```html
<!-- 带样式的基础表格 -->
<table style="border-collapse: collapse; width: 100%; font-family: Arial, sans-serif;">
    <caption style="font-size: 1.2em; margin-bottom: 10px; font-weight: bold;">
        员工考勤统计表
    </caption>
    
    <thead>
        <tr style="background-color: #4CAF50; color: white;">
            <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">姓名</th>
            <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">部门</th>
            <th style="padding: 12px; text-align: center; border: 1px solid #ddd;">出勤天数</th>
            <th style="padding: 12px; text-align: center; border: 1px solid #ddd;">迟到次数</th>
            <th style="padding: 12px; text-align: center; border: 1px solid #ddd;">状态</th>
        </tr>
    </thead>
    
    <tbody>
        <tr style="background-color: #f2f2f2;">
            <td style="padding: 12px; border: 1px solid #ddd;">张三</td>
            <td style="padding: 12px; border: 1px solid #ddd;">技术部</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd;">22</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd;">1</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd; color: green; font-weight: bold;">优秀</td>
        </tr>
        <tr>
            <td style="padding: 12px; border: 1px solid #ddd;">李四</td>
            <td style="padding: 12px; border: 1px solid #ddd;">销售部</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd;">20</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd;">3</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd; color: orange; font-weight: bold;">良好</td>
        </tr>
        <tr style="background-color: #f2f2f2;">
            <td style="padding: 12px; border: 1px solid #ddd;">王五</td>
            <td style="padding: 12px; border: 1px solid #ddd;">人事部</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd;">18</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd;">5</td>
            <td style="padding: 12px; text-align: center; border: 1px solid #ddd; color: red; font-weight: bold;">需改进</td>
        </tr>
    </tbody>
</table>
```

### 响应式表格
```html
<!-- 移动端友好的表格 -->
<div style="overflow-x: auto;">
    <table style="border-collapse: collapse; min-width: 600px;">
        <caption>移动端响应式表格</caption>
        <thead>
            <tr style="background-color: #333; color: white;">
                <th style="padding: 10px; white-space: nowrap;">产品名称</th>
                <th style="padding: 10px; white-space: nowrap;">规格型号</th>
                <th style="padding: 10px; white-space: nowrap;">制造商</th>
                <th style="padding: 10px; white-space: nowrap;">上市日期</th>
                <th style="padding: 10px; white-space: nowrap;">建议零售价</th>
                <th style="padding: 10px; white-space: nowrap;">当前库存</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">iPhone 15 Pro</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">A3108</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">Apple Inc.</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">2023-09-22</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;">¥8999</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd; text-align: center;">156</td>
            </tr>
            <tr>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">Galaxy S24</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">SM-S921</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">Samsung</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd;">2024-01-24</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;">¥5999</td>
                <td style="padding: 10px; border-bottom: 1px solid #ddd; text-align: center;">89</td>
            </tr>
        </tbody>
    </table>
</div>
```

## 实际应用场景

### 财务报表
```html
<section class="financial-report">
    <table>
        <caption>2024年第一季度财务报告（单位：万元）</caption>
        
        <thead>
            <tr>
                <th scope="col">项目</th>
                <th scope="col">本期金额</th>
                <th scope="col">上期金额</th>
                <th scope="col">增减额</th>
                <th scope="col">增减率</th>
            </tr>
        </thead>
        
        <tbody>
            <tr>
                <th scope="row">营业收入</th>
                <td style="text-align: right;">1,500</td>
                <td style="text-align: right;">1,200</td>
                <td style="text-align: right; color: green;">+300</td>
                <td style="text-align: right; color: green;">+25%</td>
            </tr>
            <tr>
                <th scope="row">营业成本</th>
                <td style="text-align: right;">900</td>
                <td style="text-align: right;">750</td>
                <td style="text-align: right; color: red;">+150</td>
                <td style="text-align: right; color: red;">+20%</td>
            </tr>
            <tr>
                <th scope="row">销售费用</th>
                <td style="text-align: right;">200</td>
                <td style="text-align: right;">180</td>
                <td style="text-align: right; color: red;">+20</td>
                <td style="text-align: right; color: red;">+11.1%</td>
            </tr>
            <tr>
                <th scope="row">管理费用</th>
                <td style="text-align: right;">150</td>
                <td style="text-align: right;">140</td>
                <td style="text-align: right; color: red;">+10</td>
                <td style="text-align: right; color: red;">+7.1%</td>
            </tr>
            <tr style="border-top: 2px solid #333; font-weight: bold;">
                <th scope="row">净利润</th>
                <td style="text-align: right;">250</td>
                <td style="text-align: right;">130</td>
                <td style="text-align: right; color: green;">+120</td>
                <td style="text-align: right; color: green;">+92.3%</td>
            </tr>
        </tbody>
    </table>
</section>
```

### 产品比较表
```html
<section class="product-comparison">
    <table>
        <caption>云服务器套餐对比</caption>
        
        <thead>
            <tr>
                <th scope="col">套餐特性</th>
                <th scope="col">基础版<br><small>¥99/月</small></th>
                <th scope="col">标准版<br><small>¥299/月</small></th>
                <th scope="col">专业版<br><small>¥599/月</small></th>
                <th scope="col">企业版<br><small>¥999/月</small></th>
            </tr>
        </thead>
        
        <tbody>
            <tr>
                <th scope="row">CPU核心数</th>
                <td style="text-align: center;">1核</td>
                <td style="text-align: center;">2核</td>
                <td style="text-align: center;">4核</td>
                <td style="text-align: center;">8核</td>
            </tr>
            <tr>
                <th scope="row">内存</th>
                <td style="text-align: center;">2GB</td>
                <td style="text-align: center;">4GB</td>
                <td style="text-align: center;">8GB</td>
                <td style="text-align: center;">16GB</td>
            </tr>
            <tr>
                <th scope="row">存储空间</th>
                <td style="text-align: center;">40GB SSD</td>
                <td style="text-align: center;">80GB SSD</td>
                <td style="text-align: center;">160GB SSD</td>
                <td style="text-align: center;">320GB SSD</td>
            </tr>
            <tr>
                <th scope="row">带宽</th>
                <td style="text-align: center;">1M</td>
                <td style="text-align: center;">3M</td>
                <td style="text-align: center;">5M</td>
                <td style="text-align: center;">10M</td>
            </tr>
            <tr>
                <th scope="row">数据库</th>
                <td style="text-align: center;">❌</td>
                <td style="text-align: center;">✅ MySQL</td>
                <td style="text-align: center;">✅ MySQL + Redis</td>
                <td style="text-align: center;">✅ 全部</td>
            </tr>
            <tr>
                <th scope="row">SSL证书</th>
                <td style="text-align: center;">❌</td>
                <td style="text-align: center;">✅</td>
                <td style="text-align: center;">✅</td>
                <td style="text-align: center;">✅</td>
            </tr>
            <tr>
                <th scope="row">技术支持</th>
                <td style="text-align: center;">社区</td>
                <td style="text-align: center;">邮件</td>
                <td style="text-align: center;">电话+邮件</td>
                <td style="text-align: center;">7×24小时</td>
            </tr>
            <tr>
                <th scope="row"></th>
                <td style="text-align: center;">
                    <button style="background: #007cba; color: white; border: none; padding: 8px 16px; cursor: pointer;">
                        选择基础版
                    </button>
                </td>
                <td style="text-align: center;">
                    <button style="background: #28a745; color: white; border: none; padding: 8px 16px; cursor: pointer;">
                        选择标准版
                    </button>
                </td>
                <td style="text-align: center;">
                    <button style="background: #ffc107; color: black; border: none; padding: 8px 16px; cursor: pointer;">
                        选择专业版
                    </button>
                </td>
                <td style="text-align: center;">
                    <button style="background: #dc3545; color: white; border: none; padding: 8px 16px; cursor: pointer;">
                        选择企业版
                    </button>
                </td>
            </tr>
        </tbody>
    </table>
</section>
```

### 数据统计表
```html
<section class="statistics-table">
    <table>
        <caption>网站访问统计（最近7天）</caption>
        
        <thead>
            <tr>
                <th scope="col">日期</th>
                <th scope="col">页面浏览量</th>
                <th scope="col">独立访客</th>
                <th scope="col">跳出率</th>
                <th scope="col">平均停留时间</th>
                <th scope="col">转化率</th>
            </tr>
        </thead>
        
        <tbody>
            <tr>
                <th scope="row">2024-01-15</th>
                <td style="text-align: right;">12,456</td>
                <td style="text-align: right;">8,921</td>
                <td style="text-align: right;">45.2%</td>
                <td style="text-align: right;">2分35秒</td>
                <td style="text-align: right; color: green; font-weight: bold;">3.2%</td>
            </tr>
            <tr style="background-color: #fff3cd;">
                <th scope="row">2024-01-16</th>
                <td style="text-align: right;">15,789</td>
                <td style="text-align: right;">11,234</td>
                <td style="text-align: right;">42.8%</td>
                <td style="text-align: right;">3分12秒</td>
                <td style="text-align: right; color: green; font-weight: bold;">4.1%</td>
            </tr>
            <tr>
                <th scope="row">2024-01-17</th>
                <td style="text-align: right;">13,567</td>
                <td style="text-align: right;">9,876</td>
                <td style="text-align: right;">48.1%</td>
                <td style="text-align: right;">2分18秒</td>
                <td style="text-align: right;">2.9%</td>
            </tr>
            <tr>
                <th scope="row">2024-01-18</th>
                <td style="text-align: right;">14,234</td>
                <td style="text-align: right;">10,123</td>
                <td style="text-align: right;">43.6%</td>
                <td style="text-align: right;">2分48秒</td>
                <td style="text-align: right;">3.5%</td>
            </tr>
            <tr>
                <th scope="row">2024-01-19</th>
                <td style="text-align: right;">16,892</td>
                <td style="text-align: right;">12,456</td>
                <td style="text-align: right;">39.7%</td>
                <td style="text-align: right;">3分42秒</td>
                <td style="text-align: right; color: green; font-weight: bold;">4.8%</td>
            </tr>
            <tr>
                <th scope="row">2024-01-20</th>
                <td style="text-align: right;">18,567</td>
                <td style="text-align: right;">13,789</td>
                <td style="text-align: right;">37.2%</td>
                <td style="text-align: right;">4分15秒</td>
                <td style="text-align: right; color: green; font-weight: bold;">5.2%</td>
            </tr>
            <tr>
                <th scope="row">2024-01-21</th>
                <td style="text-align: right;">11,234</td>
                <td style="text-align: right;">8,456</td>
                <td style="text-align: right;">51.3%</td>
                <td style="text-align: right;">2分05秒</td>
                <td style="text-align: right; color: orange;">2.1%</td>
            </tr>
        </tbody>
        
        <tfoot>
            <tr style="border-top: 2px solid #333; font-weight: bold; background-color: #f8f9fa;">
                <th scope="row">平均值</th>
                <td style="text-align: right;">14,548</td>
                <td style="text-align: right;">10,694</td>
                <td style="text-align: right;">44.0%</td>
                <td style="text-align: right;">2分59秒</td>
                <td style="text-align: right;">3.7%</td>
            </tr>
        </tfoot>
    </table>
</section>
```

### 课程时间表
```html
<section class="schedule-table">
    <table>
        <caption>计算机科学专业课程表 - 2024春季学期</caption>
        
        <thead>
            <tr>
                <th scope="col">时间</th>
                <th scope="col">周一</th>
                <th scope="col">周二</th>
                <th scope="col">周三</th>
                <th scope="col">周四</th>
                <th scope="col">周五</th>
            </tr>
        </thead>
        
        <tbody>
            <tr>
                <th scope="row">08:00-09:40</th>
                <td style="background-color: #e3f2fd; padding: 8px;">
                    <strong>数据结构</strong><br>
                    <small>A101 | 张教授</small>
                </td>
                <td style="background-color: #f3e5f5; padding: 8px;">
                    <strong>算法设计</strong><br>
                    <small>B203 | 李教授</small>
                </td>
                <td style="background-color: #e3f2fd; padding: 8px;">
                    <strong>数据结构</strong><br>
                    <small>A101 | 张教授</small>
                </td>
                <td style="background-color: #e8f5e8; padding: 8px;">
                    <strong>操作系统</strong><br>
                    <small>C305 | 王教授</small>
                </td>
                <td style="background-color: #fff3e0; padding: 8px;">
                    <strong>数据库原理</strong><br>
                    <small>D107 | 赵教授</small>
                </td>
            </tr>
            
            <tr>
                <th scope="row">10:00-11:40</th>
                <td style="background-color: #f3e5f5; padding: 8px;">
                    <strong>算法设计</strong><br>
                    <small>B203 | 李教授</small>
                </td>
                <td style="background-color: #e8f5e8; padding: 8px;">
                    <strong>操作系统</strong><br>
                    <small>C305 | 王教授</small>
                </td>
                <td style="background-color: #fff3e0; padding: 8px;">
                    <strong>数据库原理</strong><br>
                    <small>D107 | 赵教授</small>
                </td>
                <td style="background-color: #f3e5f5; padding: 8px;">
                    <strong>算法设计</strong><br>
                    <small>B203 | 李教授</small>
                </td>
                <td style="text-align: center; color: #666; font-style: italic;">
                    自由时间
                </td>
            </tr>
            
            <tr>
                <th scope="row">14:00-15:40</th>
                <td colspan="2" style="background-color: #ffebee; padding: 8px; text-align: center;">
                    <strong>软件工程实践</strong><br>
                    <small>实验室E201-E202 | 陈教授</small>
                </td>
                <td style="background-color: #e8f5e8; padding: 8px;">
                    <strong>操作系统</strong><br>
                    <small>C305 | 王教授</small>
                </td>
                <td colspan="2" style="background-color: #e1f5fe; padding: 8px; text-align: center;">
                    <strong>项目开发</strong><br>
                    <small>实验室F301-F302 | 团队导师</small>
                </td>
            </tr>
            
            <tr>
                <th scope="row">16:00-17:40</th>
                <td style="text-align: center; color: #666; font-style: italic;">
                    社团活动
                </td>
                <td style="background-color: #fff3e0; padding: 8px;">
                    <strong>数据库实验</strong><br>
                    <small>实验室D201 | 赵教授</small>
                </td>
                <td style="text-align: center; color: #666; font-style: italic;">
                    答疑时间
                </td>
                <td style="background-color: #ffebee; padding: 8px;">
                    <strong>软件工程</strong><br>
                    <small>G108 | 陈教授</small>
                </td>
                <td style="text-align: center; color: #666; font-style: italic;">
                    自由时间
                </td>
            </tr>
            
            <tr>
                <th scope="row">19:00-20:40</th>
                <td colspan="5" style="background-color: #f5f5f5; text-align: center; font-style: italic; padding: 8px;">
                    晚自习时间 / 图书馆开放 / 实验室预约使用
                </td>
            </tr>
        </tbody>
    </table>
    
    <div style="margin-top: 15px; font-size: 0.9em;">
        <p><strong>备注：</strong></p>
        <ul style="margin: 5px 0;">
            <li>实验课程需要提前15分钟到达实验室</li>
            <li>项目开发时间可根据团队需求调整</li>
            <li>答疑时间地点：各任课教师办公室</li>
            <li>课程调整请关注教务系统通知</li>
        </ul>
    </div>
</section>
```

## 表格的最佳实践

### 1. 可访问性
```html
<!-- ✅ 好的做法：提供完整的结构和属性 -->
<table>
    <caption>学生成绩表</caption>
    <thead>
        <tr>
            <th scope="col">学号</th>
            <th scope="col">姓名</th>
            <th scope="col">数学</th>
            <th scope="col">英语</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th scope="row">001</th>
            <td>张三</td>
            <td>85</td>
            <td>92</td>
        </tr>
    </tbody>
</table>

<!-- ❌ 避免的做法：缺少结构和语义 -->
<table>
    <tr>
        <td><b>学号</b></td>
        <td><b>姓名</b></td>
        <td><b>数学</b></td>
        <td><b>英语</b></td>
    </tr>
    <tr>
        <td>001</td>
        <td>张三</td>
        <td>85</td>
        <td>92</td>
    </tr>
</table>
```

### 2. 数据对齐
```html
<!-- ✅ 根据数据类型选择合适的对齐方式 -->
<table>
    <thead>
        <tr>
            <th style="text-align: left;">产品名称</th>
            <th style="text-align: right;">价格</th>
            <th style="text-align: center;">数量</th>
            <th style="text-align: right;">小计</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="text-align: left;">笔记本电脑</td>
            <td style="text-align: right;">¥5,999.00</td>
            <td style="text-align: center;">2</td>
            <td style="text-align: right;">¥11,998.00</td>
        </tr>
    </tbody>
</table>
```

### 3. 表格性能
```html
<!-- ✅ 避免过于复杂的嵌套表格 -->
<table>
    <caption>简洁的数据表格</caption>
    <!-- 简单清晰的结构 -->
</table>

<!-- ❌ 避免用表格做布局 -->
<!-- 使用CSS Grid或Flexbox代替 -->
```

### 4. 响应式处理
```html
<!-- ✅ 移动端适配方案 -->
<div style="overflow-x: auto;">
    <table style="min-width: 600px;">
        <!-- 表格内容 -->
    </table>
</div>

<!-- 或者使用媒体查询隐藏次要列 -->
<table>
    <thead>
        <tr>
            <th>主要信息</th>
            <th class="desktop-only">详细信息</th>
            <th>操作</th>
        </tr>
    </thead>
</table>
```

## 下一步学习

掌握了表格的创建和使用后，您已经完成了HTML的核心内容学习。接下来可以：

1. [表单](./09-forms.md) - 回顾HTML表单的创建和验证
2. [标签参考](./reference/tag-reference.md) - 查看完整的HTML标签列表
3. 开始学习CSS来美化您的表格和网页

---

> 💡 **记住**: 表格应该用于展示数据，而不是页面布局。始终为表格提供有意义的标题和表头，确保数据清晰易读！

*[上一章: 链接和图片](./07-links-images.md) | [返回主目录](./README.md) | [下一章: 表单](./09-forms.md)*
