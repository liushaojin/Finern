@echo off
color 2f
@set trunk_dir=D:\SVN\QtFrameDesignModel
pushd %trunk_dir%

md %trunk_dir%\Build

cd %trunk_dir%\Build

rem cmake -G 的选项指定要生成的工程类型，可以是各个VS的版本，也可以是NMake等

rem -DBINARY_DIR=%trunk_dir% 定义了一个宏变量，这是CMakeList.txt文件编写时的一个设置，其他用户可不管

cmake -G "Visual Studio 14.0" %trunk_dir% -DBINARY_DIR=%trunk_dir%


@echo on