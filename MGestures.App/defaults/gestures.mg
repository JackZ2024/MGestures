{
    "FileVersion": "3",
    "Apps": {
        "c:\\windows\\system32\\cmd.exe": {
            "$type": "MGestures.App.Gui.Model.OrderableExeApp, MGestures",
            "Order": 2,
            "Exists": true,
            "InheritGlobalGestures": true,
            "ExecutablePath": "c:\\windows\\system32\\cmd.exe",
            "Name": "cmd",
            "GestureIntents": [
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 0,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            4,
                            2
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.WindowControlCommand, MGestures.Core",
                        "ChangeWindowStateTo": 2
                    },
                    "ExecuteOnModifier": false,
                    "Name": "关闭"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 1,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [],
                        "Modifier": 4
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.WindowControlCommand, MGestures.Core",
                        "ChangeWindowStateTo": 2
                    },
                    "ExecuteOnModifier": false,
                    "Name": "关闭"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 2,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            4,
                            6
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.WindowControlCommand, MGestures.Core",
                        "ChangeWindowStateTo": 2
                    },
                    "ExecuteOnModifier": false,
                    "Name": "Quit"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 5,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            4
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [
                            164
                        ],
                        "Keys": [
                            32,
                            69,
                            80
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "粘贴"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 4,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            0
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [
                            164
                        ],
                        "Keys": [
                            32,
                            69,
                            89
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "复制"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 3,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [],
                        "Modifier": 8
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [
                            164
                        ],
                        "Keys": [
                            32,
                            69,
                            75
                        ]
                    },
                    "ExecuteOnModifier": true,
                    "Name": "标记"
                }
            ],
            "IsGesturingEnabled": true
        },
        "c:\\windows\\system32\\dllhost.exe": {
            "$type": "MGestures.App.Gui.Model.OrderableExeApp, MGestures",
            "Order": 3,
            "Exists": true,
            "InheritGlobalGestures": true,
            "ExecutablePath": "c:\\windows\\system32\\dllhost.exe",
            "Name": "图片查看",
            "GestureIntents": [
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 0,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [],
                        "Modifier": 2
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [],
                        "Keys": [
                            39
                        ]
                    },
                    "ExecuteOnModifier": true,
                    "Name": "下一张"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 1,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [],
                        "Modifier": 1
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [],
                        "Keys": [
                            37
                        ]
                    },
                    "ExecuteOnModifier": true,
                    "Name": "上一张"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 2,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            4,
                            2
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [
                            164
                        ],
                        "Keys": [
                            115
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "关闭"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 3,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [],
                        "Modifier": 4
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [
                            164
                        ],
                        "Keys": [
                            115
                        ]
                    },
                    "ExecuteOnModifier": true,
                    "Name": "关闭"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 4,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            2
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [],
                        "Keys": [
                            39
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "→"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 5,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            6
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [],
                        "Keys": [
                            37
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "←"
                }
            ],
            "IsGesturingEnabled": true
        },
        "c:\\windows\\explorer.exe": {
            "$type": "MGestures.App.Gui.Model.OrderableExeApp, MGestures",
            "Order": 1,
            "Exists": true,
            "InheritGlobalGestures": true,
            "ExecutablePath": "c:\\windows\\explorer.exe",
            "Name": "文件管理器",
            "GestureIntents": [
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 0,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            2,
                            4
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [
                            162
                        ],
                        "Keys": [
                            78
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "新窗口"
                },
                {
                    "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                    "Order": 1,
                    "Gesture": {
                        "GestureButton": 1,
                        "Dirs": [
                            2,
                            0,
                            6
                        ],
                        "Modifier": 0
                    },
                    "Command": {
                        "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                        "Modifiers": [],
                        "Keys": [
                            113
                        ]
                    },
                    "ExecuteOnModifier": false,
                    "Name": "重命名"
                }
            ],
            "IsGesturingEnabled": true
        }
    },
    "Global": {
        "Name": "(Global)",
        "GestureIntents": [
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 0,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        67
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "复制"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 1,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        86
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "粘贴"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 2,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4
                    ],
                    "Modifier": 8
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.SendTextCommand, MGestures.Core",
                    "Text": "^v{enter}"
                },
                "ExecuteOnModifier": false,
                "Name": "粘贴并回车"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 3,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0
                    ],
                    "Modifier": 8
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        88
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "剪切"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 4,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.WebSearchCommand, MGestures.Core",
                    "SearchEngineUrl": "https://bing.com/search?q={0}",
                    "SearchEngingName": "必应",
                    "UseBrowser": null
                },
                "ExecuteOnModifier": false,
                "Name": "Web搜索"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 5,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        87
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "关闭"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 6,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        6
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        164
                    ],
                    "Keys": [
                        115
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "退出"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 7,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        6
                    ],
                    "Modifier": 8
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.CmdCommand, MGestures.Core",
                    "Code": "taskkill /f /pid %WG_PROCID% /s localhost",
                    "ShowWindow": false,
                    "AutoSetWorkingDir": false
                },
                "ExecuteOnModifier": false,
                "Name": "强制退出"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 8,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2,
                        0,
                        2,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        90
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "撤销"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 9,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        8
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "退格"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 10,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6,
                        2,
                        6,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        46
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "删除"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 11,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        1
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.WindowControlCommand, MGestures.Core",
                    "ChangeWindowStateTo": 0
                },
                "ExecuteOnModifier": false,
                "Name": "最大化"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 12,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        5
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.WindowControlCommand, MGestures.Core",
                    "ChangeWindowStateTo": 1
                },
                "ExecuteOnModifier": false,
                "Name": "最小化"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 13,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        1
                    ],
                    "Modifier": 4
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        122
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "全屏"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 14,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        7
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        91
                    ],
                    "Keys": []
                },
                "ExecuteOnModifier": false,
                "Name": "开始"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 15,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        3
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        91
                    ],
                    "Keys": [
                        68
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "切换到桌面"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 16,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        13
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "Enter"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 17,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0,
                        2,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.ChangeAudioVolumeCommand, MGestures.Core",
                    "Delta": 1
                },
                "ExecuteOnModifier": false,
                "Name": "静音切换"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 18,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0,
                        2,
                        4
                    ],
                    "Modifier": 1
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.ChangeAudioVolumeCommand, MGestures.Core",
                    "Delta": 1
                },
                "ExecuteOnModifier": true,
                "Name": "音量"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 19,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2,
                        0,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        164
                    ],
                    "Keys": [
                        39
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "下一首"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 20,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2,
                        0,
                        2
                    ],
                    "Modifier": 4
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        164
                    ],
                    "Keys": [
                        80
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "播放/暂停"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 21,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2,
                        0,
                        2
                    ],
                    "Modifier": 2
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        164
                    ],
                    "Keys": [
                        39
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "下一首"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 22,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        4,
                        2,
                        0,
                        2
                    ],
                    "Modifier": 1
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        164
                    ],
                    "Keys": [
                        37
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "上一首"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 23,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        4,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        164
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "切换任务"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 24,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        4,
                        2
                    ],
                    "Modifier": 1
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.TaskSwitcherCommand, MGestures.Core"
                },
                "ExecuteOnModifier": true,
                "Name": "任务选择"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 25,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        4,
                        2,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.CmdCommand, MGestures.Core",
                    "Code": "dir",
                    "ShowWindow": true,
                    "AutoSetWorkingDir": true
                },
                "ExecuteOnModifier": false,
                "Name": "Dir"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 26,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        164
                    ],
                    "Keys": [
                        39
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "前进"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 27,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        164
                    ],
                    "Keys": [
                        37
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "后退"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 28,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        0
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        76
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "地址栏"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 29,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        0
                    ],
                    "Modifier": 4
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        68
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "添加书签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 30,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        84
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "新标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 31,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        2,
                        4
                    ],
                    "Modifier": 8
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        160
                    ],
                    "Keys": [
                        84
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "撤销关闭标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 32,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6,
                        0,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        116
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "刷新"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 33,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6,
                        0
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        36
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "Home"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 34,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6,
                        4
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [],
                    "Keys": [
                        35
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "End"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 35,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [],
                    "Modifier": 1
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        160,
                        162
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "上一个标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 36,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [],
                    "Modifier": 2
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "下一个标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 37,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [],
                    "Modifier": 8
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        84
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "新标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 38,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [],
                    "Modifier": 4
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        87
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "关闭"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 39,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        6,
                        0,
                        2
                    ],
                    "Modifier": 8
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        160
                    ],
                    "Keys": [
                        82
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "强制刷新"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 40,
                "Gesture": {
                    "GestureButton": 2,
                    "Dirs": [],
                    "Modifier": 1
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        160
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "上个标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 41,
                "Gesture": {
                    "GestureButton": 2,
                    "Dirs": [],
                    "Modifier": 2
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "下个标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 42,
                "Gesture": {
                    "GestureButton": 2,
                    "Dirs": [
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        164
                    ],
                    "Keys": [
                        39
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "下一首"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 43,
                "Gesture": {
                    "GestureButton": 2,
                    "Dirs": [
                        6
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        164
                    ],
                    "Keys": [
                        37
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "上一首"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 44,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0,
                        6
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        160
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "上一个标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 45,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0,
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        9
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "下一个标签"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 46,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0,
                        2,
                        0
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.WindowControlCommand, MGestures.Core",
                    "ChangeWindowStateTo": 3
                },
                "ExecuteOnModifier": false,
                "Name": "窗口置顶"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 47,
                "Gesture": {
                    "GestureButton": 4,
                    "Dirs": [
                        2
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        91
                    ],
                    "Keys": [
                        39
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "下一个桌面"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 48,
                "Gesture": {
                    "GestureButton": 4,
                    "Dirs": [
                        6
                    ],
                    "Modifier": 0
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162,
                        91
                    ],
                    "Keys": [
                        37
                    ]
                },
                "ExecuteOnModifier": false,
                "Name": "上一个桌面"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 49,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0
                    ],
                    "Modifier": 1
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        187
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "放大"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 50,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0
                    ],
                    "Modifier": 2
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        189
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "缩小"
            },
            {
                "$type": "MGestures.App.Gui.Model.OrderableIntent, MGestures",
                "Order": 51,
                "Gesture": {
                    "GestureButton": 1,
                    "Dirs": [
                        0
                    ],
                    "Modifier": 4
                },
                "Command": {
                    "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
                    "Modifiers": [
                        162
                    ],
                    "Keys": [
                        48
                    ]
                },
                "ExecuteOnModifier": true,
                "Name": "重置缩放"
            }
        ],
        "IsGesturingEnabled": true
    },
    "HotCornerCommands": [
        {
            "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
            "Modifiers": [
                91
            ],
            "Keys": []
        },
        {
            "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
            "Modifiers": [
                164
            ],
            "Keys": [
                9
            ]
        },
        {
            "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
            "Modifiers": [
                91
            ],
            "Keys": [
                9
            ]
        },
        {
            "$type": "MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core",
            "Modifiers": [
                91
            ],
            "Keys": [
                68
            ]
        },
        {
            "$type": "MGestures.Core.Commands.Impl.CmdCommand, MGestures.Core",
            "Code": "::控制面板\r\ncontrol ",
            "ShowWindow": false,
            "AutoSetWorkingDir": false
        },
        {
            "$type": "MGestures.Core.Commands.Impl.CmdCommand, MGestures.Core",
            "Code": "::锁定Windows\r\nrundll32.exe user32.dll,LockWorkStation",
            "ShowWindow": false,
            "AutoSetWorkingDir": false
        },
        {
            "$type": "MGestures.Core.Commands.Impl.CmdCommand, MGestures.Core",
            "Code": " ::任务管理器\r\ntaskmgr",
            "ShowWindow": false,
            "AutoSetWorkingDir": false
        },
        {
            "$type": "MGestures.Core.Commands.Impl.CmdCommand, MGestures.Core",
            "Code": "::立即关机\r\nshutdown -s -t 0",
            "ShowWindow": true,
            "AutoSetWorkingDir": false
        }
    ]
}