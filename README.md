# Welcome to MattNode!

![spr_logo](https://github.com/user-attachments/assets/6760c6d5-9f28-47a4-8fc4-5b6deaae7c4a)

# Download

[Download .zip file](https://drive.google.com/file/d/1088BGTy-44ThtOLXyUbOEmx09ZzSHPb3/view?usp=sharing)

# Tutorial

[MattNode English Tutorial](https://www.notion.so/MattNode-English-Tutorial-958fdf74e0404d73a54edf12dc0ada78?pvs=21)

[Mattnode Korean Tutorial 한국어 튜토리얼](https://www.notion.so/Mattnode-Korean-Tutorial-b3c515eaa1294721aaf84217837793c9?pvs=21)

# Features

### 0. Node Editor

MattNode is a node editor that can be used in any system that requires a node structure.

### 1. Optimized

MattNode disables all nodes that are out of the screen efficiently, so it can be used comfortably without lag regardless of how many nodes there are.

### 2. U**niversal**

MattNode is not just a tool for chatterboxes. It does not use proprietary, non-generic standards, but uses generic standards such as scripts, functions, structures, csv, etc. So you can use it with your own chatterbox or any other system that uses node structures.

### 3. Export, node type settings

You can create as many node types and export files as you need, and you can specify which type of node to put in each export file, and which information from that node to put in.
For example, to implement a chatterbox, you could create four types:

1. Text (text to be displayed in the chatterbox)
2. Choice (choice to put in the chatterbox)
3. Script (script to be executed when a message is displayed)
4. Condition (function to return whether a particular choice appears)

You will probably create two export files:

1. TextTable.csv
2. Script.txt

Each file will contain the following information:

1. TextTable.csv
- All information (text, type, connected nodes) of Text, Choice type nodes
- Connected nodes and type information of Script, Condition type nodes
1. Script.txt
- All information (text, type, connected nodes) of Choice, Script nodes

This way, you can organize all the data needed for a typical chatterbox. If you want, you can create other node types that include structures, etc., or create more export files.
