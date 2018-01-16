#设置cmake版本号
cmake_minimum_required(VERSION 3.4.0)

cmake_policy(SET CMP0020 NEW)
cmake_policy(SET CMP0071 NEW)

#设置工程名称
project(QtFrameDesignModel)

# 添加编译选项
set (CMAKE_PREFIX_PATH "C:\\Qt\\Qt5.6.3\\5.6.3\\msvc2015_64\\")

#设置工程包含当前目录，非必须
set(CMAKE_INCLUDE_CURRENT_DIR ON) 
#设置自动生成moc文件，一定要设置
set(CMAKE_AUTOMOC ON) 

#输出
message("project source dir: ${PROJECT_SOURCE_DIR}")
message("project source dir: ${PROJECT_BINARY_DIR}")

#查找需要的Qt库文件，最好每一个库都要写，Qt也会根据依赖关系自动添加
find_package(Qt5Widgets)  
find_package(Qt5Core)
find_package(Qt5Gui)

set (PROJECT_BINARY_DIR ./Bin)
#这个指令用于向当前工程添加存放源文件的子目录,并可以指定中间二进制和目标二进制存放的位置
#ADD_SUBDIRECTORY(Src Bin)
#外部构建时将生成的可执行文件拷贝到指定的目录
#set (CMAKE_INSTALL_PREFIX ${PROJECT_BINARY_DIR})

# add the binary tree to the search path for include files
# so that we will find TutorialConfig.h
include_directories("${PROJECT_BINARY_DIR}")
# The version number.
set (VERSION_MAJOR 1)
set (VERSION_MINOR 0)
# configure a header file to pass some of the CMake settings
# to the source code
# 加入一个配置头文件，用于处理 CMake 对源码的设置
configure_file (
  "${PROJECT_SOURCE_DIR}/Config.h.in"
  "${PROJECT_BINARY_DIR}/Config.h"
  )

# 添加头文件
include_directories(./Src/)
# 链接UI文件
#QT5_WRAP_UI(ui_FILES ./Src/QtFrameDesignModel.ui)
#SET (FILE main.cpp QtFrameDesignModel.h QtFrameDesignModel.cpp )



#查找当前文件夹中的所有源代码文件，也可以通过Set命令将所有文件设置为一个变量
FILE(GLOB SRC_FILES "./Src/*.cpp") 
#同理，查找当前文件夹中所有的头文件
FILE(GLOB HEAD_FILES "./Src/*.h") 
#同理，查找当前文件夹中所有的ui文件
FILE(GLOB UI_FILES "./Src/*.ui") 

#通过Ui文件生成对应的头文件，一定要添加
qt5_wrap_ui(WRAP_FILES ${UI_FILES})

#添加资源文件
set(RCC_FILES ./Resources/QtFrameDesignModel.qrc)

#将ui文件和生成文件整理在一个文件夹中，非必须
source_group("Ui" FILES ${UI_FILES} ${WRAP_FILES} )

#创建工程文件
add_executable(${PROJECT_NAME} ${SRC_FILES} ${HEAD_FILES} ${RCC_FILES} ${WRAP_FILES})
qt5_use_modules(QtFrameDesignModel Widgets)

#添加Qt5依赖项
target_link_libraries(${PROJECT_NAME} Qt5::Widgets  Qt5::Core Qt5::Gui)