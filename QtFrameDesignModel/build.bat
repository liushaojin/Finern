@echo off
color 2f
@set trunk_dir=D:\SVN\QtFrameDesignModel
pushd %trunk_dir%

md %trunk_dir%\Build

cd %trunk_dir%\Build

rem cmake -G ��ѡ��ָ��Ҫ���ɵĹ������ͣ������Ǹ���VS�İ汾��Ҳ������NMake��

rem -DBINARY_DIR=%trunk_dir% ������һ�������������CMakeList.txt�ļ���дʱ��һ�����ã������û��ɲ���

cmake -G "Visual Studio 14.0" %trunk_dir% -DBINARY_DIR=%trunk_dir%


@echo on