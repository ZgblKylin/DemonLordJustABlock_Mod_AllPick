# AllPick

Steam 游戏《卡片魔王·只剩个头》（Demon Lord: Just a Block）的创意工坊 CodeMod。

[游戏商店页面](https://store.steampowered.com/app/3720420) | [创意工坊](https://steamcommunity.com/app/3720420/workshop/)

本mod全程使用[OpenCode](https://opencode.ai/)和[DeepSeek V4 Pro](https://www.deepseek.com/)制作，梁圣的恩情还不完！

---

## 功能

允许在原本需要多选一的场景中**全部选取**，移除资源消耗，优化武器机制：

| 场景 | 原版 | 本 Mod |
|------|------|--------|
| 宝箱/包裹掉落 | 多选一 | 全选 |
| 铁匠墨菲升级武器 | 多选一 | 全选 |
| 选择能力卡片关卡 | 多选一 | 全选 |
| 商店购买 | 消耗金币 | 无需消耗 |
| 抽奖/开宝箱/英雄苹果 | 消耗金币 | 无需消耗 |
| 斗蛐蛐下注 | 每次消耗 2 金币 | 无需消耗 |
| 奇遇（需支付金币/血量） | 消耗对应资源 | 无需消耗 |
| 弩箭（诸神连弩） | 20 发后需 8 次装填 | 无限连射，无需装填 |

---

## 项目结构

```
AllPick/
├── README.md
├── mod.json                  # Mod 元信息
├── preview.png               # 创意工坊预览图
└── CodeMods/
    ├── codemod.json          # 入口配置
    ├── AllPickMod.cs         # Mod 源码
    ├── AllPickMod.csproj     # 项目文件
    └── AllPickMod.dll        # 编译产物
```

---

## 文件说明

### mod.json

```json
{
  "title": "AllPick 全选",
  "description": "宝箱、铁匠、选卡关卡不再多选一，可全部选取；商店/奇遇/抽奖无需消耗资源；弩箭移除装填时间。",
  "author": "作者名",
  "version": "0.1.2"
}
```

### CodeMods/codemod.json

```json
{
  "dll": "AllPickMod.dll",
  "entryClass": "AllPickMod.Main",
  "displayName": "AllPick"
}
```

### CodeMods/AllPickMod.csproj

- 目标框架：`netstandard2.1`
- C# 语言版本：`7.3`
- 需要引用游戏目录下的 `Assembly-CSharp.dll` 和 `UnityEngine.CoreModule.dll`

---

## 开发

### 前置条件

- .NET SDK（支持 netstandard2.1）
- 游戏已安装（路径：`D:\SteamLibrary\steamapps\common\DemonLordJustABlock`）

### 构建

```bash
dotnet build CodeMods/AllPickMod.csproj
```

编译产物 `AllPickMod.dll` 生成在 `CodeMods/` 目录下。

### 本地测试

将以下文件复制到：

```
D:\SteamLibrary\steamapps\common\DemonLordJustABlock\DemonLordJustABlock_Data\Managed\CodeMods\
```

放入所有 mod 文件（`mod.json`、`CodeMods/` 目录及其内容）即可在游戏中加载。

### 发布

通过 Steam 创意工坊上传工具发布。

---

## Mod 开发 API 参考

所有 CodeMod 需继承 `SimpleModBehaviour`，关键入口：

```csharp
using UnityEngine;

namespace AllPickMod
{
    public class Main : SimpleModBehaviour
    {
        public override void OnModLoaded()
        {
            Log("AllPickMod 已加载");
        }

        public override void OnModUnloaded()
        {
            Log("AllPickMod 已卸载");
        }
    }
}
```

游戏核心类：`BattleObject`、`SingletonData<T>`、`GameController`、`GameUIWindow`、`UnitObject`、`ConfigManager`、`SkillConfigLoader`、`DialogueManager`。

参考已安装的其他 mod（位于 `D:\SteamLibrary\steamapps\workshop\content\3720420\`）了解具体 API 用法。
